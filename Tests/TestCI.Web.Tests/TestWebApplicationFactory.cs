using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using TestCI.Web;

namespace TestCI.Web.Tests
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
