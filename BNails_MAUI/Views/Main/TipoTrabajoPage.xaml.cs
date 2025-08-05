using BNails_MAUI.ViewModels;

namespace BNails_MAUI.Views.Main
{
    public partial class TipoTrabajoPage : ContentPage
    {
        private int _tipoTrabajoId;
        public TipoTrabajoPage(int tipoTrabajoId)
        {
            InitializeComponent();
            BindingContext = new TipoTrabajosViewModel(tipoTrabajoId);
        }
    }
}