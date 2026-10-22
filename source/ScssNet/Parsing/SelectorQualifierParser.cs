using ScssNet.Lexing;
using ScssNet.Structures;

namespace ScssNet.Parsing;

internal interface ISelectorQualifierParser: IParser<ISelectorQualifier> { }

internal class SelectorQualifierParser(ParserContext parserContext): ISelectorQualifierParser
{
	private IIdSelectorParser IdSelectorParser => parserContext.GetParser<IIdSelectorParser>();
	private IClassSelectorParser ClassSelectorParser => parserContext.GetParser<IClassSelectorParser>();
	private IAttributeSelectorParser AttributeSelectorParser => parserContext.GetParser<IAttributeSelectorParser>();
	private IPseudoClassSelectorParser PseudoClassSelectorParser => parserContext.GetParser<IPseudoClassSelectorParser>();
	private IPseudoElementSelectorParser PseudoElementSelectorParser => parserContext.GetParser<IPseudoElementSelectorParser>();


	public ISelectorQualifier? Parse(TokenReader tokenReader)
	{
		return (ISelectorQualifier?)IdSelectorParser.Parse(tokenReader)
			?? (ISelectorQualifier?)ClassSelectorParser.Parse(tokenReader)
			?? (ISelectorQualifier?)AttributeSelectorParser.Parse(tokenReader)
			?? (ISelectorQualifier?)PseudoClassSelectorParser.Parse(tokenReader)
			?? PseudoElementSelectorParser.Parse(tokenReader);
	}
}
