using BNails_MAUI.Models;
using BNails_MAUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            return _usuarioRepository.GuardarUsuario(usuario);
        }

        public bool ExisteUsuarioPorEmail(string email)
        {
            return _usuarioRepository.ExisteUsuarioPorEmail(email);
        }

        public Usuario? GetUsuarioPorEmail(string email)
        {
            return _usuarioRepository.ObtenerUsuarioPorEmail(email);
        }

        public bool ActualizarPwdUsuario(Usuario usuario)
        {
            return _usuarioRepository.ActualizarPwd(usuario);
        }

        public bool GuardarCodigoVerificacion(string email,string codigo,DateTime fechaExpiracion)
        {
            return _usuarioRepository.GuardarCodigoVerificacion(email,codigo,fechaExpiracion);
        }
    }
}
