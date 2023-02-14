using EP2ATIS.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP2ATIS.service.dao
{
    interface ICitaDAO : ICrud<Cita>
    {
        bool confirmar(int idCita);

        List<Cita> listarNoConfirmadas();
    }
}
