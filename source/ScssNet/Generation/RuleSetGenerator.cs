using ScssNet.Structures;

namespace ScssNet.Generation;

internal class RuleSetGenerator(Lazy<SelectorListGenerator> selectorListGenerator, Lazy<RuleGenerator> ruleGenerator)
{
	public void Generate(RuleSet ruleSet, CssWriter writer)
	{
		selectorListGenerator.Value.Generate(ruleSet.SelectorList, writer);
		writer.Write(ruleSet.OpenBrace);

		foreach(var rule in ruleSet.Rules)
		{
			ruleGenerator.Value.Generate(rule, writer);
		}

		writer.Write(ruleSet.CloseBrace);
	}
}
