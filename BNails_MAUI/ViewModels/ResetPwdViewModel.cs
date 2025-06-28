using BNails_MAUI.Helpers;
using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Models;
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
    [QueryProperty(nameof(Email),"email")]

    public class ResetPwdViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IDialogService _dialogService;
        private readonly UsuarioService _usuarioService;

        private string? password;
        private string? rePassword;

        private bool comenzoAEscribirPassword;
        private bool comenzoAEscribirRePassword;

        private bool cumpleMinCaracteres;
        private bool tieneMayuscula;
        private bool tieneNumero;
        private bool coincidenPasswords;

        public string? Password
        {
            get => password;
            set
            {
                if(password != value)
                {
                    password = value;
                    OnPropertyChanged();
                    if(!string.IsNullOrEmpty(password))
                        ComenzoAEscribirPassword = true;
                    ValidarPassword();
                    ValidarCoincidencia();
                }
            }
        }

        public string? RePassword
        {
            get => rePassword;
            set
            {
                if(rePassword != value)
                {
                    rePassword = value;
                    OnPropertyChanged();
                    if(!string.IsNullOrEmpty(rePassword))
                        ComenzoAEscribirRePassword = true;
                    ValidarCoincidencia();
                }
            }
        }

        public bool ComenzoAEscribirPassword
        {
            get => comenzoAEscribirPassword;
            set
            {
                if(comenzoAEscribirPassword != value)
                {
                    comenzoAEscribirPassword = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MostrarMinCaracteres));
                    OnPropertyChanged(nameof(MostrarMayuscula));
                    OnPropertyChanged(nameof(MostrarNumero));
                }
            }
        }

        public bool ComenzoAEscribirRePassword
        {
            get => comenzoAEscribirRePassword;
            set
            {
                if(comenzoAEscribirRePassword != value)
                {
                    comenzoAEscribirRePassword = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MostrarCoincidencia));
                }
            }
        }

        public bool CumpleMinCaracteres
        {
            get => cumpleMinCaracteres;
            set
            {
                if(cumpleMinCaracteres != value)
                {
                    cumpleMinCaracteres = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MostrarMinCaracteres));
                }
            }
        }

        public bool TieneMayuscula
        {
            get => tieneMayuscula;
            set
            {
                if(tieneMayuscula != value)
                {
                    tieneMayuscula = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MostrarMayuscula));
                }
            }
        }

        public bool TieneNumero
        {
            get => tieneNumero;
            set
            {
                if(tieneNumero != value)
                {
                    tieneNumero = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MostrarNumero));
                }
            }
        }

        public bool CoincidenPasswords
        {
            get => coincidenPasswords;
            set
            {
                if(coincidenPasswords != value)
                {
                    coincidenPasswords = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MostrarCoincidencia));
                }
            }
        }
        public bool MostrarMinCaracteres => ComenzoAEscribirPassword && !CumpleMinCaracteres;
        public bool MostrarMayuscula => ComenzoAEscribirPassword && !TieneMayuscula;
        public bool MostrarNumero => ComenzoAEscribirPassword && !TieneNumero;
        public bool MostrarCoincidencia => ComenzoAEscribirRePassword && !CoincidenPasswords;

        public ICommand ResetPwdCommand { get; }
        public string? Email { get; set; }

        public ResetPwdViewModel(IDialogService dialogService, UsuarioService usuarioService)
        {
            _dialogService = dialogService;
            _usuarioService = usuarioService;

            ResetPwdCommand = new Command(OnActualizarPwd);
        }

        public async void OnActualizarPwd()
        {
            if(String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(RePassword))
            {
                await _dialogService.MostrarAlertaAsync("Atención!", "Completá todos los campos");
                return;
            }

            if(!Regex.IsMatch(Password,@"^(?=.*[A-Z])(?=.*\d).{8,}$"))
            {
                await _dialogService.MostrarAlertaAsync("Atención!","Correo no válido");
                return;
            }

            string hashedPassword = SeguridadHelper.HashPassword(Password);

            Usuario? actualizarUsuario = _usuarioService.GetUsuarioPorEmail(Email);

            if(actualizarUsuario != null)
            {
                actualizarUsuario.Password = hashedPassword;
            }

            bool actualizado = _usuarioService.ActualizarPwdUsuario(actualizarUsuario);

            if(actualizado)
            {
                await _dialogService.MostrarAlertaAsync("Éxito","La contraseña fue actualizada correctamente");
                await Shell.Current.GoToAsync("//Login");
            } else
            {
                await _dialogService.MostrarAlertaAsync("Error","Ocurrió un error al actualizar la contraseña");
            }
        }

        private void ValidarPassword()
        {
            CumpleMinCaracteres = Password.Length >= 8;
            TieneMayuscula = Regex.IsMatch(Password,@"[A-Z]");
            TieneNumero = Regex.IsMatch(Password,@"\d");
        }

        private void ValidarCoincidencia()
        {
            CoincidenPasswords = Password == RePassword;
        }

        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));
    }
}
