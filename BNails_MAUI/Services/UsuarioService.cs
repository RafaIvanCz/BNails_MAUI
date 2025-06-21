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
    }
}
