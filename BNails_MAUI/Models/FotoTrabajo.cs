using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Models
{
    public class FotoTrabajo
    {
        public int Id { get; set; }
        public int TipoTrabajoId { get; set; }
        public string? RutaImagen { get; set; }
        public string? Descripcion { get; set; }
    }
}
