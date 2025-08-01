using BNails_MAUI.Models;
using BNails_MAUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Controllers
{
    public class FotosTrabajosController
    {
        private readonly FotosTrabajosService _fotosTrabajosService;

        public FotosTrabajosController(FotosTrabajosService fotosTrabajosService)
        {
            _fotosTrabajosService = fotosTrabajosService;
        }

        public List<FotoTrabajo> ObtenerFotosPorTipo(int tipoTrabajoId)
        {
            return _fotosTrabajosService.ObtenerFotos(tipoTrabajoId);
        }

        public void SubirFoto(int tipoTrabajoId,string rutaImagen,string descripcion = null)
        {
            var foto = new FotoTrabajo
            {
                TipoTrabajoId = tipoTrabajoId,
                RutaImagen = rutaImagen,
                Descripcion = descripcion
            };

            _fotosTrabajosService.AgregarFoto(foto);
        }
    }
}
