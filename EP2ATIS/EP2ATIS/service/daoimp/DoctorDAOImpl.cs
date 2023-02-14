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
    class DoctorDAOImpl : IDoctorDAO
    {
        public List<Doctor> listar()
        {
            try
            {
                string consultaSQL;

                List<Doctor> doctores = new List<Doctor>();
                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT * FROM Doctor";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                SqlDataReader registro = sqlCmd.ExecuteReader();

                while (registro.Read())
                {
                    Doctor doctor = new Doctor();

                    doctor.IdDoctor = int.Parse(registro["id_doctor"].ToString());
                    doctor.Nombres = registro["nombres"].ToString();
                    doctor.Apellidos = registro["apellidos"].ToString();
                    doctor.Genero = registro["genero"].ToString();
                    doctor.Experiencia = int.Parse(registro["anios_experiencia"].ToString());
                    doctor.Telefono = registro["telefono"].ToString();
                    doctor.Direccion = registro["direccion"].ToString();
                    doctor.IdEspecialidad = int.Parse(registro["id_especialidad"].ToString());
                    doctor.IdUsuario = int.Parse(registro["id_usuario"].ToString());

                    doctores.Add(doctor);
                }

                conexion.CerraConexion();

                if (doctores.Count == 0)
                    throw new Exception();

                return doctores;
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

                consultaSQL = "SELECT * FROM Doctor WHERE id_doctor = @IdDoctor";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@IdDoctor", id);

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
