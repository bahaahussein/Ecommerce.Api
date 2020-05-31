
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Helpers
{
    public static class ConfigurationHelpers
    {
        public static T GetSectionModel<T>(this IConfiguration configuration)
        {
            return configuration.GetSection(typeof(T).Name).Get<T>();
        }
    }
}
