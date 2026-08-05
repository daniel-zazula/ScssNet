using ScssNet.Lexing;
using ScssNet.Structures;

namespace ScssNet.Parsing;

internal class StatementParser(Lazy<AtRuleParser> atCharsetParser, Lazy<RuleSetParser> ruleSetParser)
{
	internal IStatement? Parse(TokenReader tokenReader)
	{
		return (IStatement?)atCharsetParser.Value.Parse(tokenReader)
			?? ruleSetParser.Value.Parse(tokenReader);
	}
}
