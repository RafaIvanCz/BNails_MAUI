namespace BNails_MAUI.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void Registro_Tapped(object sender,TappedEventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(Registro));
    }

    private void OnShowPasswordCheckBox(object sender,CheckedChangedEventArgs e)
    {
        txtPasswordLogin.IsPassword = !e.Value;
    }
}