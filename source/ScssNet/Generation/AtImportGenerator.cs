using ScssNet.Structures;

namespace ScssNet.Generation;

internal class AtImportGenerator(Lazy<UrlGenerator> urlGenerator, Lazy<MediaQueryGenerator> mediaQueryGenerator)
{
	public void Generate(AtImport atImport, CssWriter writer)
	{
		writer.Write(atImport.AtSign);
		writer.Write(atImport.Import);
		writer.Write(" ");
		urlGenerator.Value.Generate(atImport.Url, writer);

		if (atImport.MediaQuery is not null)
		{
			writer.Write(" ");
			mediaQueryGenerator.Value.Generate(atImport.MediaQuery, writer);
		}

		if(atImport.SemiColon is not null)
		{
			writer.Write(atImport.SemiColon);
		}
	}
}
