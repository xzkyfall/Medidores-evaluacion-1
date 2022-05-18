using Medidor.Comunicacion;
using MedidorModel.DAL;
using MedidorModel.DTO;
using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medidor
{
    class Program
    {
        private static IMedidoresDAL medidorDAL = MedidorDALArchivos.GetInstacia();

        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("¿Que quiere hacer");
            Console.WriteLine(" 1. Ingresar \n 2. Mostrar todos los medidores \n 3. Buscar medidor \n 0. Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1": Ingresar();
                    break;
                case "2": Mostrar();
                    break;
                case "3": Buscar();
                    break;
                case "0": continuar = false;
                    break;
                default: Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;
        }


        static void Main(string[] args)
        {
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.IsBackground = true;
            t.Start();
            while (Menu()) ;
        }

        static void Ingresar()
        {
            Console.WriteLine("Ingrese medidor :");
            string medidor = Console.ReadLine().Trim();
            Console.WriteLine("Ingrese fecha : ");
            string fecha = Console.ReadLine().Trim();
            Console.WriteLine("Ingrese valor de consumo : ");
            string valorConsumo = Console.ReadLine().Trim();
            Medidores medidores = new Medidores()
            {
                Medidor = medidor,
                Fecha = fecha,
                ValorConsumo = valorConsumo
            };
            medidorDAL.AgregarMedidor(medidores);
        }

        static void Mostrar()
        {
            List<Medidores> medidores = medidorDAL.ObtenerMedidores();
            foreach(Medidores medidor in medidores)
            {
                Console.WriteLine(medidor);
            }
        }

        static void Buscar()
        {
            Console.WriteLine("Ingrese medidor: ");
            List<Medidores> filtrada = medidorDAL.FiltrarMedidores(Console.ReadLine().Trim());
            filtrada.ForEach(m => Console.WriteLine("Medidor: {0} Fecha {1} Valor de consumo {2}" , m.Medidor, m.Fecha, m.ValorConsumo));
        }

    }
}
