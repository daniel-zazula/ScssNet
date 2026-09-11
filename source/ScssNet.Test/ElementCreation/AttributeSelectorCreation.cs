using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class AttributeSelectorCreation
{
	extension(AttributeSelector)
	{
		internal static AttributeSelector Create
		(
			ISourceElement? predecessor = null
		)
		{
			var openBracket = SymbolToken.Create(Symbol.OpenBracket, predecessor: predecessor);
			var attribute = IdentifierToken.Create("attr", predecessor: openBracket);
			var equalSign = SymbolToken.Create(Symbol.Equals, predecessor: attribute);
			var value = StringToken.Create(@"""some-value""", predecessor: equalSign);
			var closeBracket = SymbolToken.Create(Symbol.CloseBracket, predecessor: value);

			return new AttributeSelector(openBracket, attribute, equalSign, value, null, closeBracket, null);
		}
	}
}
