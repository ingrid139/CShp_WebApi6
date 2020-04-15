using System.ComponentModel.DataAnnotations;

namespace Loja.DTO
{
    public class PromocaoProdutoDTO
    {
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public int PromocaoId { get; set; }
    }
}
