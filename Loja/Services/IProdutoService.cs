using Loja.Api.Models;
using System.Collections.Generic;

namespace Loja.Services
{
    public interface IProdutoService
    {
        IList<Produto> ProcurarPorCategoria(string nome);
        IList<Produto> ListarProdutos();
        Produto ProcurarPorId(int produtoId);
        Produto ProcurarAleatorio();
        Produto Salvar(Produto produto);
    }
}