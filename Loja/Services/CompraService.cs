using Loja.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Services
{
    public class CompraService : ICompraService
    {
        private LojaContexto _context;

        public CompraService(LojaContexto contexto)
        {
            _context = contexto;
        }

        public Compra ProcurarPorId(int compraId)
        {
            //utilzar metodo Find
            return _context.Compras.Find(compraId);
        }

        public IList<Compra> ProcurarPorClienteId(int clienteId)
        {
            //utilizar método Where
            return _context.Clientes
                .Where(x => x.Id == clienteId)
                .SelectMany(x => x.Compras)
                .Distinct()
                .ToList();
        }

        public IList<Compra> ProcurarPorProduto(int produtoId)
        {
            //utilizar método Where
            return _context.Produtos
                .Where(x => x.Id == produtoId)
                .SelectMany(x => x.Compras)
                .Distinct()
                .ToList();
        }

        public Compra Salvar(Compra compra)
        {
            //verificar se é adicionar ou alterar
            var estado = compra.Id == 0 ? EntityState.Added : EntityState.Modified;

            //setar estado do entity
            _context.Entry(compra).State = estado;

            //persistir os dados
            _context.SaveChanges();

            //retornar o objeto
            return compra;
        }
    }
}
