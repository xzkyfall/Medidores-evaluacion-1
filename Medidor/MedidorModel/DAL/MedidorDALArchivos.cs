using MedidorModel.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DAL
{
    public class MedidorDALArchivos : IMedidoresDAL
    {
        private MedidorDALArchivos()
        {

        }
        private static MedidorDALArchivos instancia;
        public static IMedidoresDAL GetInstacia()
        {
            if (instancia == null)
            {
                instancia = new MedidorDALArchivos();
            }
            return instancia;
        }

        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/Medidores.txt";
        public void AgregarMedidor(Medidores medidor)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(medidor.Medidor + ";" + medidor.Fecha + ";" + medidor.ValorConsumo);
                    writer.Flush();
                }
            }
            catch (Exception)
            {

            }
            
        }
        public List<Medidores> FiltrarMedidores(string medidor)
        {
            return ObtenerMedidores().FindAll(m => m.Medidor == medidor);

        }
        public List<Medidores> ObtenerMedidores()
        {

            List<Medidores> lista = new List<Medidores>();
            try
            {
                using(StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if(texto != null)
                        {
                            string[] arr = texto.Trim().Split(';');
                            Medidores medidor = new Medidores()
                            {
                                Medidor = arr[0],
                                Fecha = arr[1],
                                ValorConsumo = arr[2]
                            };
                            lista.Add(medidor);
                        }

                    } while (texto != null);
                }
            }catch (Exception)
            {
                lista = null;
            }
            return lista;
        }
    }
}
