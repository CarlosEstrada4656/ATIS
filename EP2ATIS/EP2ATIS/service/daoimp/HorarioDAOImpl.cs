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
    class HorarioDAOImpl : IHorarioDAO
    {
        public int buscarIdPorHora(string hora)
        {
            try
            {
                string consultaSQL;
                int id_horario;

                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT id_horario FROM Horario WHERE hora = @Hora";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Hora", hora);

                SqlDataReader registro = sqlCmd.ExecuteReader();

                registro.Read();

                id_horario = int.Parse(registro["id_horario"].ToString());

                conexion.CerraConexion();

                return id_horario;
            }
            catch
            {
                return 0;
            }
        }

        public List<Horario> listarDisponible(DateTime? fecha, int? idDoctor)
        {
            try
            {
                string consultaSQL;

                List<Horario> horarios = new List<Horario>();
                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT * FROM Horario WHERE hora NOT IN " +
                    "(SELECT h.hora FROM Cita c INNER JOIN Horario h ON c.id_horario = h.id_horario " +
                    "WHERE c.fecha = @Fecha  AND c.id_doctor = @IdDoctor AND c.estado = 0)";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Fecha", fecha);
                sqlCmd.Parameters.AddWithValue("@IdDoctor", idDoctor);

                SqlDataReader registro = sqlCmd.ExecuteReader();

                while (registro.Read())
                {
                    Horario horario = new Horario();

                    horario.id_Horario = int.Parse(registro["id_horario"].ToString());
                    horario.hora = registro["hora"].ToString();
                   
                    horarios.Add(horario);
                }

                conexion.CerraConexion();

                if (horarios.Count == 0)
                    throw new Exception();

                return horarios;
            }
            catch
            {
                return null;
            }
        }

        public string listarHoraPorId(int id)
        {
            try
            {
                string consultaSQL, hora;
              
                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "SELECT * FROM Horario WHERE id_horario = @IdHorario";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@IdHorario", id);

                SqlDataReader registro = sqlCmd.ExecuteReader();

                registro.Read();

                hora = registro["hora"].ToString();

                conexion.CerraConexion();

                return hora;
            }
            catch
            {
                return null;
            }
        }
    }
}
