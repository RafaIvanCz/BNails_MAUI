namespace BNails_MAUI.Views;

public partial class Registro : ContentPage
{
	public Registro()
	{
		InitializeComponent();
	}

    private async void VolverLogin_Tapped(object sender,TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }

    private void OnShowPasswordCheckBox_Registro(object sender,CheckedChangedEventArgs e)
    {
        txtPasswordRegistro.IsPassword = !e.Value;
        txtRePassword.IsPassword = !e.Value;
    }
}