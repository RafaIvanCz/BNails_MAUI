using BNails_MAUI.Interfaces;
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
    public class RegistroViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IDialogService dialogService;

        private string? nombre;
        private string? email;
        private string? password;
        private string? rePassword;

        private bool comenzoAEscribirEmail;
        private bool comenzoAEscribirPassword;
        private bool comenzoAEscribirRePassword;

        private bool emailCorrecto;
        private bool cumpleMinCaracteres;
        private bool tieneMayuscula;
        private bool tieneNumero;
        private bool coincidenPasswords;

        public string Nombre
        {
            get => nombre;
            set
            {
                if(nombre != value)
                {
                    nombre = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
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

        public string Password
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

        public string RePassword
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

        // Mostrar validaciones solo si el usuario ya empezó a escribir
        public bool MostrarFormatoEmail => ComenzoAEscribirEmail && !EmailCorrecto;
        public bool MostrarMinCaracteres => ComenzoAEscribirPassword && !CumpleMinCaracteres;
        public bool MostrarMayuscula => ComenzoAEscribirPassword && !TieneMayuscula;
        public bool MostrarNumero => ComenzoAEscribirPassword && !TieneNumero;
        public bool MostrarCoincidencia => ComenzoAEscribirRePassword && !CoincidenPasswords;

        public ICommand? RegistrarCommand { get; }

        public RegistroViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            RegistrarCommand = new Command(OnRegistrar);
        }

        private async void OnRegistrar()
        {
            if(string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(RePassword))
            {
                await dialogService.MostrarAlertaAsync("Error","Completá todos los campos");
                return;
            }

            if(!Regex.IsMatch(Email,@"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                await dialogService.MostrarAlertaAsync("Error","Correo inválido");
                return;
            }

            if(!CumpleMinCaracteres || !TieneMayuscula || !TieneNumero)
            {
                await dialogService.MostrarAlertaAsync("Error","Contraseña insegura");
                return;
            }

            if(!CoincidenPasswords)
            {
                await dialogService.MostrarAlertaAsync("Error","Las contraseñas no coinciden");
                return;
            }

            string hashedPassword = Helpers.SeguridadHelper.HashPassword(Password);

            // Acá llamás a tu servicio para guardar el usuario en la base
        }

        private void ValidarEmail()
        {
            EmailCorrecto = Regex.IsMatch(Email,@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
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
