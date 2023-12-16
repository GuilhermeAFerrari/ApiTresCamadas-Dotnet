using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Models;

namespace DevIO.Api.Configurations;

public class AutoMapperConfigure : Profile
{
    public AutoMapperConfigure()
    {
        CreateMap<Supplier, SupplierViewModel>().ReverseMap();
        CreateMap<Address, AddressViewModel>().ReverseMap();

        CreateMap<Product, ProductViewModel>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));

        CreateMap<ProductViewModel, Product>();
    }
}