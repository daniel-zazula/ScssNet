using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class BlockCreation
{
	extension(Block)
	{
		internal static Block Create(ISourceElement? predecessor = null)
		{
			var openBrace = SymbolToken.Create(Symbol.OpenBrace, predecessor: predecessor);
			var rule = Rule.Create(predecessor: openBrace);
			var closeBrace = SymbolToken.Create(Symbol.CloseBrace, predecessor: rule);
			return new Block(openBrace, [rule], closeBrace);
		}
	}
}
