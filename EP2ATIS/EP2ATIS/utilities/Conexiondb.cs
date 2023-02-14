using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP2ATIS.utilities
{
    class Conexiondb
    {
        SqlConnection conDB;

        public Conexiondb()
        {
            conDB = new SqlConnection(Properties.Settings.Default.conexionDB);
        }

        public SqlConnection Conectar()
        {
            if (conDB.State == ConnectionState.Closed)
            {
                conDB.Open();
                return conDB;

            }
            else
            {
                return null;
            }

        }

        public void CerraConexion()
        {
            if (conDB.State == ConnectionState.Open)
                conDB.Close();
        }
    }
}
