using AutoMapper;

using Upr8.WebApp.Data.Models;
using Upr8.WebApp.Models;

namespace Upr8.WebApp.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CategoryDto, Categories>()
                .ReverseMap();

            CreateMap<ProductDto, Products>()
                .ReverseMap();
        }
    }
}
