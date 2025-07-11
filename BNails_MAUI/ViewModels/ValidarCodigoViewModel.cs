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

        private int intentosIngresoCod = 3;
        private bool codigoVencido = false;

        private string? codigoIngresado;
        public string? CodigoIngresado
        {
            get => codigoIngresado;
            set
            {
                codigoIngresado = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UnenabledBtn));
                OnPropertyChanged(nameof(UnenabledReenviarCodigoLbl));
            }
        }

        public bool UnenabledBtn => !String.IsNullOrWhiteSpace(codigoIngresado) && codigoIngresado.Length == 4 && !codigoVencido;
        public bool UnenabledReenviarCodigoLbl => codigoVencido;
        public Color LabelTextColor => codigoVencido ? Colors.DarkBlue : Colors.Gray;


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
                intentosIngresoCod--;

                if(intentosIngresoCod <= 0)
                {
                    await _dialogService.MostrarAlertaAsync("Atención!","Superaste el número máximo de intentos. Por favor, volvé a solicitar el código de recuperación.");
                    codigoVencido = true;
                    OnPropertyChanged(nameof(UnenabledReenviarCodigoLbl));
                    OnPropertyChanged(nameof(LabelTextColor));
                    return;
                }

                await _dialogService.MostrarAlertaAsync("Atención!",$"El código ingresado no coincide con el enviado a tu correo electrónico. Te quedan {intentosIngresoCod} intentos.");
                return;                
            }

            if(usuario.CodigoRecuExpiro < DateTime.Now)
            {
                await _dialogService.MostrarAlertaAsync("Atención!","El código ingresado ha expirado. Por favor, volvé a solicitarlo.");
                codigoVencido = true;
                OnPropertyChanged(nameof(UnenabledReenviarCodigoLbl));
                OnPropertyChanged(nameof(LabelTextColor));
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
                await _dialogService.MostrarAlertaAsync("Código reenviado con éxito!","Revisá tu correo electrónico.");
                codigoVencido = false;
                intentosIngresoCod = 3;
                OnPropertyChanged(nameof(UnenabledReenviarCodigoLbl));
                OnPropertyChanged(nameof(LabelTextColor));
                OnPropertyChanged(nameof(UnenabledBtn));
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
