using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.ViewModels.MainViewModels
{
    [QueryProperty(nameof(Email), "email")]

    public class HomePageViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly UsuarioService _usuarioService;

        private string? email;
        public string Email
        {
            get => email;
            set
            {
                if(email != value)
                {
                    email = value;
                    OnPropertyChanged();
                    _ = VerificiarPrimerIngreso();
                }
            }
        }

        public HomePageViewModel(IDialogService dialogService, UsuarioService usuarioService)
        {
            _dialogService = dialogService;
            _usuarioService = usuarioService;
        }

        public async Task VerificiarPrimerIngreso()
        {
            var usuario = _usuarioService.GetUsuarioPorEmail(Email);
            if(usuario == null)
                return;

            if(usuario.FechaPrimerIngreso == null)
            {
                DateTime fechaIngreso = DateTime.Now;
                usuario.FechaPrimerIngreso = fechaIngreso;

                bool fechaGuardada = _usuarioService.GuardarFechaPrimerIngreso(Email, fechaIngreso);

                if(fechaGuardada)
                {
                    await _dialogService.MostrarAlertaAsync($"¡Bienvenido/a {usuario.Nombre} a BNails!","Esperamos que tengas una excelente experiencia en la app.");
                }
            }
        }
    }
}
