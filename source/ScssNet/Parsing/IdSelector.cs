using System;
using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class IdSelector(SymbolToken hash, IdentifierToken identifier, ISelectorQualifier? qualifier) : ISelectorQualifier
	{
		public SymbolToken Hash { get; } = hash;
		public IdentifierToken Identifier { get; } = identifier;
		public ISelectorQualifier? Qualifier { get; } = qualifier;
	}

	internal class IdSelectorParser(Lazy<SelectorQualifierParser> selectorQualifierParser): ParserBase
	{
		internal IdSelector? Parse(TokenReader tokenReader, bool skipWhitespace = true)
		{
			if(!Match(tokenReader, Symbol.Hash, out var hash, skipWhitespace))
				return null;

			tokenReader.Read();

			if(tokenReader.Peek(false) is not IdentifierToken identifier)
				identifier = new MissingIdentifierToken(tokenReader.LineNumber, tokenReader.ColumnNumber);
			else
				tokenReader.Read();

			return new IdSelector(hash!, identifier, selectorQualifierParser.Value.Parse(tokenReader));
		}
	}
}
