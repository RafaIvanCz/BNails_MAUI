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
        private const string conexionBD = "server=localhost;port=3306;database=bnails;user=root;password=root";

        public static MySqlConnection ObtenerConexionBD()
        {
            var conexion = new MySqlConnection(conexionBD);
            conexion.Open();
            return conexion;
        }
    }
}
