using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Loja.Api.Models;
using Loja.DTO;
using Loja.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        // GET: api/Produto
        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDto>> Get()
        {
            var produtos = _produtoService.ListarProdutos();

            if (produtos != null)
            {
                return Ok(_mapper.Map<List<ProdutoDto>>(produtos));
            }
            else
                return NotFound();
        }

        // GET: api/Produto/5
        [HttpGet("{id}")]
        public ActionResult<ProdutoDto> Get(int id)
        {
            var produto = _produtoService.ProcurarPorId(id);

            if (produto != null)
            {
                var retorno = _mapper.Map<ProdutoDto>(produto);

                return Ok(retorno);
            }
            else
                return NotFound();
        }


        // GET: api/Produto/aleatorio
        [HttpGet("aleatorio")]
        public ActionResult<ProdutoJSONDTO> GetAleatorio()
        {
            var produto = _produtoService.ProcurarAleatorio();

            if (produto != null)
            {
                var retorno = _mapper.Map<ProdutoJSONDTO>(produto);

                return Ok(retorno);
            }
            else
                return NotFound();
        }

        // GET: api/Produto/aleatorio
        // fromQuery para requests get com objeto de argumento para pesquisa
        [HttpGet("pesquisaADO")]
        public ActionResult<List<ProdutoPesquisaDTO>> GetPesquisaADO([FromQuery]ProdutoParametrosPesquisaDTO parametros)
        {
            // criei outra DTO pois os parâmetros não são obrigatórios
            var produto = _produtoService.pesquisaAdo(parametros).Result;

            if (produto != null)
            {
                return Ok(produto);
            }
            else
                return NotFound();
        }

        // POST: api/Produto
        [HttpPost]
        public ActionResult<ProdutoDto> Post([FromBody]ProdutoDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var produto = _mapper.Map<Produto>(value);
            //Salvar
            var retorno = _produtoService.Salvar(produto);
            //mapear Model para Dto
            return Ok(_mapper.Map<ProdutoDto>(retorno));
        }

        // PUT: api/Produto/5
        [HttpPut]
        public ActionResult<ProdutoDto> Put([FromBody] ProdutoDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var produto = _mapper.Map<Produto>(value);
            //Salvar
            var retorno = _produtoService.Salvar(produto);
            //mapear Model para Dto

            return Ok(_mapper.Map<ProdutoDto>(retorno));
        }

    }
}
