using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;

namespace ScssNet.Test.Generation;

public abstract class GeneratorTestBase
{
	protected static ServiceProvider BuildServiceProvider()
	{
		var services = new ServiceCollection();
		services.AddScoped<StringWriter>();
		services.AddScoped<TextWriter>(p => p.GetRequiredService<StringWriter>());
		services.AddScoped<CssWriter>();
		services.AddGenerators();

		return services.BuildServiceProvider();
	}
}
