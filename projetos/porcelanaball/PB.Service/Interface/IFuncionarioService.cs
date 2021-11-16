using PB.Domain;
using System.Collections.Generic;

namespace PB.Service.Interface
{
    public interface IFuncionarioService
    {
        List<Funcionario> Get();
        Funcionario Get(int codigo);
        int Insert(Funcionario funcionario);
        int Update(Funcionario funcionario);
        int Delete(int codigo);
    }
}
