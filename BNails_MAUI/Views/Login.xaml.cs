using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Services;
using BNails_MAUI.ViewModels;

namespace BNails_MAUI.Views;

public partial class Login : ContentPage
{
	public Login()
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

        BindingContext = new LoginViewModel(dialogService, usuarioRepo, usuarioService);
        loadingPopup.BindingContext = BindingContext;
    }

    private async void Registro_Tapped(object sender,TappedEventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(Registro));
    }

    private void OnShowPasswordCheckBox(object sender,CheckedChangedEventArgs e)
    {
        txtPasswordLogin.IsPassword = !e.Value;
    }

    private async void RecuperarPwd_Tapped(object sender,TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RecuperarPwd));
    }

    private async void HomePage_Tapped(object sender,TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Views.Main.HomePage));
    }

    private void OnEntryFocused(object sender,FocusEventArgs e)
    {
        if(sender is Entry entry)
        {
            if(entry == txtPasswordLogin)
                PasswordBorder.Stroke = Color.FromArgb("#7B1FA2");
            else
                EmailBorder.Stroke = Color.FromArgb("#7B1FA2");
        }
    }

    private void OnEntryUnfocused(object sender,FocusEventArgs e)
    {
        if(sender is Entry entry)
        {
            if(entry == txtPasswordLogin)
                PasswordBorder.Stroke = Color.FromArgb("#9C27B0");
            else
                EmailBorder.Stroke = Color.FromArgb("#9C27B0");
        }
    }
}