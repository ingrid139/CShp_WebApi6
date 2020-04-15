using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.Api.Models
{
    [Table("Promocao_Produto")]
    public class PromocaoProduto
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Produto_Id")]
        [Required]
        public int ProdutoId { get; set; }

        [Column("Promocao_Id")]
        [Required]
        public int PromocaoId { get; set; }


        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }
        
        [ForeignKey("PromocaoId")]
        public virtual Promocao Promocao { get; set; }
    }
}
