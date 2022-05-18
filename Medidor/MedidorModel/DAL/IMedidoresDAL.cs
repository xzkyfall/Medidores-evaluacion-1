using MedidorModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DAL
{
    public interface IMedidoresDAL
    {
        void AgregarMedidor(Medidores medidores);

        List<Medidores> ObtenerMedidores();

        List <Medidores> FiltrarMedidores(string medidor);
    }
}

