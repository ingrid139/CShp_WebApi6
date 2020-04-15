using Loja.Api.Models;
using System.Collections.Generic;

namespace Loja.Services
{
    public interface IClienteService
    {
        Cliente ProcurarPorId(int clienteId);
        IList<Cliente> ProcurarPorNome(string nome);
        Cliente Salvar(Cliente cliente);
    }
}