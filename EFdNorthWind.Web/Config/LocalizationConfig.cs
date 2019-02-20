using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFdNorthWind.Web.Config
{
    public class LocalizationConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources" );
        }
    }
}
