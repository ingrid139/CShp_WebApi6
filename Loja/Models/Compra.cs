using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.Api.Models
{
    [Table("Compra")]
    public class Compra
    {
        //1 para N

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Quantidade")]
        [Required]
        public int Quantidade { get; set; }

        [Column("Preco", TypeName = "decimal(9,2)")]
        [Required]
        public double Preco { get; set; }

        [Column("Cliente_Id")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }


        [Column("Produto_Id")]
        [Required]
        public int ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        public virtual Produto Produtos { get; set; }

    }
}