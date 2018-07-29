using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IAcessoDB<T>
    {
        int SalvarRegistro(T registro);
        bool AtualizarRegistro(T registro);
        List<T> ObterRegistro(T registro);
        List<T> ObterTodos();
        T ObterRegistroPorCodigo(int codigo);
        bool DeletarRegistro(T registro);
    }
}
