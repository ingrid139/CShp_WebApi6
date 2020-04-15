using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.Api.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        [Column("Id")]
        [Required]
        public int Id { get; set; }

        [Column("Name")]
        [StringLength(100)]
        [Required]
        public string Nome { get; set; }

        [Column("Categoria")]
        [StringLength(50)]
        [Required]
        public string Categoria { get; set; }

        [Column("PrecoUnitario", TypeName ="decimal(9,2)")]
        [Required]
        public decimal PrecoUnitario { get; set; }

        public ICollection<PromocaoProduto> Promocoes { get; set; }

        //propriedade de navegação
        public virtual ICollection<Compra> Compras { get; set; }


        public override string ToString()
        {
            return $"Produto: {this.Id}, {this.Nome}, {this.Categoria}, {this.PrecoUnitario}";
        }
    }
}