using BNails_MAUI.Data;
using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public bool GuardarUsuario(Usuario usuario)
        {
            using var connection = new MySqlConnection(ConexionBD.ObtenerConexionBD());
            connection.Open();

            string query = "INSERT INTO usuarios (nombre, email, password) VALUES (@Nombre, @Email, @Password)";
            using var command = new MySqlCommand(query,connection);
            command.Parameters.AddWithValue("@nombre",usuario.Nombre);
            command.Parameters.AddWithValue("@email",usuario.Email);
            command.Parameters.AddWithValue("@password",usuario.Password);

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }

        public bool ExisteUsuarioPorEmail(string email)
        {
            using var connection = new MySqlConnection(ConexionBD.ObtenerConexionBD());
            connection.Open();

            string query = "SELECT COUNT(*) FROM usuarios WHERE email = @Email";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            var resultado = Convert.ToInt32(command.ExecuteScalar());

            return resultado > 0;
        }

        public Usuario? ObtenerUsuarioPorEmail(string email)
        {
            using var connection = new MySqlConnection(ConexionBD.ObtenerConexionBD());
            connection.Open();

            string query = "SELECT * FROM usuarios WHERE email = @Email";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            using var reader = command.ExecuteReader();

            if(reader.Read())
            {
                int idxCodigo = reader.GetOrdinal("codigo_recuperacion");
                int idxFecha = reader.GetOrdinal("codigo_fecha_exp");

                return new Usuario
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    Email = reader.GetString("email"),
                    Password = reader.GetString("password"),
                    CodigoRecuperacion = reader.IsDBNull(idxCodigo) ? null : reader.GetString(idxCodigo),
                    CodigoRecuExpira = reader.IsDBNull(idxFecha) ? null : reader.GetDateTime(idxFecha).ToLocalTime()
                };
            }

            return null;
        }

        public bool ActualizarPwd(Usuario usuario)
        {
            using var connection = new MySqlConnection(ConexionBD.ObtenerConexionBD());
            connection.Open();

            string query = "UPDATE usuarios SET password = @Password WHERE email = @Email";
            using var command = new MySqlCommand(query,connection);
            command.Parameters.AddWithValue("@Password",usuario.Password);
            command.Parameters.AddWithValue("@Email",usuario.Email);

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }

        public bool GuardarCodigoVerificacion(string email,string codigo,DateTime fechaExpiracion)
        {
            using var connection = new MySqlConnection(ConexionBD.ObtenerConexionBD());
            connection.Open();

            string query = @"UPDATE usuarios 
                     SET codigo_recuperacion = @Codigo, 
                         codigo_fecha_exp = @Expira
                     WHERE email = @Email";

            using var command = new MySqlCommand(query,connection);
            command.Parameters.AddWithValue("@Codigo",codigo);
            command.Parameters.AddWithValue("@Expira",fechaExpiracion);
            command.Parameters.AddWithValue("@Email",email);

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }
    }
}
