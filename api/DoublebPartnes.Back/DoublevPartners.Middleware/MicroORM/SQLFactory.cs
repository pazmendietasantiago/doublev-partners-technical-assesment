using Microsoft.Extensions.Configuration;

namespace DoublebPartnes.Middleware.MicroORM;

public class SQLFactory
{
    public static SQLKernel sqlKernel { get; private set; }

    public static void Configure(IConfiguration configuration) => sqlKernel = new SQLKernel(configuration);
}