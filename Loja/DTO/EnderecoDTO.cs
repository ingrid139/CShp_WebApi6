using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.DTO
{
    public class EnderecoDTO
    {

        public int Id { get; set; }

        [Required]
        public int Numero { get; set; }


        [Required]
        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }
    }
}
