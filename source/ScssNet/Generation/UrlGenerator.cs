using ScssNet.Structures;

namespace ScssNet.Generation;

internal class UrlGenerator(GeneratorContext generatorContext): IGenerator<IUrl>
{
	private IGenerator<FunctionCall> FunctionCallGenerator => generatorContext.GetGenerator<FunctionCall>();

	public void Generate(IUrl url, CssWriter writer)
	{
		switch(url)
		{
			case StringUrl stringUrl:
				writer.Write(stringUrl.Value);
				break;
			case FunctionUrl functionUrl:
				FunctionCallGenerator.Generate(functionUrl.Value, writer);
				break;
			default:
				throw new InvalidOperationException($"Unsupported value type: {url.GetType().FullName}");
		}
	}
}
