using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class BlockGenerator(Lazy<RuleGenerator> ruleGenerator)
{
	public void Generate(Block block, CssWriter writer)
	{
		writer.Write(block.OpenBrace);

		foreach(var rule in block.Rules)
		{
			ruleGenerator.Value.Generate(rule, writer);
		}

		writer.Write(block.CloseBrace);
	}
}
