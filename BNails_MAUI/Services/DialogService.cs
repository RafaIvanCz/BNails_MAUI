using BNails_MAUI.Interfaces;
using BNails_MAUI.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Services
{
    public class DialogService : IDialogService
    {
        public async Task MostrarAlertaAsync(string titulo, string mensaje, string botonTexto = "Aceptar")
        {
            var page = Application.Current?.Windows[0].Page;
            if(page != null)
                await page.DisplayAlert(titulo, mensaje, botonTexto);
        }
    }
}
