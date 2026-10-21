using ScssNet.Structures;

namespace ScssNet.Generation;

internal class UrlGenerator(Lazy<FunctionCallGenerator> functionCallGenerator)
{
	public void Generate(IUrl url, CssWriter writer)
	{
		switch(url)
		{
			case StringUrl stringUrl:
				writer.Write(stringUrl.Value);
				break;
			case FunctionUrl functionUrl:
				functionCallGenerator.Value.Generate(functionUrl.Value, writer);
				break;
			default:
				throw new InvalidOperationException($"Unsupported value type: {url.GetType().FullName}");
		}
	}
}
