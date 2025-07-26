using BNails_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        bool GuardarUsuario(Usuario usuario);
        bool ExisteUsuarioPorEmail(string email);
        Usuario? ObtenerUsuarioPorEmail(string email);
        bool GuardarCodigoVerificacion(string email,string codigo,DateTime fechaExpiracion);
        bool GuardarFechaPrimerIngreso(string email, DateTime fechaPrimerIngreso);
    }
}
