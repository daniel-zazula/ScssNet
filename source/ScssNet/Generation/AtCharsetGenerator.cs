using ScssNet.Structures;

namespace ScssNet.Generation;

internal class AtCharsetGenerator: IGenerator<AtCharset>
{
	public void Generate(AtCharset atCharset, CssWriter writer)
	{
		writer.Write(atCharset.AtSign);
		writer.Write(atCharset.Charset);
		writer.Write(" ");
		writer.Write(atCharset.CharsetName);
		if(atCharset.SemiColon is not null)
		{
			writer.Write(atCharset.SemiColon);
		}
	}
}
