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

namespace EP2ATIS.service.dao
{
    class DAOdoctor
    {
        public DAOdoctor()
        {

        }

        //Metodo para cargar datagrid/combobox de doctores
        public static List<Doctor> MuestraDoctores()
        {
            List<Doctor> lstDoctores = new List<Doctor>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        //command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "select * from Doctor";
                        //DbDataReader dr = command.ExecuteReader();
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Doctor doc = new Doctor();
                                    doc.IdDoctor = int.Parse(dr["id_doctor"].ToString());
                                    doc.Nombres = dr["nombres"].ToString();
                                    doc.Apellidos = dr["apellidos"].ToString();
                                    

                                    //Agregar a la lista
                                    lstDoctores.Add(doc);
                                }//Fin de While
                            }//Fin de IF
                        }//Fin Using datareader
                    }//Fin using commando
                }//Fin using conn
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al mostrar los doctores " + ex.Message);
            }
            return lstDoctores;
        }//Fin muestraDoctores
    }
}
