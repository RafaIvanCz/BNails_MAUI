using BNails_MAUI.Interfaces;
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
            if(Application.Current?.MainPage != null)
                await Application.Current.MainPage.DisplayAlert(titulo, mensaje, botonTexto);
        }
    }
}
