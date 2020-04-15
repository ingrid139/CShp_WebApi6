using System.ComponentModel.DataAnnotations;

namespace Loja.DTO
{
    public class CompraDTO
    {
        public int Id { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public double Preco { get; set; }

        [Required]
        public int ClienteId { get; set; }


        [Required]
        public int ProdutoId { get; set; }
    }
}
