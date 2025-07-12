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
    public class RegistroViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IDialogService _dialogService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioService _usuarioService;

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

        private bool isCargando;

        public string? Nombre
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

        // Mostrar validaciones solo si el usuario ya empezó a escribir
        public bool MostrarFormatoEmail => ComenzoAEscribirEmail && !EmailCorrecto;
        public bool MostrarMinCaracteres => ComenzoAEscribirPassword && !CumpleMinCaracteres;
        public bool MostrarMayuscula => ComenzoAEscribirPassword && !TieneMayuscula;
        public bool MostrarNumero => ComenzoAEscribirPassword && !TieneNumero;
        public bool MostrarCoincidencia => ComenzoAEscribirRePassword && !CoincidenPasswords;

        public ICommand? RegistrarCommand { get; }

        public RegistroViewModel(IDialogService dialogService, IUsuarioRepository usuarioRepository, UsuarioService usuarioService)
        {
            this._dialogService = dialogService;
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;

            RegistrarCommand = new Command(OnRegistrar);
        }

        private async void OnRegistrar()
        {
            IsCargando = true;

            try
            {
                if(string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Email) ||
                    string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(RePassword))
                {
                    await _dialogService.MostrarAlertaAsync("Atención!","Completá todos los campos");
                    return;
                }

                if(!EmailCorrecto)
                {
                    await _dialogService.MostrarAlertaAsync("Atención!","Correo no válido");
                    return;
                }

                if(_usuarioRepository.ExisteUsuarioPorEmail(Email))
                {
                    await _dialogService.MostrarAlertaAsync("Atención!","El correo ya se encuentra registrado en el sistema");
                    return;
                }

                if(!CumpleMinCaracteres || !TieneMayuscula || !TieneNumero)
                {
                    await _dialogService.MostrarAlertaAsync("Atención!","La contraseña no cumple las condiciones de seguridad");
                    return;
                }

                if(!CoincidenPasswords)
                {
                    await _dialogService.MostrarAlertaAsync("Atención!","Las contraseñas no coinciden");
                    RePassword = string.Empty;
                    return;
                }

                string hashedPassword = Helpers.SeguridadHelper.HashPassword(Password);

                var usuario = new Usuario
                {
                    Nombre = Nombre,
                    Email = Email,
                    Password = hashedPassword
                };

                var usuarioService = new UsuarioService();
                bool registroExitoso = usuarioService.RegistrarUsuario(usuario);

                if (registroExitoso)
                {
                    await _dialogService.MostrarAlertaAsync("Felicidades!", "Usuario guardado con éxito!");
                    await Shell.Current.GoToAsync("..");
                } else
                {
                    await _dialogService.MostrarAlertaAsync("Atención!", "No se pudo guardar el usuario. Intente nuevamente.");
                }
            }finally
            {
                IsCargando = true;
            }

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
