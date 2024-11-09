using Crud_Angular.Models;
using System.Data;
using System.Data.SqlClient;
namespace Crud_Angular.Data
{
    public class EstudianteData
    {

        private readonly string conexion;
        public EstudianteData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }
        public async Task<List<Estudiante>> Lista()
        {
            List<Estudiante> lista = new List<Estudiante>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_ListaEstudiante", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Estudiante
                        {
                            EstudianteID = Convert.ToInt32(reader["EstudianteID"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            FechaNacimiento = reader["FechaNacimiento"].ToString(),
                            Genero = reader["Genero"]?.ToString()[0] ?? 'N',
                            CorreoElectronico = reader["CorreoElectronico"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            FechaIngreso = reader["FechaIngreso"].ToString()
                        });
                    }
                }

            }
            return lista;
        }

        public async Task<Estudiante> Obtener(int Id)
        {
            Estudiante objeto = new Estudiante();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_ObtenerEstudiante", con);
                cmd.Parameters.AddWithValue("@estudianteID", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Estudiante
                        {
                            EstudianteID = Convert.ToInt32(reader["EstudianteID"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            FechaNacimiento = reader["FechaNacimiento"].ToString(),
                            Genero = reader["Genero"]?.ToString()[0] ?? 'N',
                            CorreoElectronico = reader["CorreoElectronico"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            FechaIngreso = reader["FechaIngreso"].ToString()
                        };
                    }
                }

            }
            return objeto;
        }

        public async Task<bool> Crear(Estudiante objeto)
        {
            bool respuesta = false;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_CrearEstudiante", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@FechaNacimiento", objeto.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Genero", objeto.Genero);
                cmd.Parameters.AddWithValue("@CorreoElectronico", objeto.CorreoElectronico);
                cmd.Parameters.AddWithValue("@Telefono", objeto.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", objeto.Direccion);
                cmd.Parameters.AddWithValue("@FechaIngreso", objeto.FechaIngreso);

                try
                {
                    await con.OpenAsync();
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        // Asignar el EstudianteID generado al objeto
                        objeto.EstudianteID = Convert.ToInt32(reader["EstudianteID"]);
                        respuesta = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    respuesta = false;
                }
            }

            return respuesta;
        }


        public async Task<bool> Editar(Estudiante objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_EditarEstudiante", con);
                cmd.Parameters.AddWithValue("@estudianteID", objeto.EstudianteID);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@FechaNacimiento", objeto.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Genero", objeto.Genero);
                cmd.Parameters.AddWithValue("@CorreoElectronico", objeto.CorreoElectronico);
                cmd.Parameters.AddWithValue("@Telefono", objeto.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", objeto.Direccion);
                cmd.Parameters.AddWithValue("@FechaIngreso", objeto.FechaIngreso);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    // Eliminamos la segunda llamada a con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    respuesta = false;
                }

                return respuesta;
            }
        }


        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_EliminarEstudiante", con);
                cmd.Parameters.AddWithValue("@estudianteID", id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al intentar eliminar el estudiante: " + ex.Message);
                    respuesta = false;
                }

                return respuesta;
            }
        }


    }
}
