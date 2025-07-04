using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Models
{
    public class Configuracion
    {
        public int Id { get; set; }

        public string SMTPServer { get; set; } = string.Empty;

        public int SMTPPort { get; set; }

        public string SMTPUser { get; set; } = string.Empty;

        public string SMTPPwd { get; set; } = string.Empty;

        public string SMTPFrom { get; set; } = string.Empty;

        public string SMTPFromNombre { get; set; } = string.Empty;
    }
}
