using BNails_MAUI.Data;
using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Repositories
{
    public class ConfiguracionRepository : IConfiguracionRepository
    {
        public Configuracion? GetConfiguracion()
        {
            using var connection = ConexionBD.GetConexionBD();
            connection.Open();

            string query = "SELECT * FROM configuraciones LIMIT 1"; // Asumimos una sola fila
            using var command = new MySqlCommand(query,connection);

            using var reader = command.ExecuteReader();

            if(reader.Read())
            {
                return new Configuracion
                {
                    Id = reader.GetInt32("id"),
                    SMTPServer = reader.GetString("smtp_server"),
                    SMTPPort = reader.GetInt32("smtp_port"),
                    SMTPUser = reader.GetString("smtp_user"),
                    SMTPPwd = reader.GetString("smtp_password"),
                    SMTPFrom = reader.GetString("smtp_from"),
                    SMTPFromNombre = reader.GetString("smtp_from_nombre")
                };
            }

            return null;
        }

        public bool GuardarConfiguracion(Configuracion config)
        {
            using var connection = ConexionBD.GetConexionBD();
            connection.Open();

            string query = @"INSERT INTO configuraciones 
                    (SMTPServer, SMTPPort, SMTPUser, SMTPPwd, SMTPFrom, SMTPFromNombre)
                     VALUES (@server, @port, @user, @pwd, @from, @fromNombre)";

            using var cmd = new MySqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@server",config.SMTPServer);
            cmd.Parameters.AddWithValue("@port",config.SMTPPort);
            cmd.Parameters.AddWithValue("@user",config.SMTPUser);
            cmd.Parameters.AddWithValue("@pwd",config.SMTPPwd); // ya encriptado
            cmd.Parameters.AddWithValue("@from",config.SMTPFrom);
            cmd.Parameters.AddWithValue("@fromNombre",config.SMTPFromNombre);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
