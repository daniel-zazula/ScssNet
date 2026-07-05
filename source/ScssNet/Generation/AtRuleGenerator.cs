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
			default:
				throw new NotSupportedException($"No generator found for AtRule type {atRule.GetType().Name}.");
		}
	}

	private void Generate(AtCharset charset, CssWriter writer)
	{
		writer.Write(charset.AtSign);
		writer.Write(charset.Value);
		if(charset.SemiColon is not null)
		{
			writer.Write(charset.SemiColon);
		}
	}
}
