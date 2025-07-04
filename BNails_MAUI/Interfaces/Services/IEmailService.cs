using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Interfaces.Services
{
    public interface IEmailService
    {
        string GenerarCodigoVerificacion();
        Task<bool> EnviarEmailCodigoVerificacion(string email, string codigoVerificacion, string nombreUsuario);
    }
}
