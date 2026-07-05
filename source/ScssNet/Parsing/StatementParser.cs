using ScssNet.Lexing;
using ScssNet.Structures;

namespace ScssNet.Parsing;

internal class StatementParser(Lazy<AtRuleParser> charsetParser)
{
	internal IStatement? Parse(ITokenReader tokenReader)
	{
		return ruleSetParser.Value.Parse(tokenReader);
	}
}
