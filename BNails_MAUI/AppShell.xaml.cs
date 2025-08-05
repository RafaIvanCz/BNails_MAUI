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
            Routing.RegisterRoute(nameof(RecuperarPwd),typeof(RecuperarPwd));
            Routing.RegisterRoute(nameof(ResetPwd), typeof(ResetPwd));
            Routing.RegisterRoute(nameof(ValidarCodigo),typeof(ValidarCodigo));
            Routing.RegisterRoute(nameof(Views.Main.HomePage),typeof(Views.Main.HomePage));
        }
    }
}
