using AutoMapper;
using IdentityModel.Client;
using Loja.Api.Models;
using Loja.DTO;
using Loja.Models;
using Loja.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Loja.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        // GET api/cliente/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ClienteDTO> Get(int id)
        {
            var cliente = _clienteService.ProcurarPorId(id);

            if (cliente != null)
            {
                var retorno = _mapper.Map<ClienteDTO>(cliente);

                return Ok(retorno);
            }
            else
                return NotFound();
        }

        // POST api/cliente
        // binding argumento
        [HttpPost]
        public ActionResult<ClienteDTO> Post([FromBody]ClienteDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorResponse.FromModelState(ModelState));


            var endereco = new Endereco()
            {
                Logradouro = value.Endereco.Logradouro,
                Numero = value.Endereco.Numero,
                Complemento = value.Endereco.Complemento,
                Bairro = value.Endereco.Bairro,
                Cidade = value.Endereco.Cidade
            };

            var cliente = new Cliente()
            {
                Nome = value.Nome,
                Email = value.Email,
                Password = value.Password,
                EnderecoDeEntrega = endereco
            };

            var retornoCliente = _clienteService.Salvar(cliente);

            var retorno = _mapper.Map<ClienteDTO>(retornoCliente);

            return Ok(retorno);
        }

        // POST api/cliente
        [HttpPut]
        public ActionResult<ClienteDTO> Put([FromBody] ClienteDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var endereco = new Endereco()
            {
                Id = value.EnderecoId,
                Logradouro = value.Endereco.Logradouro,
                Numero = value.Endereco.Numero,
                Complemento = value.Endereco.Complemento,
                Bairro = value.Endereco.Bairro,
                Cidade = value.Endereco.Cidade
            };

            var cliente = new Cliente()
            {
                Id = value.Id,
                Nome = value.Nome,
                Email = value.Email,
                Password = value.Password,
                EnderecoId = value.EnderecoId,
                EnderecoDeEntrega = endereco
            };

            var retornoCliente = _clienteService.Salvar(cliente);
            var retorno = _mapper.Map<ClienteDTO>(retornoCliente);

            return Ok(retorno);
        }

        // fromQuery para requests get com objeto de argumento para pesquisa
        [HttpGet("getToken")]
        public async Task<ActionResult<TokenResponse>> AuthToken([FromQuery]TokenDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // async request - await para aguardar retorno
            var disco = await DiscoveryClient.GetAsync("http://localhost:5001");

            // nesta parte, temos um exemplo de requisição com o tipo "password" 
            // esta é a forma mais comum
            var httpClient = new HttpClient();
            var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "codenation.api_client",
                ClientSecret = "codenation.api_secret",
                UserName = value.UserName,
                Password = value.Password,
                Scope = "codenation"
            });

            // Se não tiver tiver um erro retornar token
            if (!tokenResponse.IsError)
            {
                return Ok(tokenResponse);
            }

            //retorna não autorizado e descrição do erro
            return Unauthorized(ErrorResponse.FromTokenResponse(tokenResponse));
        }
    }
}