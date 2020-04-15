using AutoMapper;
using Loja.Api.Models;
using Loja.DTO;

namespace Loja.ConfigStartup
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Produto, ProdutoJSONDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<CompraDTO, Compra>().ReverseMap();
            CreateMap<PromocaoProduto, PromocaoProdutoDTO>().ReverseMap();
            CreateMap<Promocao, PromocaoDTO>()
                .ForMember(dest => dest.PromocaoProdutos, opt => opt.MapFrom(src => src.Produtos))
                .ReverseMap();
            CreateMap<ProdutoJSONDTO, Produto>().ReverseMap();
        }
    }
}
