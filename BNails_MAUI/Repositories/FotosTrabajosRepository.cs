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
    public class FotosTrabajosRepository : IFotosTrabajosRepository
    {
        public bool AgregarFotoTrabajo(FotoTrabajo foto)
        {
            using var conn = ConexionBD.GetConexionBD();
            conn.Open();

            string query = "INSERT INTO fotos_trabajos (tipo_trabajo_id, ruta_imagen, descripcion) VALUES (@TipoTrabajoId, @RutaImagen, @Descripcion)";
            using var cmd = new MySqlCommand(query,conn);
            cmd.Parameters.AddWithValue("@TipoTrabajoId",foto.TipoTrabajoId);
            cmd.Parameters.AddWithValue("@RutaImagen",foto.RutaImagen);
            cmd.Parameters.AddWithValue("@Descripcion",foto.Descripcion ?? string.Empty);

            return cmd.ExecuteNonQuery() > 0;
        }

        public List<FotoTrabajo> ObtenerFotosPorTipoTrabajo(int tipoTrabajoId)
        {
            var fotos = new List<FotoTrabajo>();

            using var conn = ConexionBD.GetConexionBD();
            conn.Open();

            string query = "SELECT id, tipo_trabajo_id, ruta_imagen, descripcion FROM fotos_trabajos WHERE tipo_trabajo_id = @TipoTrabajoId";
            using var cmd = new MySqlCommand(query,conn);
            cmd.Parameters.AddWithValue("@TipoTrabajoId",tipoTrabajoId);

            using var reader = cmd.ExecuteReader();
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

            return fotos;
        }
    }
}
