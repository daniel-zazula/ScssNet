using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class TagSelectorCreation
{
	extension(TagSelector)
	{
		internal static TagSelector Create(ISourceElement? predecessor = null)
		{
			var identifier = IdentifierToken.Create("h2", predecessor: predecessor);
			return new TagSelector(identifier, null);
		}
	}
}

internal static class RuleExtensions
{
	extension(Rule)
	{
		internal static Rule Create(ISourceElement? predecessor = null)
		{
			var property = IdentifierToken.Create("prop", predecessor: predecessor);
			var colon = SymbolToken.Create(Symbol.Colon, predecessor: property);
			var value = IdentifierToken.Create("val", predecessor: colon);
			var semiColon = SymbolToken.Create(Symbol.SemiColon, predecessor: value);

			return new Rule(property, colon, value, null, semiColon);
		}
	}
}
