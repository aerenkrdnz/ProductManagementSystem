using AutoMapper;
using ProductManagementBusiness.Dtos.Product;

using ProductManagementData.Entities;

namespace ProductManagementBusiness.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {

            CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));


            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));


            CreateMap<Product, ProductDto>();
        }
    }
}
