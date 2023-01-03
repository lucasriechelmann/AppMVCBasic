using AppMVCBasic.Business.Models;
using AppMVCBasic.UI.Models;
using AutoMapper;

namespace AppMVCBasic.UI.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
