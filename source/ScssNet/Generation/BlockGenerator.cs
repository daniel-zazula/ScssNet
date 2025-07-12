using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class BlockGenerator(RuleGenerator ruleGenerator)
{
	public void Generate(Block block, TextWriter writer)
	{
		writer.Write("{");
		foreach(var rule in block.Rules)
		{
			ruleGenerator.Generate(rule, writer);
		}
		writer.Write("}");
	}
}
