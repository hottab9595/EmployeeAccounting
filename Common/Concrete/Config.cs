using Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Common.Concrete
{
    public class Config : IConfig
    {
        private readonly IConfigurationRoot _configuration;
        public string ConnectionString => _configuration["ConnectionStrings:DefaultConnection"];
    }
}