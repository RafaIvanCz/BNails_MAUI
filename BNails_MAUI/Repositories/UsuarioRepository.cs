using BNails_MAUI.Data;
using BNails_MAUI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Repositories
{
    public class UsuarioRepository
    {
        public bool GuardarUsuario(Usuario usuario)
        {
            using var connection = new MySqlConnection(ConexionBD.ObtenerConexionBD());
            connection.Open();

            string query = "INSERT INTO usuarios (nombre, email, password) VALUES (@Nombre, @Email, @Password)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@nombre", usuario.Nombre);
            command.Parameters.AddWithValue("@email", usuario.Email);
            command.Parameters.AddWithValue("@password", usuario.Password);

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }
    }
}
