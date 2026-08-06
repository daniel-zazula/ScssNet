using ScssNet.Structures;

namespace ScssNet.Generation;

internal class AtImportGenerator(Lazy<ValueGenerator> valueGenerator)
{
	public void Generate(AtImport atImport, CssWriter writer)
	{
		writer.Write(atImport.AtSign);
		writer.Write(atImport.Import);
		writer.Write(" ");
		valueGenerator.Value.Generate(atImport.Path, writer);
		if(atImport.SemiColon is not null)
		{
			writer.Write(atImport.SemiColon);
		}
	}
}
