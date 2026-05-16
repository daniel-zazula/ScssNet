using ScssNet.Structures;

namespace ScssNet.Generation;

internal class RuleGenerator(Lazy<ValueGenerator> valueGenerator)
{
	public void Generate(Rule rule, CssWriter writer)
	{
		writer.Write(rule.Property);
		writer.Write(rule.Colon);
		valueGenerator.Value.Generate(rule.Value, writer);
		if (rule.SemiColon != null)
		{
			writer.Write(rule.SemiColon);
		}
	}
}
