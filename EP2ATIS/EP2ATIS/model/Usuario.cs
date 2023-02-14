using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP2ATIS.model
{
    class Usuario
    {
        public int IdUsuario { get; set; }

        public string Correo { get; set; }

        public string Clave { get; set; }

        public int IdRol { get; set; }
    }
}
