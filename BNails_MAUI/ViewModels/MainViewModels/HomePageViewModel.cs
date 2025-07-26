using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.ViewModels.MainViewModels
{
    [QueryProperty(nameof(Email), "email")]

    public class HomePageViewModel
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public string? Email { get; set; }

        public HomePageViewModel(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        DateTime fecha = DateTime.Now;
    }
}
