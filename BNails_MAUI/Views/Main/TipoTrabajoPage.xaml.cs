using BNails_MAUI.ViewModels;

namespace BNails_MAUI.Views.Main
{
    public partial class TipoTrabajoPage : ContentPage
    {
        public TipoTrabajoPage(TipoTrabajosViewModel tipoTrabajoVM)
        {
            InitializeComponent();
            BindingContext = tipoTrabajoVM;
        }
    }
}