using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class BlockGenerator(TokenGenerator tokenGenerator, Lazy<RuleGenerator> ruleGenerator)
{
	public void Generate(Block block, TextWriter writer)
	{
		tokenGenerator.Generate(block.OpenBrace, writer);
		foreach(var rule in block.Rules)
		{
			ruleGenerator.Value.Generate(rule, writer);
		}
		tokenGenerator.Generate(block.CloseBrace, writer);
	}
}
