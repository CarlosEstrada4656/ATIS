using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP2ATIS.service.dao
{
    interface ICrud<T>
    {
        bool insetar(T t);

        List<T> listar();

        T listarPorId(int id);

        bool editar(T t);

        bool eliminar(int id);

    }
}
