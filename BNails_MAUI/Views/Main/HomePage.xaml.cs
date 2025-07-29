using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Services;
using BNails_MAUI.ViewModels.MainViewModels;

namespace BNails_MAUI.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        var services = ((App) App.Current!).Services;

        var dialogService = services?.GetService<IDialogService>();
        var usuarioService = services?.GetService<UsuarioService>();

        if(dialogService == null || usuarioService == null)
        {
            throw new Exception("No se pudieron obtener los servicios necesarios.");
        }

        BindingContext = new HomePageViewModel(dialogService,usuarioService);
    }
}