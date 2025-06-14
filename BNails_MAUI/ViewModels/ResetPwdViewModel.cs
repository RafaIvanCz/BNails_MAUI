using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BNails_MAUI.ViewModels
{
    class ResetPwdViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string email;
        private bool comenzoAEscribirEmail;
        private bool emailCorrecto;

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

        private bool MostrarFormatoEmail => ComenzoAEscribirEmail && !EmailCorrecto;

        private void ValidarEmail()
        {
            EmailCorrecto = Regex.IsMatch(Email,@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }


        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));

    }
}
