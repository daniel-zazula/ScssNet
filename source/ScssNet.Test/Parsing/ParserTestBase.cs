using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace ScssNet.Test.Parsing;

public abstract class ParserTestBase
{
	protected static ServiceProvider BuildServiceProvider(string source)
	{
		var reader = new StringReader(source);

		var services = new ServiceCollection();
		services.AddSingleton<TextReader>(reader);
		services.AddReaders();
		services.AddTokenParsers();
		services.AddParsers();

		return services.BuildServiceProvider();
	}
}
