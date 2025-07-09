using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Models;
using BNails_MAUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
        private readonly UsuarioService _usuarioService;
        private readonly IEmailService _emailService;

        private string? codigoIngresado;
        public string? CodigoIngresado
        {
            get => codigoIngresado;
            set
            {
                codigoIngresado = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UnenabledBtn));
            }
        }

        public bool UnenabledBtn => !String.IsNullOrWhiteSpace(codigoIngresado) && codigoIngresado.Length == 4;
        public bool ReenviarCodigo;

        public ICommand ConfirmarCodigoCommand { get; }
        public ICommand ReenviarCodigoCommand { get; }

        public ValidarCodigoViewModel(IDialogService dialogService, UsuarioService usuarioService, IEmailService emailService)
        {
            _dialogService = dialogService;
            _usuarioService = usuarioService;
            _emailService = emailService;

            ConfirmarCodigoCommand = new Command(OnValidarCodigo);
            ReenviarCodigoCommand = new Command(OnReenviarCodigo);
        }

        private int intentos = 3;

        public async void OnValidarCodigo()
        { 
            Usuario? usuario = _usuarioService.GetUsuarioPorEmail(Email);

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
                    return;
                }

                await _dialogService.MostrarAlertaAsync("Atención!",$"El código ingresado no coincide con el enviado a tu correo electrónico. Te quedan {intentos} intentos.");
                return;                
            }

            if(usuario.CodigoRecuExpiro < DateTime.Now)
            {
                await _dialogService.MostrarAlertaAsync("Atención!","El código ingresado ha expirado. Por favor, volvé a solicitarlo.");
                return;
            }

            await Shell.Current.GoToAsync($"ResetPwd?email={Email}");
        }

        public async void OnReenviarCodigo()
        {
            string? nombreUsuario = _usuarioService?.GetUsuarioPorEmail(Email).Nombre;
            string codigoVerificacion = _emailService.GenerarCodigoVerificacion();

            bool codigoReenviado = await _emailService.EnviarEmailCodigoVerificacion(Email,codigoVerificacion,nombreUsuario);

            if(codigoReenviado)
            {
                await _dialogService.MostrarAlertaAsync("Código reenviado con éxito!","Revisá tu correo electrónico para ver el código de 4 dígitos que se te envió.");
                return;
            } else
            {
                await _dialogService.MostrarAlertaAsync("Error","Ocurrió un error al enviar el email. Intentá nuevamente.");
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));
    }
}
