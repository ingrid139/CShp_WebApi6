using System.ComponentModel.DataAnnotations;

namespace Loja.DTO
{
    public class TokenDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
