using BNails_MAUI.Controllers;

namespace BNails_MAUI.Views.Main;

public partial class ListaTipoTrabajos : ContentPage
{
    private readonly FotosTrabajosController _fotosTrabajosController;
    private readonly int _tipoTrabajoId;

    public ListaTipoTrabajos(FotosTrabajosController fotosTrabajosController,int tipoTrabajoId)
    {
        InitializeComponent();
        _fotosTrabajosController = fotosTrabajosController;
        _tipoTrabajoId = tipoTrabajoId;

        CargarFotos();
    }

    private void CargarFotos()
    {
        var fotos = _fotosTrabajosController.ObtenerFotosPorTipo(_tipoTrabajoId);
        FotosCollectionView.ItemsSource = fotos;
    }
}