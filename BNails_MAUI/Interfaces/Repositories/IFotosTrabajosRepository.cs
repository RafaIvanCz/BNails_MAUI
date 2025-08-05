using BNails_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Interfaces.Repositories
{
    public interface IFotosTrabajosRepository
    {
        bool AgregarFotoTrabajo(FotoTrabajo foto);
        List<FotoTrabajo> ObtenerFotosPorTipoTrabajo(int tipoTrabajoId);
    }
}
