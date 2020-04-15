using System.ComponentModel.DataAnnotations;

namespace Loja.DTO
{
    public class ProdutoDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        public decimal PrecoUnitario { get; set; }
    }
}
