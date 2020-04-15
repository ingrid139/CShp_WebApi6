using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Loja.Api.Models
{
    public class Promocao
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Descricao")]
        [StringLength(100)]
        [Required]
        public string Descricao { get; set; }

        [Column("DataInicio")]
        [StringLength(100)]
        [Required]
        public DateTime DataInicio { get; set; }

        [Column("DataTermino")]
        [StringLength(100)]
        [Required]
        public DateTime DataTermino { get; set; }

        //propriedade de navegação
        public ICollection<PromocaoProduto> Produtos { get; set; }

        public Promocao()
        {
            this.Produtos = new List<PromocaoProduto>();
        }

        public void IncluiProduto(Produto produto)
        {
            this.Produtos.Add(new PromocaoProduto() { Id = 0, PromocaoId = this.Id, ProdutoId = produto.Id });
        }

    }
}
