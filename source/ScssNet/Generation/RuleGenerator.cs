using ScssNet.Structures;

namespace ScssNet.Generation;

internal class RuleGenerator(GeneratorContext generatorContext): IGenerator<Rule>
{
	private IGenerator<IValue> ValueGenerator => generatorContext.GetGenerator<IValue>();

	public void Generate(Rule rule, CssWriter writer)
	{
		writer.Write(rule.Property);
		writer.Write(rule.Colon);
		ValueGenerator.Generate(rule.Value, writer);
		Generate(rule.Important, writer);
		if (rule.SemiColon != null)
		{
			writer.Write(rule.SemiColon);
		}
	}

	private void Generate(ImportantValue? important, CssWriter writer)
	{
		if(important is null)
			return;

		writer.Write(important.Exclamation);
		writer.Write(important.Important);
	}
}
