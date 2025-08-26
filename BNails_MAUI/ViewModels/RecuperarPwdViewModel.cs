using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BNails_MAUI.ViewModels
{
    public class RecuperarPwdViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IDialogService _dialogService;
        private readonly UsuarioService _usuarioService;
        private readonly IEmailService _emailService;

        private string? email;
        private bool comenzoAEscribirEmail;
        private bool emailCorrecto;
        private bool isCargando;

        public string? Email
        {
            get => email;
            set
            {
                if(email != value)
                {
                    email = value;
                    OnPropertyChanged();
                    if(!string.IsNullOrEmpty(email))
                    {
                        ComenzoAEscribirEmail = true;
                        ValidarEmail();
                    }
                }
            }
        }

        public bool EmailCorrecto
        {
            get => emailCorrecto;
            set
            {
                if(emailCorrecto != value)
                {
                    emailCorrecto = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MostrarFormatoEmail));
                }
            }
        }

        public bool ComenzoAEscribirEmail
        {
            get => comenzoAEscribirEmail;
            set
            {
                if(comenzoAEscribirEmail != value)
                {
                    comenzoAEscribirEmail = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MostrarFormatoEmail));
                }
            }
        }

        public bool IsCargando
        {
            get => isCargando;
            set
            {
                if(isCargando != value)
                {
                    isCargando = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool MostrarFormatoEmail => ComenzoAEscribirEmail && !EmailCorrecto;

        public ICommand RecuperarPwdCommand { get; }

        public RecuperarPwdViewModel(IDialogService dialogService, UsuarioService usuarioService, IEmailService emailService)
        {
            _dialogService = dialogService;
            _usuarioService = usuarioService;
            _emailService = emailService;

            RecuperarPwdCommand = new Command(OnRecuperarPwd);
        }

        public async void OnRecuperarPwd()
        {
            IsCargando = true;

            try
            {
                if(!EmailCorrecto)
                {
                    await _dialogService.MostrarAlertaAsync("Formato de Email incorrecto!","Formato correcto: 'ejemplo@mail.com'");
                    return;
                }

                if(!_usuarioService.ExisteUsuarioPorEmail(Email))
                {
                    await _dialogService.MostrarAlertaAsync("Atención!","El correo electrónico no está registrado");
                    return;
                }

                string codigoVerificacion = _emailService.GenerarCodigoVerificacion();
                string? nombreUsuario = _usuarioService.GetUsuarioPorEmail(Email)?.Nombre;

                bool emailEnviado = await _emailService.EnviarEmailCodigoVerificacion(Email,codigoVerificacion,nombreUsuario);

                if(emailEnviado)
                {
                    await _dialogService.MostrarAlertaAsync("Código enviado con éxito!","Revisá tu correo electrónico.");
                    await Shell.Current.GoToAsync($"ValidarCodigo?email={Email}");
                } else
                {
                    await _dialogService.MostrarAlertaAsync("Error","Ocurrió un error al enviar el email. Intentá nuevamente.");
                }

            } finally
            {
                IsCargando = true;
            }            
        }

        private void ValidarEmail()
        {
            EmailCorrecto = Regex.IsMatch(Email,@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }


        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));
    }
}
