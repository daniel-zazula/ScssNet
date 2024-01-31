using System;
using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	internal abstract class ParserBase
	{
		protected SymbolToken Require(TokenReader tokenReader, SymbolToken requiredToken)
		{
			if(tokenReader.Peek() is not SymbolToken symbolToken || symbolToken != requiredToken)
				throw new NotImplementedException();

			return symbolToken;
		}
	}
}
