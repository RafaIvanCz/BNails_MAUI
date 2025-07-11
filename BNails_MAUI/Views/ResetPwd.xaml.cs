using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Services;
using BNails_MAUI.ViewModels;

namespace BNails_MAUI.Views;

public partial class ResetPwd : ContentPage
{
    public ResetPwd()
    {
        InitializeComponent();

        var services = ((App) App.Current!).Services;

        var dialogService = services?.GetService<IDialogService>();
        var usuarioService = services?.GetService<UsuarioService>();

        if(dialogService == null || usuarioService == null)
            throw new Exception("No se pudieron obtener los servicios necesarios.");

        BindingContext = new ResetPwdViewModel(dialogService,usuarioService);
        loadingPopup.BindingContext = BindingContext;
    }

    private async void VolverLogin_Tapped(object sender,TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//Login");
    }

    private void OnShowPasswordCheckBox_ResetPwd(object sender,CheckedChangedEventArgs e)
    {
        txtPasswordResetPwd.IsPassword = !e.Value;
        txtRePasswordResetPwd.IsPassword = !e.Value;
    }
}
