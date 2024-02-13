using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class ClassSelector(SymbolToken dot, IdentifierToken identifier)
	{
		public SymbolToken Dot { get; } = dot;
		public IdentifierToken Identifier { get; } = identifier;
	}

	internal class ClassSelectorParser : ParserBase
	{
		internal ClassSelector? Parse(TokenReader tokenReader)
		{
			var hash = Match(tokenReader, Symbol.Hash);
			if(hash is null)
				return null;

			tokenReader.Read();

			if(tokenReader.Peek(false) is not IdentifierToken identifier)
				identifier = new MissingIdentifierToken(tokenReader.LineNumber, tokenReader.ColumnNumber);
			else
				tokenReader.Read();

			return new ClassSelector(hash, identifier);
		}
	}
}
