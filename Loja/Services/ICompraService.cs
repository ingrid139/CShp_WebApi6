using Loja.Api.Models;
using System.Collections.Generic;

namespace Loja.Services
{
    public interface ICompraService
    {
        IList<Compra> ProcurarPorClienteId(int clienteId);
        Compra ProcurarPorId(int compraId);
        IList<Compra> ProcurarPorProduto(int produtoId);
        Compra Salvar(Compra compra);
    }
}