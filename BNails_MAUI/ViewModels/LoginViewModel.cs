using BNails_MAUI.Helpers;
using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Services;
using BNails_MAUI.Views;
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
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IDialogService _dialogService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioService _usuarioService;

        private string? email;
        private string? password;
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
                }
            }
        }

        public string? Password
        {
            get => password;
            set
            {
                if(password != value)
                {
                    password = value;
                    OnPropertyChanged();
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

        public ICommand LoginCommand { get; }

        public LoginViewModel(IDialogService dialogService, IUsuarioRepository usuarioRepository, UsuarioService usuarioService)
        {
            _dialogService = dialogService;
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;

            LoginCommand = new Command(OnLogin);
        }

        private async void OnLogin()
        {
            IsCargando = true;

            try
            {
                if(String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Password))
                {
                    await _dialogService.MostrarAlertaAsync("Atención!","Los campos Email y Contraseña no pueden estar vacíos");
                    return;
                }

                if(!Regex.IsMatch(Email,@"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    await _dialogService.MostrarAlertaAsync("Formato de Email incorrecto!","Formato correcto: 'ejemplo@mail.com'");
                    return;
                }

                if(!_usuarioService.ExisteUsuarioPorEmail(Email))
                {
                    await _dialogService.MostrarAlertaAsync("Atención!","El correo electrónico no está registrado");
                    return;
                }

                string? passwordGuardada = _usuarioRepository.ObtenerUsuarioPorEmail(Email)?.Password;

                if(!SeguridadHelper.VerificarPassword(Password,passwordGuardada))
                {
                    await _dialogService.MostrarAlertaAsync("Atención!","La contraseña es incorrecta!");
                    IsCargando = false;
                    return;
                }

                await Shell.Current.GoToAsync(nameof(HomePage));

            } finally
            {
                IsCargando = false;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));
    }
}
