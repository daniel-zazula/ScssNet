using Microsoft.Extensions.DependencyInjection;

namespace ScssNet.Parsing;

internal class ParserContext(IServiceProvider serviceProvider)
{
	internal T GetParser<T>() where T: IParser
	{
		return serviceProvider.GetRequiredService<T>();
	}
}
