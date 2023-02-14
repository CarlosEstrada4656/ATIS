using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EP2ATIS.model;
using System.Data.Common;
using System.Windows;

namespace EP2ATIS.service.dao
{
    class DAOpaciente
    {
        DAOpaciente()
        {

        }

        public static List<Paciente> MuestraPacientes()
        {
            List<Paciente> lstPacientes = new List<Paciente>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        //command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "select * from Paciente";
                        //DbDataReader dr = command.ExecuteReader();
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Paciente pa = new Paciente();
                                    pa.id_Paciente = int.Parse(dr["id_paciente"].ToString());
                                    pa.nombre = dr["nombres"].ToString();
                                    pa.apellido = dr["apellidos"].ToString();
                                    pa.genero = dr["genero"].ToString();
                                    pa.fecha_Nacimiento = DateTime.Parse(dr["fecha_nacimiento"].ToString());
                                    pa.direccion = dr["direccion"].ToString();
                                    pa.telefono = dr["telefono"].ToString();
                                    pa.dui = dr["dui"].ToString();
                                    pa.tipo_Sangre = dr["tipo_sangre"].ToString();
                                    pa.id_Usuario = int.Parse(dr["id_usuario"].ToString());


                                    //Agregar a la lista
                                    lstPacientes.Add(pa);
                                }//Fin de While
                            }//Fin de IF
                        }//Fin Using datareader
                    }//Fin using commando
                }//Fin using conn
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al mostrar los pacientes " + ex.Message);
            }
            return lstPacientes;
        }//Fin muestraPacientes

        public static List<Paciente> MuestraPacientesFiltro(int id)
        {
            List<Paciente> lstPacientes = new List<Paciente>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        //command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "select * from Paciente where id_paciente = " + id;
                        //DbDataReader dr = command.ExecuteReader();
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Paciente pa = new Paciente();
                                    pa.id_Paciente = int.Parse(dr["id_paciente"].ToString());
                                    pa.nombre = dr["nombres"].ToString();
                                    pa.apellido = dr["apellidos"].ToString();
                                    pa.genero = dr["genero"].ToString();
                                    pa.fecha_Nacimiento = DateTime.Parse(dr["fecha_nacimiento"].ToString());
                                    pa.direccion = dr["direccion"].ToString();
                                    pa.telefono = dr["telefono"].ToString();
                                    pa.dui = dr["dui"].ToString();
                                    pa.tipo_Sangre = dr["tipo_sangre"].ToString();
                                    pa.id_Usuario = int.Parse(dr["id_usuario"].ToString());


                                    //Agregar a la lista
                                    lstPacientes.Add(pa);
                                }//Fin de While
                            }//Fin de IF
                        }//Fin Using datareader
                    }//Fin using commando
                }//Fin using conn
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al mostrar los pacientes " + ex.Message);
            }
            return lstPacientes;
        }//Fin muestraPacientes
    }

}



