using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.Api.Models
{
    [Table("Enderecos")]
    public class Endereco
    {
        //1 para 1
        [Key]
        [Column("Id")]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("Numero")]
        [Required]
        public int Numero { get; set; }

        [Column("Logradouro")]
        [StringLength(200)]
        [Required]
        public string Logradouro { get; set; }

        [Column("Complemento")]
        [StringLength(100)]
        public string Complemento { get; set; }

        [Column("Bairro")]
        [StringLength(50)]
        [Required]
        public string Bairro { get; set; }

        [Column("Cidade")]
        [StringLength(100)]
        [Required]
        public string Cidade { get; set; }

 
        public virtual Cliente Cliente { get; set; }

    }
}