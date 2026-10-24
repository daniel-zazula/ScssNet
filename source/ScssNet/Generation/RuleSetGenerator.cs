using ScssNet.Structures;

namespace ScssNet.Generation;

internal class RuleSetGenerator(GeneratorContext generatorContext): IGenerator<RuleSet>
{
	private IGenerator<SelectorList> SelectorListGenerator => generatorContext.GetGenerator<SelectorList>();
	private IGenerator<Block> BlockGenerator => generatorContext.GetGenerator<Block>();

	public void Generate(RuleSet ruleSet, CssWriter writer)
	{
		SelectorListGenerator.Generate(ruleSet.SelectorList, writer);
		BlockGenerator.Generate(ruleSet.RuleBlock, writer);
	}
}
