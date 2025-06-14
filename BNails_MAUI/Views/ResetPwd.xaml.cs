using BNails_MAUI.ViewModels;

namespace BNails_MAUI.Views;

public partial class ResetPwd : ContentPage
{
	public ResetPwd()
	{
		InitializeComponent();
		BindingContext = new RegistroViewModel();
    }

    private async void VolverLogin_Tapped(object sender,TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}