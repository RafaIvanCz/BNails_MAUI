using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Models;
using FFImageLoading.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BNails_MAUI.ViewModels
{
    [QueryProperty(nameof(Id),"id")]
    [QueryProperty(nameof(Nombre),"nombre")]
    public class TipoTrabajosViewModel : BaseViewModel
    {
        private readonly IFotosTrabajosRepository _fotosRepository;

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if(_id == value) return;
                _id = value;
                // cuando llega el parámetro, cargamos
                if(_id > 0) CargarFotos();
                OnPropertyChanged();
            }
        }

        public string Nombre { get; set; } = string.Empty;

        public ObservableCollection<FotoTrabajo> Fotos { get; } = new();

        public TipoTrabajosViewModel(IFotosTrabajosRepository fotosRepository)
        {
            _fotosRepository = fotosRepository;
        }

        private void CargarFotos()
        {
            Fotos.Clear();
            var fotos = _fotosRepository.ObtenerFotosPorTipoTrabajo(Id);
            foreach(var f in fotos) Fotos.Add(f);
        }
    }
}