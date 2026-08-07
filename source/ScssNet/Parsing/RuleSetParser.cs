using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class RuleSetParser(Lazy<SelectorListParser> selectorListParser, Lazy<RuleParser> ruleParser)
{
	internal RuleSet? Parse(TokenReader tokenReader)
	{
		var selectorList = selectorListParser.Value.Parse(tokenReader);
		if(selectorList == null)
			return null;

		var openBrace = tokenReader.Require(Symbol.OpenBrace);
		if(openBrace is null)
			return null;

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

		return new RuleSet(selectorList, openBrace, rules, closeBrace);
	}
}
