using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Services;
using BNails_MAUI.ViewModels;

namespace BNails_MAUI.Views;

public partial class ValidarCodigo : ContentPage
{
	public ValidarCodigo()
	{
		InitializeComponent();
        var services = ((App) App.Current!).Services;

        var dialogService = services?.GetService<IDialogService>();
        var usuarioService = services?.GetService<UsuarioService>();
        var emailService = services?.GetService<IEmailService>();

        if(dialogService == null || usuarioService == null || emailService == null)
        {
            throw new Exception("No se pudieron obtener los servicios necesarios.");
        }

        BindingContext = new ValidarCodigoViewModel(dialogService,usuarioService,emailService);
    }

    private async void VolverLogin_Tapped(object sender,TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("//Login");
    }
}