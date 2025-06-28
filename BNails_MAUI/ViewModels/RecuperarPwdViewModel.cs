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
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioService _usuarioService;

        private string? email;
        private bool comenzoAEscribirEmail;
        private bool emailCorrecto;

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

        public bool MostrarFormatoEmail => ComenzoAEscribirEmail && !EmailCorrecto;

        public ICommand RecuperarPwdCommand { get; }

        public RecuperarPwdViewModel(IDialogService dialogService, IUsuarioRepository usuarioRepository, UsuarioService usuarioService)
        {
            _dialogService = dialogService;
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;

            RecuperarPwdCommand = new Command(OnRecuperarPwd);
        }

        public async void OnRecuperarPwd()
        {
            if(!EmailCorrecto)
            {
                await _dialogService.MostrarAlertaAsync("Formato de Email incorrecto!", "Formato correcto: 'ejemplo@mail.com'");
                return;
            }

            if(!_usuarioService.ExisteUsuarioPorEmail(Email))
            {
                await _dialogService.MostrarAlertaAsync("Atención!", "El correo electrónico no está registrado");
                return;
            }

            await _dialogService.MostrarAlertaAsync("Email enviado con éxito!", "Revisa tu correo electrónico y seguí los pasos para crear una contraseña nueva.");
        }

        private void ValidarEmail()
        {
            EmailCorrecto = Regex.IsMatch(Email,@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }


        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));

    }
}
