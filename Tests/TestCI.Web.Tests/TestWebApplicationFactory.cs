using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestCI;
using TestCI.Web;

namespace Sanakan.Web.Tests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var configurationBuilder = new ConfigurationBuilder()
                    .Build();

                config.AddConfiguration(configurationBuilder);
            });
        }
    }
}
