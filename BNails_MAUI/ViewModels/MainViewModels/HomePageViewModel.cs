using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Models;
using BNails_MAUI.Services;
using FFImageLoading.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BNails_MAUI.ViewModels.MainViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly ITipoTrabajoRepository _tipoTrabajoRepository;

        public ObservableCollection<TipoTrabajo> TiposTrabajo { get; } = [];

        public ICommand TipoTrabajoCommand { get; }

        public HomePageViewModel(ITipoTrabajoRepository tipoTrabajoRepository)
        {
            _tipoTrabajoRepository = tipoTrabajoRepository;
            CargarTiposTrabajo();

            TipoTrabajoCommand = new Command<TipoTrabajo>(OnTipoTrabajo);
        }

        public async void OnTipoTrabajo(TipoTrabajo tipoTrabajo)
        {
            if (tipoTrabajo == null) return;

            var nombre = Uri.EscapeDataString(tipoTrabajo.Nombre ?? String.Empty);
            await Shell.Current.GoToAsync($"{nameof(Views.Main.TipoTrabajoPage)}?id={tipoTrabajo.Id}&nombre={nombre}");
        }

        private void CargarTiposTrabajo()
        {
            var lista = _tipoTrabajoRepository.ObtenerTodos();

            TiposTrabajo.Clear();
            foreach(var tipo in lista)
                TiposTrabajo.Add(tipo);
        }
    }
}