using Loja.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Services
{
    public class EnderecoService
    {
        private LojaContexto _context;

        public EnderecoService(LojaContexto contexto)
        {
            _context = contexto;
        }

        public Endereco ProcurarPorId(int enderecoId)
        {
            //utilzar metodo Find
            return _context.Enderecos.Find(enderecoId);
        }

        public IList<Endereco> ProcurarPorClienteId(int clienteId)
        {
            //utilizar método Where
            return _context.Enderecos
                            .Where(x => x.Cliente.Id == clienteId)
                            .ToList();
        }

        public Endereco Salvar(Endereco endereco)
        {
            //verificar se é adicionar ou alterar
            var estado = endereco.Id == 0 ? EntityState.Added : EntityState.Modified;

            //setar estado do entity
            _context.Entry(endereco).State = estado;

            //persistir os dados
            _context.SaveChanges();

            //retornar o objeto
            return endereco;
        }
    }
}
