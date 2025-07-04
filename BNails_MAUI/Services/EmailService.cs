using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguracionRepository _configuracionRepository;
        private readonly UsuarioService _usuarioService;

        public EmailService(IConfiguracionRepository configuracionRepository, UsuarioService usuarioService)
        {
            _configuracionRepository = configuracionRepository;
            _usuarioService = usuarioService;
        }

        public string GenerarCodigoVerificacion()
        {
            Random codigoValidacion = new Random();
            return codigoValidacion.Next(1000,9999).ToString();
        }

        public async Task<bool> EnviarEmailCodigoVerificacion(string email,string codigoVerificacion,string nombreUsuario)
        {
            try
            {
                var Existeusuario = _usuarioService.ExisteUsuarioPorEmail(email);
                if(Existeusuario == null) return false;

                var expiraCodigo = DateTime.Now.AddMinutes(10);

                bool codigoGuardado = _usuarioService.GuardarCodigoVerificacion(email, codigoVerificacion, expiraCodigo);

                if(!codigoGuardado)
                    return false;

                // Obtenés configuración SMTP
                Configuracion? config = _configuracionRepository.GetConfiguracion();
                if(config == null) return false;

                // Armás el mail
                MailMessage mail = new()
                {
                    From = new MailAddress(config.SMTPFrom,config.SMTPFromNombre),
                    Subject = $"Código de verificación - {config.SMTPFromNombre}",
                    IsBodyHtml = true,
                    Body = $"<p>Hola <strong>{nombreUsuario}</strong>,</p>" +
                           "<p>Tu código de verificación es:</p>" +
                           $"<h2>{codigoVerificacion}</h2>" +
                           "<p>Usalo para continuar con el proceso de recuperación de contraseña.</p>" +
                           "<p>Este código es válido por 10 minutos.</p>" +
                           "<br><p>Gracias por usar nuestra app.</p>"
                };

                mail.To.Add(email);

                using SmtpClient smtp = new(config.SMTPServer,Convert.ToInt32(config.SMTPPort))
                {
                    Credentials = new NetworkCredential(config.SMTPUser,config.SMTPPwd),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mail);
                return true;

            } catch(Exception)
            {
                return false;
            }
        }
    }
}
