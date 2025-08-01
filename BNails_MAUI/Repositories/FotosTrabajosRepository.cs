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
    public class FotosTrabajosRepository
    {
        private readonly ConexionBD _conexion;

        public FotosTrabajosRepository(ConexionBD conexion)
        {
            _conexion = conexion;
        }

        public List<FotoTrabajo> ObtenerFotosPorTipoTrabajo(int tipoTrabajoId)
        {
            var fotos = new List<FotoTrabajo>();

            using(var conn = ConexionBD.GetConexionBD())
            {
                conn.Open();
                string query = "SELECT * FROM fotos_trabajos WHERE tipo_trabajo_id = @tipoId";
                using(var cmd = new MySqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@tipoId",tipoTrabajoId);
                    using(var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            fotos.Add(new FotoTrabajo
                            {
                                Id = reader.GetInt32("id"),
                                TipoTrabajoId = reader.GetInt32("tipo_trabajo_id"),
                                RutaImagen = reader.GetString("ruta_imagen"),
                                Descripcion = reader.GetString("descripcion")
                            });
                        }
                    }
                }
            }

            return fotos;
        }

        public void AgregarFoto(FotoTrabajo foto)
        {
            using(var conn = ConexionBD.GetConexionBD())
            {
                conn.Open();
                string query = "INSERT INTO fotos_trabajos (tipo_trabajo_id, ruta_imagen, descripcion) VALUES (@tipoId, @ruta, @desc)";
                using(var cmd = new MySqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@tipoId",foto.TipoTrabajoId);
                    cmd.Parameters.AddWithValue("@ruta",foto.RutaImagen);
                    cmd.Parameters.AddWithValue("@desc",foto.Descripcion ?? (object) DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
