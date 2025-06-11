namespace BNails_MAUI.Views;

public partial class ResetPwd : ContentPage
{
	public ResetPwd()
	{
		InitializeComponent();
	}

    private async void VolverLogin_Tapped(object sender,TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}