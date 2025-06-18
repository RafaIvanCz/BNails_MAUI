using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Interfaces
{
    public interface IDialogService
    {
        Task MostrarAlertaAsync(string titulo,string mensaje,string botonTexto = "Aceptar");
    }
}
