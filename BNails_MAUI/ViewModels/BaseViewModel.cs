using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private string? _title;

        public event PropertyChangedEventHandler PropertyChanged;

        // Para mostrar si la vista está cargando o haciendo algo
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy,value);
        }

        // Título opcional de la página
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title,value);
        }

        // Método genérico para notificar cambios en propiedades
        protected bool SetProperty<T>(ref T backingStore,T value,[CallerMemberName] string propertyName = "")
        {
            if(EqualityComparer<T>.Default.Equals(backingStore,value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
    }
}
