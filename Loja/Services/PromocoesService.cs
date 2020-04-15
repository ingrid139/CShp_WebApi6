using Loja.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Services
{
    public class PromocoesService : IPromocoesService
    {
        private LojaContexto _context;

        public PromocoesService(LojaContexto contexto)
        {
            _context = contexto;
        }

        public Promocao ProcurarPorId(int promocaoId)
        {
            //utilzar metodo Find
            return _context.Promocoes.Find(promocaoId);
        }

        public IList<Promocao> ProdutosPorPromocaoId(int promocaoId)
        {
            return _context.Promocoes
                            .Where(x => x.Id == promocaoId)
                            .Include(p => p.Produtos)
                            .ThenInclude(pp => pp.Produto)
                            .ToList();
        }

        public Promocao ProdutosPromocoes()
        {
            return _context.Promocoes
                            .Include(p => p.Produtos)
                            .ThenInclude(pp => pp.Produto)
                            .FirstOrDefault();
        }

        public IList<Promocao> ProdutosPromocoesLista()
        {
            return _context.Promocoes
                            .Include(p => p.Produtos)
                            .ThenInclude(pp => pp.Produto)
                            .ToList();
        }

        public Promocao Salvar(Promocao produto)
        {

            var existe = _context.Promocoes.Find(produto.Id);

            if (existe == null)
                _context.Promocoes.Add(produto);
            else
            {
                existe.DataInicio = produto.DataInicio;
                existe.DataTermino = produto.DataTermino;
            }

            _context.SaveChanges();

            return produto;
        }
    }
}
