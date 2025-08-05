using System.Collections.Generic;
using BNails_MAUI.Models;

namespace BNails_MAUI.Interfaces.Repositories
{
    public interface ITipoTrabajoRepository
    {
        List<TipoTrabajo> ObtenerTodos();
    }
}
