using Loja.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Services
{
    public class ProdutoService : IProdutoService
    {
        private LojaContexto _context;

        public ProdutoService(LojaContexto contexto)
        {
            _context = contexto;
        }

        public Produto ProcurarPorId(int produtoId)
        {
            //utilzar metodo Find
            return _context.Produtos.Find(produtoId);
        }

        public IList<Produto> ListarProdutos()
        {
            return _context.Produtos.ToList();
        }

        public IList<Produto> ProcurarPorCategoria(string nome)
        {
            //utilizar método Where
            return _context.Produtos.Where(x => x.Categoria == nome).ToList();
        }

        public Produto ProcurarAleatorio()
        {
            //lista inteira de Produtos
            var query = _context.Produtos.ToList();

            // se não retronar valor na lista não retorna valor na requisição
            if (query == null)
                return null;

            // a partir do tamanho da lista, retorna um valor aleatório que utilizaremos de index
            var RamdomIndex = new System.Random().Next(query.Count);

            // o metodo skip ignora a lista até o index passado como argumento
            var retorno = query.Skip(RamdomIndex).FirstOrDefault();
            
            //retorno produto aleatorio
            return retorno;
        }


        public Produto Salvar(Produto produto)
        {
            //verificar se é adicionar ou alterar
            var estado = produto.Id == 0 ? EntityState.Added : EntityState.Modified;

            //setar estado do entity
            _context.Entry(produto).State = estado;

            //persistir os dados
            _context.SaveChanges();

            //retornar o objeto
            return produto;
        }
    }
}
