using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP2ATIS.model
{
    class Paciente
    {

        public Paciente()
        {

        }

        public int id_Paciente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string genero { get; set; }
        public DateTime fecha_Nacimiento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string dui { get; set; }
        public string tipo_Sangre { get; set; }
        public int id_Usuario { get; set; }




    }
}
