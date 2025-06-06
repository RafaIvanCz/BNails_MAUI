namespace BNails_MAUI.Views;

public partial class Registro : ContentPage
{
	public Registro()
	{
		InitializeComponent();
	}

    private async void VolverAlLogin_btn(object sender,EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }

    private void OnShowPasswordCheckBox_Registro(object sender,CheckedChangedEventArgs e)
    {
        txtPasswordRegistro.IsPassword = !e.Value;
        txtRePassword.IsPassword = !e.Value;
    }
}