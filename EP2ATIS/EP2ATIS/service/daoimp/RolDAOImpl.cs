using EP2ATIS.model;
using EP2ATIS.service.dao;
using EP2ATIS.utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP2ATIS.service.daoimp
{
    class RolDAOImpl : IRolDAO
    {
        public int buscarIdPorNombre(string nombre)
        {
            try
            {
                string consultaSQL;
                int id_rol;

                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT id_rol FROM Rol WHERE nombre = @Nombre";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Nombre", nombre);
                SqlDataReader registro = sqlCmd.ExecuteReader();

                registro.Read();

                id_rol = int.Parse(registro["id_rol"].ToString());

                conexion.CerraConexion();

                return id_rol;
            }
            catch
            {
                return 0;
            }
        }

        public string buscarNombreporID(int id)
        {
            try
            {
                string consultaSQL, nombre;
     
                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT nombre FROM Rol WHERE id_rol = @Id_rol";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Id_rol", id);
                SqlDataReader registro = sqlCmd.ExecuteReader();

                registro.Read();

                nombre = registro["nombre"].ToString();

                conexion.CerraConexion();

                return nombre;
            }
            catch
            {
                return null;
            }
        }

        public Rol buscarporId(int id)
        {
            try
            {
                string consultaSQL;

                Rol rol = new Rol();
                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT * FROM Rol WHERE id_rol = @Id_rol";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Id_rol", id);
                SqlDataReader registro = sqlCmd.ExecuteReader();

                registro.Read();

                rol.IdRol = int.Parse(registro["id_rol"].ToString());
                rol.Nombre = registro["nombre"].ToString();

                conexion.CerraConexion();

                return rol;
            }
            catch
            {
                return null;
            }
        }
    }
}
