using Dapper;
using Loja.Api.Models;
using Loja.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Produto> OrdenarPorCategoria(List<Produto> produtos)
        {
            // definir ordenação
            var ordenacao = produtos.GroupBy(x => x.Categoria)
                                    .Select(group => new
                                            {
                                                Categoria = group.Key,
                                                Quantidade = group.Count()
                                            })
                                    .OrderByDescending(x => x.Quantidade)
                                    .ToList();
            
            // aplicar ordenação definida na lista desejada                                    
            return produtos.OrderBy(x => ordenacao.Select(y => y.Categoria).IndexOf(x.Categoria)).ToList();
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

        // pesquisa com filtros através de ADO NET
        public async Task<IEnumerable<ProdutoPesquisaDTO>> pesquisaAdo(ProdutoParametrosPesquisaDTO parametros)
        {
            // estabelecer conexao com o banco de dados
            // get connection string - pega string connection do banco respectivo
            using (IDbConnection connection = GetConnection())
            {
                // select todos campos (*)
                // where 1 = 1 para poder add AND caso tenha necessidade
                // caso não sejam passados campos para filtro, a requisição deverá responder normalmente
                var sql = new StringBuilder();
                sql.Append(@"SELECT * FROM PRODUTO WHERE 1 = 1 ");

                // filtros
                if (parametros.Id > 0)
                {
                    sql.AppendFormat(" AND Id = {0}", parametros.Id);
                }
                if (!string.IsNullOrEmpty(parametros.Nome))
                {
                    //like para procurar por algo parecido e não só exato
                    // os sinais de % no inicio e no fim para procurar em qualquer parte da string
                    sql.AppendFormat(" AND Name like '%{0}%'", parametros.Nome);
                }
                if (!string.IsNullOrEmpty(parametros.Categoria))
                {
                    //like para procurar por algo parecido e não só exato
                    // os sinais de % no inicio e no fim para procurar em qualquer parte da string
                    sql.AppendFormat(" AND Categoria like '%{0}%'", parametros.Categoria);
                }

                //ordenação sempre no final. Testar true/false
                var ordenarPor = true;
                if (ordenarPor)
                {
                    sql.Append(string.Format($" ORDER BY Name"));
                }
                else
                {
                    sql.Append(string.Format($" ORDER BY Categoria"));
                }

                // executa o comando sql em forma de string (sql.ToString()) através do método QueryAsync
                // dapper é uma biblioteca de mapeamento de objeto tipado por <ProdutoPesquisaDTO> com ADO - noco DTO para refletir campos da tabela - dapper não considera dataannotation
                // listas retornar como IEnumerable
                // metodo de requisição assincrona (utilizar await para aguardar retorno da resposta da requisição)
                // add async Task no retorno do metodo
                return await connection.QueryAsync<ProdutoPesquisaDTO>(sql.ToString());
            }
        }

        private IDbConnection GetConnection()
        {
            // retornar string connection
            return new SqlConnection("Server=localhost,1433;Database=LojaServices3;User Id =sa;Password=Ing@2020;Trusted_Connection=False;");
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
