using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DTO
{
    public class Medidores
    {
        private string medidor;
        private string fecha;
        private string valorConsumo;

        public string Medidor { get => medidor; set => medidor = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string ValorConsumo { get => valorConsumo; set => valorConsumo = value; }

        public override string ToString()
        {
            return medidor + " | " + fecha + " | " + valorConsumo;
        }
    }
}
