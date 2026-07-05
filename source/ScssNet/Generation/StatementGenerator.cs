using ScssNet.Structures;

namespace ScssNet.Generation;

internal class StatementGenerator(Lazy<RuleSetGenerator> ruleSetGenerator)
{
	public void Generate(IStatement statement, CssWriter writer)
	{
		switch(statement)
		{
			case RuleSet ruleSet:
				ruleSetGenerator.Value.Generate(ruleSet, writer);
				break;
			default:
				throw new NotSupportedException($"No generator found for Statement type {statement.GetType().Name}.");
		}
	}
}
