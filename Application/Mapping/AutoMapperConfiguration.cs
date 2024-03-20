using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
