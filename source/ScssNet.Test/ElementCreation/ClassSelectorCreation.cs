using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class ClassSelectorCreation
{
	extension(ClassSelector)
	{
		internal static ClassSelector Create(ISourceElement? predecessor = null)
		{
			var dot = SymbolToken.Create(Symbol.Dot, predecessor: predecessor);
			var identifier = IdentifierToken.Create("my-class", predecessor: dot);

			return new ClassSelector(dot, identifier, null);
		}
	}
}
