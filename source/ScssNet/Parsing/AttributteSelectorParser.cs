using ScssNet.SourceElements;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class AttributteSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser)
{
	internal AttributteSelector? Parse(TokenReader tokenReader, bool skipWhitespace = true)
	{
		var openBracket = tokenReader.Match(Symbol.OpenBracket, skipWhitespace);
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
			modifier = tokenReader.RequireIdentifier();
		}

		var closeBracket = tokenReader.Require(Symbol.CloseBracket);
		var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

		return new AttributteSelector(openBracket, attribute, @operator, value, modifier, closeBracket, compoundSelector);
	}
}
