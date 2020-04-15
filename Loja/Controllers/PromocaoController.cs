using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Loja.Api.Models;
using Loja.DTO;
using Loja.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Controllers
{
    [Produces("application/json")]
    [Authorize("Admin")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PromocaoController : ControllerBase
    {
        private readonly IPromocoesService _promocaoService;
        private readonly IMapper _mapper;

        public PromocaoController(IPromocoesService promocaoService, IMapper mapper)
        {
            _promocaoService = promocaoService;
            _mapper = mapper;
        }

        // GET: api/Promocao
        [HttpGet]
        public ActionResult<IEnumerable<PromocaoDTO>> GetAll()
        {
            return Ok(_promocaoService.ProdutosPromocoesLista().
                Select(x => _mapper.Map<PromocaoDTO>(x)).
                ToList());

        }

        // GET: api/Promocao/5
        [HttpGet("{id}")]
        public ActionResult<PromocaoDTO> Get(int id)
        {
            var promocao = _promocaoService.ProcurarPorId(id);

            if (promocao != null)
            {
                // Substituir mapeamento de objeto manual por mapeamento com AutoMapper

                return Ok(_mapper.Map<PromocaoDTO>(promocao));
            }
            else
                return NotFound();
        }

        // POST: api/Promocao
        [HttpPost]
        public ActionResult<PromocaoDTO> Post([FromBody] PromocaoDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var promocao = _mapper.Map<Promocao>(value);
            //Salvar
            var retorno = _promocaoService.Salvar(promocao);
            //mapear Model para Dto
            return Ok(_mapper.Map<PromocaoDTO>(retorno));
        }

        // PUT: api/Promocao/5
        [HttpPut]
        public ActionResult<PromocaoDTO> Put([FromBody] PromocaoDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var promocao = _mapper.Map<Promocao>(value);
            //Salvar
            var retorno = _promocaoService.Salvar(promocao);
            //mapear Model para Dto
            return Ok(_mapper.Map<PromocaoDTO>(retorno));
        }

    }
}
