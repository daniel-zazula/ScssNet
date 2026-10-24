using ScssNet.Structures;

namespace ScssNet.Generation;

internal class AtNamespaceGenerator(GeneratorContext generatorContext): IGenerator<AtNamespace>
{
	private IGenerator<IUrl> UrlGenerator => generatorContext.GetGenerator<IUrl>();

	public void Generate(AtNamespace atNamespace, CssWriter writer)
	{
		writer.Write(atNamespace.AtSign);
		writer.Write(atNamespace.Namespace);

		if (atNamespace.Prefix is not null)
		{
			writer.Write(" ");
			writer.Write(atNamespace.Prefix);
		}

		writer.Write(" ");
		UrlGenerator.Generate(atNamespace.Url, writer);

		if (atNamespace.SemiColon is not null)
		{
			writer.Write(atNamespace.SemiColon);
		}
	}
}
