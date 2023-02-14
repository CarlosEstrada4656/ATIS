using EP2ATIS.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP2ATIS.service.dao
{
    interface IDoctorDAO
    {
        string listarNombrePorId(int id);
        List<Doctor> listar();
    }
}
