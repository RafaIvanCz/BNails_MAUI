using BNails_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Interfaces.Services
{
    public interface IUsuarioService
    {
        public bool RegistrarUsuario(Usuario usuario);
        public bool ExisteUsuarioPorEmail(string email);
        public Usuario? GetUsuarioPorEmail(string email);
        public bool ActualizarPwdUsuario(Usuario usuario);
        public bool GuardarCodigoVerificacion(string email,string codigo,DateTime fechaExpiracion);
        public bool GuardarFechaPrimerIngreso(string email, DateTime fechaPrimerIngreso);
    }
}
