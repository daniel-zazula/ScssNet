using ScssNet.Lexing;
using ScssNet.Structures;

namespace ScssNet.Parsing;

internal interface IStatementParser: IParser<IStatement> { }

internal class StatementParser(ParserContext parserContext): IStatementParser
{
	private IAtRuleParser AtRuleParser => parserContext.GetParser<IAtRuleParser>();
	private IRuleSetParser RuleSetParser => parserContext.GetParser<IRuleSetParser>();

	public IStatement? Parse(TokenReader tokenReader)
	{
		return (IStatement?)AtRuleParser.Parse(tokenReader)
			?? RuleSetParser.Parse(tokenReader);
	}
}
