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
    public class TipoTrabajosViewModel : BaseViewModel
    {
        private readonly IFotosTrabajosRepository _fotosRepository;
        private readonly Usuario _usuarioActual;
        private readonly int _tipoTrabajoId;

        public ObservableCollection<FotoTrabajo> Fotos { get; } = new();
        public bool IsAdmin => _usuarioActual.Email == "ivancho.ugbs@gmail.com"; // ajustar según tu lógica
        public ICommand SubirFotoCommand { get; }

        public TipoTrabajosViewModel(int tipoTrabajoId)
        {
            _fotosRepository = ServiceHelper.GetService<IFotosTrabajosRepository>();
            _tipoTrabajoId = tipoTrabajoId;

            SubirFotoCommand = new Command(async () => await SubirFoto());
            CargarFotos();
        }

        private void CargarFotos()
        {
            var fotos = _fotosRepository.ObtenerFotosPorTipoTrabajo(_tipoTrabajoId);
            Fotos.Clear();

            foreach(var foto in fotos)
                Fotos.Add(foto);
        }

        private async Task SubirFoto()
        {
            var file = await FilePicker.PickAsync(new PickOptions { PickerTitle = "Selecciona una foto" });
            if(file != null)
            {
                var nuevaFoto = new FotoTrabajo
                {
                    TipoTrabajoId = _tipoTrabajoId,
                    RutaImagen = file.FullPath,
                    Descripcion = "Nueva foto"
                };

                if(_fotosRepository.AgregarFotoTrabajo(nuevaFoto))
                    Fotos.Add(nuevaFoto);
            }
        }
    }
}