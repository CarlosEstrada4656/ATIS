using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP2ATIS.model
{
    class Cita
    {
        public int IdCita { get; set; }

        public DateTime Fecha { get; set; }

        public int Estado { get; set; }

        public int Confirmada { get; set; }

        public int IdHorario { get; set; }

        public int IdDoctor { get; set; }

        public int IdPaciente { get; set; }
        
    }
}
