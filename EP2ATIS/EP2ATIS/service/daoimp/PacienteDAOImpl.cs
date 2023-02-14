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
    class PacienteDAOImpl : IPacienteDAO
    {
        public bool insertar(Paciente paciente)
        {
            try
            {
                string consultaSQL;
                int resultado;

                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "INSERT INTO Paciente (nombres, apellidos, genero, fecha_nacimiento, direccion, " +
                    "telefono, dui, tipo_sangre, id_usuario) VALUES (@Nombres, @Apellidos, @Genero, @Nacimiento, " +
                    "@Direccion, @Telefono, @DUI, @TipoSangre, @IdUsuario)";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Nombres", paciente.nombre);
                sqlCmd.Parameters.AddWithValue("@Apellidos", paciente.apellido);
                sqlCmd.Parameters.AddWithValue("@Genero", paciente.genero);
                sqlCmd.Parameters.AddWithValue("@Nacimiento", paciente.fecha_Nacimiento);
                sqlCmd.Parameters.AddWithValue("@Direccion", paciente.direccion);
                sqlCmd.Parameters.AddWithValue("@Telefono", paciente.telefono);
                sqlCmd.Parameters.AddWithValue("@DUI", paciente.dui);
                sqlCmd.Parameters.AddWithValue("@TipoSangre", paciente.tipo_Sangre);
                sqlCmd.Parameters.AddWithValue("@IdUsuario", paciente.id_Usuario);

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

        public List<Paciente> listar()
        {
            try
            {
                string consultaSQL;

                List<Paciente> pacientes = new List<Paciente>();
                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT * FROM Paciente";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                SqlDataReader registro = sqlCmd.ExecuteReader();

                while (registro.Read())
                {
                    Paciente paciente = new Paciente();

                    paciente.id_Paciente = int.Parse(registro["id_paciente"].ToString());
                    paciente.nombre = registro["nombres"].ToString();
                    paciente.apellido = registro["apellidos"].ToString();
                    paciente.genero = registro["genero"].ToString();
                    paciente.fecha_Nacimiento = DateTime.Parse(registro["fecha_nacimiento"].ToString());
                    paciente.direccion = registro["direccion"].ToString();
                    paciente.telefono = registro["telefono"].ToString();
                    paciente.telefono = registro["dui"].ToString();
                    paciente.telefono = registro["tipo_sangre"].ToString();
                    paciente.id_Usuario = int.Parse(registro["id_usuario"].ToString());

                    pacientes.Add(paciente);
                }

                conexion.CerraConexion();

                if (pacientes.Count == 0)
                    throw new Exception();

                return pacientes;
            }
            catch
            {
                return null;
            }
        }

        public string listarNombrePorId(int id)
        {
            try
            {
                string consultaSQL, nombre;

                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT * FROM Paciente WHERE id_paciente = @IdPaciente";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@IdPaciente", id);

                SqlDataReader registro = sqlCmd.ExecuteReader();

                registro.Read();

                nombre = registro["nombres"].ToString();

                conexion.CerraConexion();

                return nombre;
            }
            catch
            {
                return null;
            }
        }
    }
}
