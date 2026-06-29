using ScssNet.Structures;

namespace ScssNet.Generation;

internal class RuleGenerator(Lazy<ValueGenerator> valueGenerator)
{
	public void Generate(Rule rule, CssWriter writer)
	{
		writer.Write(rule.Property);
		writer.Write(rule.Colon);
		valueGenerator.Value.Generate(rule.Value, writer);
		if (rule.Important != null)
		{
			Generate(rule.Important, writer);
		}
		if (rule.SemiColon != null)
		{
			writer.Write(rule.SemiColon);
		}
	}

	private void Generate(ImportantValue important, CssWriter writer)
	{
		writer.Write(important.Exclamation);
		writer.Write(important.Important);
	}
}
