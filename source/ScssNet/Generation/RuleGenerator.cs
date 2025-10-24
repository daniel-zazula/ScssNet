using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class RuleGenerator(Lazy<ValueGenerator> valueGenerator)
{
	public void Generate(Rule rule, CssWriter writer)
	{
		writer.Write(rule.Property);
		writer.Write(rule.Colon);
		valueGenerator.Value.Generate(rule.Value, writer);
		writer.Write(rule.SemiColon);
	}
}
