using BNails_MAUI.Interfaces;
using BNails_MAUI.ViewModels;
namespace BNails_MAUI.Views;

public partial class Registro : ContentPage
{
	public Registro()
	{
		InitializeComponent();
        BindingContext = new RegistroViewModel(((App) App.Current).Services.GetService<IDialogService>());
    }

    private async void VolverLogin_Tapped(object sender,TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }

    private void OnShowPasswordCheckBox_Registro(object sender,CheckedChangedEventArgs e)
    {
        txtPasswordRegistro.IsPassword = !e.Value;
        txtRePasswordRegistro.IsPassword = !e.Value;
    }
}