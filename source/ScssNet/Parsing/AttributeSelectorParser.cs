using ScssNet.SourceElements;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class AttributeSelectorParser(Lazy<SelectorParser> selectorParser)
{
	internal AttributeSelector? Parse(ITokenReader tokenReader)
	{
		var openBracket = tokenReader.Match(Symbol.OpenBracket);
		if(openBracket is null)
			return null;

		var attribute = tokenReader.RequireIdentifier();

		var operatorSymbols = new[] { Symbol.Equals, Symbol.ContainsWord, Symbol.StartsWithWord, Symbol.StartsWith, Symbol.EndsWith, Symbol.Contains };
		var @operator = tokenReader.Match(operatorSymbols);
		StringToken? value = null;
		IdentifierToken? modifier = null;
		if(@operator != null)
		{
			value = tokenReader.RequireString();
			modifier = tokenReader.Match<IdentifierToken>();
		}

		var closeBracket = tokenReader.Require(Symbol.CloseBracket);

		var selectorQualifier = closeBracket.TrailingSeparator == Separator.Empty
			? selectorParser.Value.ParseQualifier(tokenReader)
			: default;;

		return new AttributeSelector(openBracket, attribute, @operator, value, modifier, closeBracket, selectorQualifier);
	}
}
