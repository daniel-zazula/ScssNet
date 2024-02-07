using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class IdSelector(SymbolToken hash, IdentifierToken identifier)
	{
		public SymbolToken Hash { get; } = hash;
		public IdentifierToken Identifier { get; } = identifier;
	}

	internal class IdSelectorParser: ParserBase
	{
		internal IdSelector? Parse(TokenReader tokenReader)
		{
			var hash = Match(tokenReader, Symbol.Hash);
			if(hash is null)
				return null;

			tokenReader.Read();

			if(tokenReader.Peek() is not IdentifierToken identifier)
				identifier = new MissingIdentifierToken(tokenReader.LineNumber, tokenReader.ColumnNumber);
			else
				tokenReader.Read();

			return new IdSelector(hash, identifier);
		}
	}
}
