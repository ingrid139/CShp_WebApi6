using Loja.Api.Models;
using Loja.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Services
{
    public interface IProdutoService
    {
        IList<Produto> ProcurarPorCategoria(string nome);
        IList<Produto> ListarProdutos();
        Produto ProcurarPorId(int produtoId);
        Produto ProcurarAleatorio();
        Task<IEnumerable<ProdutoPesquisaDTO>> pesquisaAdo(ProdutoParametrosPesquisaDTO parametros);
        Produto Salvar(Produto produto);
    }
}