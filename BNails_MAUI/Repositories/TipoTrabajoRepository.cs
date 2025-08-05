using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Repositories
{
    public class TipoTrabajoRepository : ITipoTrabajoRepository
    {
        public List<TipoTrabajo> ObtenerTodos()
        {
            // Ejemplo de datos hardcodeados para prueba
            return new List<TipoTrabajo>
            {
                new TipoTrabajo { Id = 1, Nombre = "Francesita" },
                new TipoTrabajo { Id = 2, Nombre = "Soft Gel" },
                new TipoTrabajo { Id = 3, Nombre = "Acrílica" },
                new TipoTrabajo { Id = 4, Nombre = "Kapping" },
                new TipoTrabajo { Id = 5, Nombre = "Semi-permanentes" }
            };
        }
    }
}
