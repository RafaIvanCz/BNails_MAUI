using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Models;
using BNails_MAUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BNails_MAUI.ViewModels
{
    [QueryProperty(nameof(Email),"email")]

    public class ValidarCodigoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string? Email { get; set; }

        private readonly IDialogService _dialogService;
        private readonly UsuarioService usuarioService;

        private string? codigo;

        public string? CodigoIngresado
        {
            get => codigo;
            set
            {
                codigo = value;
                OnPropertyChanged();
            }
        }

        public ICommand ConfirmarCodigoCommand { get; }

        public ValidarCodigoViewModel(IDialogService dialogService, UsuarioService usuarioService)
        {
            _dialogService = dialogService;
            this.usuarioService = usuarioService;

            ConfirmarCodigoCommand = new Command(ValidarCodigo);
        }

        private int intentos = 3;

        public async void ValidarCodigo()
        { 
            Usuario? usuario = usuarioService.GetUsuarioPorEmail(Email);

            if(usuario == null)
            {
                await _dialogService.MostrarAlertaAsync("Atención!","No se encontró el usuario.");
                return;
            }

            if(CodigoIngresado != usuario.CodigoRecuperacion)
            {
                intentos--;

                if(intentos <= 0)
                {
                    await _dialogService.MostrarAlertaAsync("Atención!","Superaste el número máximo de intentos. Por favor, volvé a solicitar el código de recuperación.");
                    await Shell.Current.GoToAsync("RecuperarPwd");
                    return;
                }

                await _dialogService.MostrarAlertaAsync("Atención!",$"El código ingresado no coincide con el enviado a tu correo electrónico. Te quedan {intentos} intentos.");
                return;                
            }

            if(usuario.CodigoRecuExpira < DateTime.Now)
            {
                await _dialogService.MostrarAlertaAsync("Atención!","El código ingresado ha expirado. Por favor, volvé a solicitar el código de recuperación.");
                await Shell.Current.GoToAsync("//RecuperarPwd");
                return;
            }

            await Shell.Current.GoToAsync($"ResetPwd?email={Email}");
        }

        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));
    }
}
