using ScssNet.Lexing;
using ScssNet.Structures;

namespace ScssNet.Parsing;

internal interface IRuleSetParser: IParser<RuleSet> { }

internal class RuleSetParser(ParserContext parserContext): IRuleSetParser
{
	private IRuleParser RuleParser => parserContext.GetParser<IRuleParser>();
	private ISelectorListParser SelectorListParser => parserContext.GetParser<ISelectorListParser>();
	private IBlockParser BlockParser => parserContext.GetParser<IBlockParser>();

	public RuleSet? Parse(TokenReader tokenReader)
	{
		var selectorList = SelectorListParser.Parse(tokenReader);
		if(selectorList == null)
			return null;

		var ruleBlock = BlockParser.Require(tokenReader);
		return new RuleSet(selectorList, ruleBlock);
	}
}
