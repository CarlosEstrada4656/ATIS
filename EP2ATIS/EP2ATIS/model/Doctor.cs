using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP2ATIS.model
{
    class Doctor
    {
        public int IdDoctor { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Genero { get; set; }

        public int Experiencia { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public int IdEspecialidad { get; set; }

        public int IdUsuario { get; set; }
    }
}
