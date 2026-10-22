using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IClassSelectorParser: IParser<ClassSelector> { }

internal class ClassSelectorParser(ParserContext parserContext): IClassSelectorParser
{
	private ISelectorQualifierParser SelectorQualifierParser => parserContext.GetParser<ISelectorQualifierParser>();

	public ClassSelector? Parse(TokenReader tokenReader)
	{
		var dot = tokenReader.MatchSymbol(Symbol.Dot);
		if(dot is null)
			return null;

		var identifier = tokenReader.RequireIdentifier();

		var selectorQualifier = !identifier.HasTrailingSeparator()
			? SelectorQualifierParser.Parse(tokenReader)
			: default;

		return new ClassSelector(dot, identifier, selectorQualifier);
	}
}
