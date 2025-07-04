using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Helpers
{
    public static class SeguridadHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerificarPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public static string Encriptar(string textoPlano)
        {
            using var aes = Aes.Create();
            // Clave y vector deben guardarse seguros y ser constantes
            aes.Key = Encoding.UTF8.GetBytes("clave-secreta-de-32bytes!!"); // 32 bytes para AES-256
            aes.IV = Encoding.UTF8.GetBytes("vector-16bytes!!"); // 16 bytes

            using var encryptor = aes.CreateEncryptor();
            var buffer = Encoding.UTF8.GetBytes(textoPlano);
            var resultado = encryptor.TransformFinalBlock(buffer,0,buffer.Length);
            return Convert.ToBase64String(resultado);
        }

    }
}
