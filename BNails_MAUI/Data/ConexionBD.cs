using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Data
{
    public class ConexionBD
    {
        private const string conexionBD = "server=192.168.0.62;port=3306;database=bnails;user=root;password=root";

        public static MySqlConnection GetConexionBD()
        {
            try
            {
                return new MySqlConnection(conexionBD);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
                throw;
            }
        }
    }
}
