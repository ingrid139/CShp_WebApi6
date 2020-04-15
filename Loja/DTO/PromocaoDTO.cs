using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.DTO
{
    public class PromocaoDTO
    {
        public int Id { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataTermino { get; set; }

        //propriedade de navegação
        public IEnumerable<PromocaoProdutoDTO> PromocaoProdutos { get; set; }
    }
}
