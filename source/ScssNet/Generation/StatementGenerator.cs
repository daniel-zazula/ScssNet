using ScssNet.Structures;

namespace ScssNet.Generation;

internal class StatementGenerator(GeneratorContext generatorContext): IGenerator<IStatement>
{
	private IGenerator<RuleSet> RuleSetGenerator => generatorContext.GetGenerator<RuleSet>();
	private IGenerator<IAtRule> AtRuleGenerator => generatorContext.GetGenerator<IAtRule>();

	public void Generate(IStatement statement, CssWriter writer)
	{
		switch(statement)
		{
			case RuleSet ruleSet:
				RuleSetGenerator.Generate(ruleSet, writer);
				break;
			case IAtRule atRule:
				AtRuleGenerator.Generate(atRule, writer);
				break;
			default:
				throw new NotSupportedException($"No generator found for Statement type {statement.GetType().Name}.");
		}
	}
}
