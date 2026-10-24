using Microsoft.Extensions.DependencyInjection;

namespace ScssNet.Generation;

internal class GeneratorContext(IServiceProvider serviceProvider)
{
	internal IGenerator<T> GetGenerator<T>() where T: ISourceElement
	{
		return serviceProvider.GetRequiredService<IGenerator<T>>();
	}
}
