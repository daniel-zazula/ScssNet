using ScssNet.Structures;

namespace ScssNet.Generation;

internal class AtRuleGenerator
{
	public void Generate(IAtRule atRule, CssWriter writer)
	{
		switch(atRule) {
			case AtCharset charset:
				Generate(charset, writer);
				break;
			case AtImport import:
				Generate(import, writer);
				break;
			default:
				throw new NotSupportedException($"No generator found for AtRule type {atRule.GetType().Name}.");
		}
	}

	private void Generate(AtCharset atCharset, CssWriter writer)
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

	private void Generate(AtImport atImport, CssWriter writer)
	{
		writer.Write(atImport.AtSign);
		writer.Write(atImport.Import);
		writer.Write(" ");
		writer.Write(atImport.Path);
		if(atImport.SemiColon is not null)
		{
			writer.Write(atImport.SemiColon);
		}
	}
}
