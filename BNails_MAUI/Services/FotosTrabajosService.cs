using BNails_MAUI.Models;
using BNails_MAUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Services
{
    public class FotosTrabajosService
    {
        public readonly FotosTrabajosRepository _fotosTrabajosRepo;

        public FotosTrabajosService(FotosTrabajosRepository fotosTrabajosRepo)
        {
            _fotosTrabajosRepo = fotosTrabajosRepo;
        }

        public List<FotoTrabajo> ObtenerFotos(int tipoTrabajoId)
        {
            return _fotosTrabajosRepo.ObtenerFotosPorTipoTrabajo(tipoTrabajoId);
        }

        public void AgregarFoto(FotoTrabajo foto)
        {
            _fotosTrabajosRepo.AgregarFoto(foto);
        }
    }
}
