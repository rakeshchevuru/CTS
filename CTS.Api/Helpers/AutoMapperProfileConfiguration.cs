using AutoMapper;
using CTS.Models.Adventureworks;
using CTS.Models.Product;

namespace CTS.Api.Helpers
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() : this("MyProfile")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName) : base(profileName)
        {
            CreateMap<Product, ProductInventoryModel>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.ProductSubcategory.ProductCategory.Name))
                .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.ProductSubcategory.Name));
        }
    }
}
