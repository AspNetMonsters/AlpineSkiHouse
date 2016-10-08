using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AlpineSkiHouse.Web.Tests.Data
{
public static class InMemoryDbContextOptionsFactory
{
    public static DbContextOptions<T> Create<T>() where T : DbContext
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();
            
        var builder = new DbContextOptionsBuilder<T>();
        builder.UseInMemoryDatabase()
                .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }
}
}
