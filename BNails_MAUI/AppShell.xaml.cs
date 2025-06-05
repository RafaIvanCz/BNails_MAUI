using BNails_MAUI.Views;

namespace BNails_MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(Registro), typeof(Registro));
        }
    }
}
