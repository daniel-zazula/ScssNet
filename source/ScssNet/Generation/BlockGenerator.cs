using ScssNet.Structures;

namespace ScssNet.Generation;

internal class BlockGenerator(GeneratorContext generatorContext): IGenerator<Block>
{
	private IGenerator<Rule> RuleGenerator => generatorContext.GetGenerator<Rule>();

	public void Generate(Block block, CssWriter writer)
	{
		writer.Write(block.OpenBrace);

		foreach(var rule in block.Rules)
		{
			RuleGenerator.Generate(rule, writer);
		}

		writer.Write(block.CloseBrace);
	}
}
