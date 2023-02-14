using EP2ATIS.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP2ATIS.service.dao
{
    interface IHorarioDAO
    {
        string listarHoraPorId(int id);

        List<Horario> listarDisponible(DateTime? fecha, int? idDoctor);

        int buscarIdPorHora(string hora);
    }
}
