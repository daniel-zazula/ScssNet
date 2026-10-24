using ScssNet.Structures;

namespace ScssNet.Generation;

internal class AtImportGenerator(GeneratorContext generatorContext): IGenerator<AtImport>
{
	private IGenerator<IUrl> UrlGenerator => generatorContext.GetGenerator<IUrl>();
	private IGenerator<IMediaQuery> MediaQueryGenerator => generatorContext.GetGenerator<IMediaQuery>();

	public void Generate(AtImport atImport, CssWriter writer)
	{
		writer.Write(atImport.AtSign);
		writer.Write(atImport.Import);
		writer.Write(" ");
		UrlGenerator.Generate(atImport.Url, writer);

		if (atImport.MediaQuery is not null)
		{
			writer.Write(" ");
			MediaQueryGenerator.Generate(atImport.MediaQuery, writer);
		}

		if(atImport.SemiColon is not null)
		{
			writer.Write(atImport.SemiColon);
		}
	}
}
