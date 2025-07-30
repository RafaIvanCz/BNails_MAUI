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
            set => SetProperty(ref email,value,async () =>
            {
                if(!string.IsNullOrWhiteSpace(email))
                    await VerificiarPrimerIngreso();
            });
        }

        public HomePageViewModel(IDialogService dialogService, UsuarioService usuarioService)
        {
            _dialogService = dialogService;
            _usuarioService = usuarioService;
        }

        private async Task VerificiarPrimerIngreso()
        {
            IsBusy = true;

            try
            {
                var usuario = _usuarioService.GetUsuarioPorEmail(Email);

                if(usuario == null)
                {
                    await _dialogService.MostrarAlertaAsync("Usuario no encontrado", "Hubo un error y no se encontró el usuario. Volvé a iniciar sesión.");
                    await Shell.Current.GoToAsync("//Login");
                }

                if(usuario.FechaPrimerIngreso != null)
                    return;

                DateTime fechaIngreso = DateTime.Now;
                usuario.FechaPrimerIngreso = fechaIngreso;

                bool fechaGuardada = _usuarioService.GuardarFechaPrimerIngreso(Email,fechaIngreso);

                if(fechaGuardada)
                {
                    await _dialogService.MostrarAlertaAsync(
                        $"¡Bienvenido/a {usuario.Nombre} a BNails!",
                        "Esperamos que tengas una excelente experiencia en la app."
                    );
                }

            } finally
            {
                IsBusy = false;
            }
        }
    }
}
