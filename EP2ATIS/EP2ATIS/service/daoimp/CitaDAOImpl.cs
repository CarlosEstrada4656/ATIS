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
    class CitaDAOImpl : ICitaDAO
    {
        public bool confirmar(int idCita)
        {
            try
            {
                string consultaSQL;
                int resultado;

                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "UPDATE Cita SET confirmada = @Confirmada WHERE Id_cita = @IdCita";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Confirmada", 1);
                sqlCmd.Parameters.AddWithValue("@IdCita", idCita);
              
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

        public bool editar(Cita cita)
        {
            try
            {
                string consultaSQL;
                int resultado;

                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "UPDATE Cita SET fecha = @Fecha, estado = @Estado, confirmada = @Confirmada, " +
                    "id_horario = @Id_horario, id_doctor = @id_doctor, id_paciente = @Id_paciente WHERE id_cita = @Id_cita";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Fecha", cita.Fecha);
                sqlCmd.Parameters.AddWithValue("@Estado", cita.Estado);
                sqlCmd.Parameters.AddWithValue("@Confirmada", cita.Confirmada);
                sqlCmd.Parameters.AddWithValue("@id_horario", cita.IdHorario);
                sqlCmd.Parameters.AddWithValue("@id_doctor", cita.IdDoctor);
                sqlCmd.Parameters.AddWithValue("@Id_paciente", cita.IdPaciente);

                sqlCmd.Parameters.AddWithValue("@Id_cita", cita.IdCita);

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

        public bool eliminar(int id)
        {
            try
            {
                string consultaSQL;
                int resultado;

                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "DELETE FROM Cita WHERE id_cita = @IdCita";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@IdCita", id);
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

        public bool insetar(Cita cita)
        {
            try
            {
                string consultaSQL;
                int resultado;

                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "INSERT INTO Cita (fecha, estado, confirmada, id_horario, id_doctor, id_paciente) VALUES " +
                    "(@Fecha, @Estado, @Confirmada, @Id_horario, @Id_doctor, @Id_paciente)";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Fecha", cita.Fecha);
                sqlCmd.Parameters.AddWithValue("@Estado", cita.Estado);
                sqlCmd.Parameters.AddWithValue("@Confirmada", cita.Confirmada);
                sqlCmd.Parameters.AddWithValue("@Id_horario", cita.IdHorario);
                sqlCmd.Parameters.AddWithValue("@Id_doctor", cita.IdDoctor);
                sqlCmd.Parameters.AddWithValue("@Id_paciente", cita.IdPaciente);

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

        public List<Cita> listar()
        {
            try
            {
                string consultaSQL;

                List<Cita> citas = new List<Cita>();
                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT * FROM Cita";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                SqlDataReader registro = sqlCmd.ExecuteReader();

                while (registro.Read())
                {
                    Cita cita = new Cita();

                    cita.IdCita = int.Parse(registro["id_cita"].ToString());
                    cita.Fecha = DateTime.Parse(registro["fecha"].ToString());
                    cita.Estado = registro["estado"].ToString() == "False" ? 0 : 1;
                    cita.Confirmada = registro["confirmada"].ToString() == "False" ? 0 : 1;
                    cita.IdHorario = int.Parse(registro["id_horario"].ToString());

                    try
                    {
                        cita.IdPaciente = int.Parse(registro["id_paciente"].ToString());
                    }
                    catch
                    {
                        cita.IdPaciente = 0;
                    }
                        
                    cita.IdDoctor = int.Parse(registro["id_doctor"].ToString());

                    citas.Add(cita);
                }

                conexion.CerraConexion();

                if (citas.Count == 0)
                    throw new Exception();

                return citas;
            }
            catch
            {
                return null;
            }
        }

        public List<Cita> listarNoConfirmadas()
        {
            try
            {
                string consultaSQL;

                List<Cita> citas = new List<Cita>();
                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT * FROM Cita WHERE confirmada = 0 AND estado = 0";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                SqlDataReader registro = sqlCmd.ExecuteReader();

                while (registro.Read())
                {
                    Cita cita = new Cita();

                    cita.IdCita = int.Parse(registro["id_cita"].ToString());
                    cita.Fecha = DateTime.Parse(registro["fecha"].ToString());
                    cita.Estado = registro["estado"].ToString() == "False" ? 0 : 1;
                    cita.Confirmada = registro["confirmada"].ToString() == "False" ? 0 : 1;
                    cita.IdHorario = int.Parse(registro["id_horario"].ToString());

                    try
                    {
                        cita.IdPaciente = int.Parse(registro["id_paciente"].ToString());
                    }
                    catch
                    {
                        cita.IdPaciente = 0;
                    }

                    cita.IdDoctor = int.Parse(registro["id_doctor"].ToString());

                    citas.Add(cita);
                }

                conexion.CerraConexion();

                if (citas.Count == 0)
                    throw new Exception();

                return citas;
            }
            catch
            {
                return null;
            }
        }

        public Cita listarPorId(int id)
        {
            try
            {
                string consultaSQL;

                Cita cita = new Cita();
                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT * FROM Cita WHERE id_cita = @IdCita";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@IdCita", id);

                SqlDataReader registro = sqlCmd.ExecuteReader();

                registro.Read();

                cita.IdCita = int.Parse(registro["id_cita"].ToString());
                cita.Fecha = DateTime.Parse(registro["fecha"].ToString());
                cita.Estado = int.Parse(registro["estado"].ToString());
                cita.Confirmada = int.Parse(registro["confirmada"].ToString());
                cita.IdHorario = int.Parse(registro["id_horario"].ToString());
                cita.IdDoctor = int.Parse(registro["id_paciente"].ToString());
                cita.IdDoctor = int.Parse(registro["id_doctor"].ToString());

                conexion.CerraConexion();

                return cita;
            }
            catch
            {
                return null;
            }
        }
            
    }
}
