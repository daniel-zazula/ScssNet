using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class BlockParser(Lazy<RuleParser> ruleParser)
{
	internal Block? Parse(TokenReader tokenReader)
	{
		var openBrace = tokenReader.Match(Symbol.OpenBrace);
		if(openBrace is null)
			return null;

		return ParseRemainder(tokenReader, openBrace);
	}

	internal Block Require(TokenReader tokenReader)
	{
		return ParseRemainder(tokenReader, tokenReader.Require(Symbol.OpenBrace));
	}

	private Block ParseRemainder(TokenReader tokenReader, SymbolToken openBrace)
	{
		var rules = new List<Rule>();
		var rule = ruleParser.Value.Parse(tokenReader);
		while(rule != null)
		{
			rules.Add(rule);
			if(rule.SemiColon is null)
				break;

			rule = ruleParser.Value.Parse(tokenReader);
		}

		var closeBrace = tokenReader.Require(Symbol.CloseBrace);
		return new Block(openBrace, rules, closeBrace);
	}
}
