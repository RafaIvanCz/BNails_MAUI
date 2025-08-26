using BNails_MAUI.ViewModels;

namespace BNails_MAUI.Views.Main
{
    public partial class TipoTrabajoPage : ContentPage
    {
        private int _tipoTrabajoId;
        public TipoTrabajoPage(TipoTrabajosViewModel tipoTrabajoVM)
        {
            InitializeComponent();
            BindingContext = tipoTrabajoVM;
        }
    }
}