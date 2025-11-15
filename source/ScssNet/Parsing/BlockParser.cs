using ScssNet.SourceElements;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class BlockParser(Lazy<RuleParser> ruleParser)
{
	internal Block? Parse(ITokenReader tokenReader)
	{
		var openBrace = tokenReader.Match(Symbol.OpenBrace);
		if(openBrace is null)
			return null;

		var rule = ruleParser.Value.Parse(tokenReader);
		var rules = new List<Rule>();
		while(rule != null)
		{
			rules.Add(rule);
			rule = ruleParser.Value.Parse(tokenReader);
		}

		var closeBrace = tokenReader.Require(Symbol.CloseBrace);
		return new Block(openBrace!, rules, closeBrace);
	}
}
