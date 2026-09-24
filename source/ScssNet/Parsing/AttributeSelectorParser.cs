using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class AttributeSelectorParser(Lazy<SelectorParser> selectorParser)
{
	internal AttributeSelector? Parse(TokenReader tokenReader)
	{
		var openBracket = tokenReader.MatchSymbol(Symbol.OpenBracket);
		if(openBracket is null)
			return null;

		var attribute = tokenReader.RequireIdentifier();

		var operators = new[] { Symbol.Equals, Symbol.ContainsWord, Symbol.StartsWithWord, Symbol.StartsWith, Symbol.EndsWith, Symbol.Contains };
		var @operator = tokenReader.MatchSymbol(operators);
		StringToken? value = null;
		IdentifierToken? modifier = null;
		if(@operator != null)
		{
			value = tokenReader.RequireString();
			modifier = tokenReader.MatchIdentifier();
		}

		var closeBracket = tokenReader.RequireSymbol(Symbol.CloseBracket);

		var selectorQualifier = closeBracket.TrailingSeparator == Separator.Empty
			? selectorParser.Value.ParseQualifier(tokenReader)
			: default;;

		return new AttributeSelector(openBracket, attribute, @operator, value, modifier, closeBracket, selectorQualifier);
	}
}
