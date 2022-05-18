using MedidorModel.DAL;
using MedidorModel.DTO;
using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Medidor.Comunicacion
{
    public class HebraServidor
    {
        private static IMedidoresDAL medidorDAL = MedidorDALArchivos.GetInstacia();
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket servidor = new ServerSocket(puerto);
            Console.WriteLine("S: Lenvantando servidor en puerto {0}", puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("S: Esperando Cliente..");
                    Socket cliente = servidor.ObtenerCliente();
                    Console.WriteLine("S: cliente recibido");
                    ClienteCom clienteCom = new ClienteCom(cliente);

                    clienteCom.Escribir("Ingrese medidor : ");
                    string medidor = clienteCom.Leer();
                    clienteCom.Escribir("Ingrese fecha :");
                    string fecha = clienteCom.Leer();
                    clienteCom.Escribir("Ingrese valor de consumo :");
                    string valorConsumo = clienteCom.Leer();
                    Medidores mensaje = new Medidores()
                    {
                        Medidor = medidor,
                        Fecha = fecha,
                        ValorConsumo = valorConsumo
                    };
                    clienteCom.Escribir("Ingresado con exito");

                    medidorDAL.AgregarMedidor(mensaje);
                    
                    clienteCom.Desconectar();
                }
            }
            else
            {
                Console.WriteLine("no se puede levantar server en {0}", puerto);
            }
        }
    }
}
