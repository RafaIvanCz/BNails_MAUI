using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? CodigoRecuperacion { get; set; }
        public DateTime? CodigoRecuExpiro { get; set; }
        public DateTime? FechaPrimerIngreso { get; set; }
    }
}
