using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Models;
using BNails_MAUI.Services;
using BNails_MAUI.ViewModels.MainViewModels;
using BNails_MAUI.Views.Main;

namespace BNails_MAUI.Views.Main
{
    public partial class HomePage : ContentPage
    {
        public HomePage(HomePageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnTipoTrabajoSeleccionado(object sender,SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.FirstOrDefault() is TipoTrabajo tipo)
            {
                await Navigation.PushAsync(new TipoTrabajoPage(tipo.Id));
            }
        }
    }
}