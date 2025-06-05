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
}