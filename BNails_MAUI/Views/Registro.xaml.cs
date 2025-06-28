using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Services;
using BNails_MAUI.ViewModels;

namespace BNails_MAUI.Views;

public partial class Registro : ContentPage
{
    public Registro()
    {
        InitializeComponent();
        var services = ((App) App.Current!).Services;

        var dialogService = services?.GetService<IDialogService>();
        var usuarioRepo = services?.GetService<IUsuarioRepository>();
        var usuarioService = services?.GetService<UsuarioService>();

        if(dialogService == null || usuarioRepo == null || usuarioService == null)
        {
            throw new Exception("No se pudieron obtener los servicios necesarios.");
        }

        BindingContext = new RegistroViewModel(dialogService,usuarioRepo,usuarioService);
    }

    private async void VolverLogin_Tapped(object sender,TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("//Login");
    }

    private void OnShowPasswordCheckBox_Registro(object sender,CheckedChangedEventArgs e)
    {
        txtPasswordRegistro.IsPassword = !e.Value;
        txtRePasswordRegistro.IsPassword = !e.Value;
    }
}