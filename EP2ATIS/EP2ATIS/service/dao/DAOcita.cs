using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EP2ATIS.model;
using System.Data.Common;
using System.Windows;
using EP2ATIS.utilities;

namespace EP2ATIS.service.dao
{
    class DAOcita
    {
        public DAOcita()
        {

        }

        //Metodo para insertar cita
        public static int InsertarCita(Cita ci)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        
                        //command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "INSERT INTO Cita (fecha, estado, confirmada, id_horario, id_doctor, id_paciente) values (@fecha, @estado, @confirmada, @id_horario, @id_doctor, @id_paciente)";
                        command.Parameters.AddWithValue("@fecha", ci.Fecha);
                        command.Parameters.AddWithValue("@estado", ci.Estado);
                        command.Parameters.AddWithValue("@confirmada", ci.Confirmada);
                        command.Parameters.AddWithValue("@id_horario", ci.IdHorario);
                        command.Parameters.AddWithValue("@id_doctor", ci.IdDoctor);
                        command.Parameters.AddWithValue("@id_paciente", ci.IdPaciente);


                        res = command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar la cita: " + ex.Message);
            }
            return res;
        }//Fin InsertarCita

        //Metodo para cargar datagrid/combobox de horarios
        public static List<Horario> MuestraHorarios()
        {
            List<Horario> lstHorarios = new List<Horario>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        //command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "select * from Horario";
                        //DbDataReader dr = command.ExecuteReader();
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Horario ho = new Horario();
                                    ho.id_Horario = int.Parse(dr["id_horario"].ToString());
                                    ho.hora = dr["hora"].ToString();

                                    //Agregar a la lista
                                    lstHorarios.Add(ho);
                                }//Fin de While
                            }//Fin de IF
                        }//Fin Using datareader
                    }//Fin using commando
                }//Fin using conn
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al mostrar los horarios " + ex.Message);
            }
            return lstHorarios;
        }//Fin muestraHorarios

        //Metodo para cargar datagrid/combobox de cupos
        public static int MuestraCupos(DateTime fecha)
        {
            int cupo = 0;

            try
            {
                string consultaSQL;

                Conexiondb conexion = new Conexiondb();
                SqlConnection conDB = conexion.Conectar();

                consultaSQL = "select count(*) AS cupo from Cita where fecha = @Fecha and estado !=1 and confirmada !=0";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Fecha", fecha);

                SqlDataReader registro = sqlCmd.ExecuteReader();

                registro.Read();

                cupo = int.Parse(registro["cupo"].ToString());

                conexion.CerraConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al ejecutar la consulta " + ex.Message);
            }
            return cupo;
        }//Fin muestraCupos

        //Metodo para cargar datagrid/combobox de citas de paciente
        public static List<CitaPaciente> MuestraCitas(int idPaciente)
        {
            List<CitaPaciente> lstCitas = new List<CitaPaciente>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        //command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "select c.id_cita, c.fecha, h.hora, d.nombres + d.apellidos nombreD, p.nombres + p.apellidos as nombreP from Cita c inner join Horario h on h.id_horario = c.id_horario inner join Doctor d on d.id_doctor = c.id_doctor inner join Paciente p on p.id_paciente = c.id_paciente where c.estado = 0 and c.id_paciente = " + idPaciente;
                        //DbDataReader dr = command.ExecuteReader();
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CitaPaciente cp = new CitaPaciente();
                                    cp.IdCita = int.Parse(dr["id_cita"].ToString());
                                    cp.Fecha = DateTime.Parse(dr["fecha"].ToString());
                                    cp.hora = dr["hora"].ToString();
                                    cp.nombreDoctor = dr["nombreD"].ToString();
                                    cp.nombrePaciente = dr["nombreP"].ToString();

                                 

                                    //Agregar a la lista
                                    lstCitas.Add(cp);
                                }//Fin de While
                            }//Fin de IF
                        }//Fin Using datareader
                    }//Fin using commando
                }//Fin using conn
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al mostrar las citas " + ex.Message);
            }
            return lstCitas;
        }//Fin muestraCitas

        //Metodo para actualizar citas de pacientes
        public static int ModificarCita(int idCita)
        {
            int res = 0;
            try
            {

                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "UPDATE Cita set estado = 1 where id_cita = " + idCita;
  
                        res = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar la cita: " + ex.Message);
            }
            return res;
        }//Fin de ModificarCita

        //Metodo para validar horario
        /*public static int validarCita(int idHorario, int idDoctor, DateTime fecha)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    string fechaPrueba = (fecha.ToShortDateString()).ToString();
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        //command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SELECT count(*) from Cita where id_horario = @id_horario and id_doctor = @id_doctor and fecha = '@fecha';";
                        command.Parameters.AddWithValue("@id_horario", idHorario);
                        command.Parameters.AddWithValue("@id_doctor", idDoctor);
                        command.Parameters.AddWithValue("@fecha", fecha.ToShortDateString());
                        //DbDataReader dr = command.ExecuteReader();
                        res = (int)command.ExecuteScalar();
                    }//Fin using commando
                }//Fin using conn
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al validar el horario " + ex.Message);
            }
            return res;
        }//Fin validarCita
        */

        public List<Horario> listarDisponible(DateTime fecha, int idDoctor)
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
    }
}
