using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP2ATIS.model
{
    class CitaPaciente
    {
        public CitaPaciente()
        {

        }

        public int IdCita { get; set; }
        public DateTime Fecha { get; set; }
        public string hora { get; set; }

        public string nombreDoctor { get; set; }

        public string nombrePaciente { get; set; }

    }
}
