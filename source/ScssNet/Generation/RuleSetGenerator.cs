using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class RuleSetGenerator(Lazy<SelectorListGenerator> selectorListGenerator, Lazy<BlockGenerator> blockGenerator)
{
	public void Generate(RuleSet ruleSet, CssWriter writer)
	{
		selectorListGenerator.Value.Generate(ruleSet.SelectorList, writer);
		blockGenerator.Value.Generate(ruleSet.RuleBlock, writer);
	}
}
