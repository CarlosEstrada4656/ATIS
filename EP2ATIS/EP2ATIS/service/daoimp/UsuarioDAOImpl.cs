using EP2ATIS.model;
using EP2ATIS.service.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using EP2ATIS.utilities;
using System.Data;

namespace EP2ATIS.service.daoimp
{
    class UsuarioDAOImpl : IUsuarioDAO
    {
        public bool eliminar(int id)
        {
            try
            {
                string consultaSQL;
                int resultado;

                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "DELETE FROM Usuario WHERE id_usuario = @IdUsuario";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@IdUsuario", id);
                resultado = sqlCmd.ExecuteNonQuery();

                conexion.CerraConexion();

                if (resultado > 0)
                    return true;

                else
                    throw new Exception();
            }
            catch
            {
                return false;
            }
        }

        public int insertar(Usuario usuario)
        {
            try
            {
                string consultaSQL;
                int id;

                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "INSERT INTO Usuario (correo, clave, id_rol) VALUES " +
                    "(@Correo, ENCRYPTBYPASSPHRASE('@clinicaEP2DS21', @Clave), @Id_rol);" +
                    "SELECT SCOPE_IDENTITY() AS id";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                sqlCmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                sqlCmd.Parameters.AddWithValue("@Id_rol", usuario.IdRol);

                SqlDataReader registro = sqlCmd.ExecuteReader();

                registro.Read();

                id = int.Parse(registro["id"].ToString());

                conexion.CerraConexion();

                return id;

            }
            catch
            {
                return 0;
            }
        }

        public bool login(string correo, string clave)
        {
            Conexiondb conexion = new Conexiondb();

            try
            {
                string consultaSQL;
                bool result = false;

                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT * FROM Usuario WHERE correo = @Correo AND " +
                    "cast(DECRYPTBYPASSPHRASE('@clinicaEP2DS21', clave) as varchar) = @Clave";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Correo", correo);
                sqlCmd.Parameters.AddWithValue("@Clave", clave);
                SqlDataReader registro = sqlCmd.ExecuteReader();

                if (registro.Read())
                {
                    result = true;

                    UsuarioCache.Id = int.Parse(registro["id_usuario"].ToString());
                    UsuarioCache.Usuario = registro["correo"].ToString();

                    int idRol = int.Parse(registro["id_rol"].ToString());

                    UsuarioCache.IdRol = idRol;
                }

                conexion.CerraConexion();

                return result;
            }
            catch
            {
                conexion.CerraConexion();
                return false;
            }
        }

        //
        public int idPaciente(string correo, string clave)
        {
            Conexiondb conexion = new Conexiondb();
            int result = 0;
            try
            {
                string consultaSQL;
                string query;

                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT id_usuario FROM Usuario WHERE correo = @Correo AND " +
                    "cast(DECRYPTBYPASSPHRASE('@clinicaEP2DS21', clave) as varchar) = @Clave";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Correo", correo);
                sqlCmd.Parameters.AddWithValue("@Clave", clave);
                SqlDataReader registro = sqlCmd.ExecuteReader();

                if (registro.Read())
                {
                    result = int.Parse(registro["id_usuario"].ToString()); 

                }

                conexion.CerraConexion();
                SqlConnection conDB2 = conexion.Conectar();


                query = "SELECT id_paciente from Paciente where id_usuario = @id_usuario";
                SqlCommand sqlCmd2 = new SqlCommand(query, conDB2);
                sqlCmd2.CommandType = CommandType.Text;
                
                sqlCmd2.Parameters.AddWithValue("@id_usuario", result);
                
                SqlDataReader registro2 = sqlCmd2.ExecuteReader();

                if (registro2.Read())
                {
                    result = int.Parse(registro2["id_paciente"].ToString());

                }
                
                conexion.CerraConexion();

                return result;
            }
            catch(Exception ex)
            {
                conexion.CerraConexion();
                return result;
                
            }
        }



    }
}
