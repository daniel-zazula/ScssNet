using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class ClassSelector(SymbolToken dot, IdentifierToken identifier, ISelectorQualifier? qualifier): ISelectorQualifier
	{
		public SymbolToken Dot { get; } = dot;
		public IdentifierToken Identifier { get; } = identifier;
		public ISelectorQualifier? Qualifier { get; } = qualifier;
	}

	internal class ClassSelectorParser(Lazy<SelectorQualifierParser> selectorQualifierParser): ParserBase
	{
		internal ClassSelector? Parse(TokenReader tokenReader, bool skipWhitespace = true)
		{
			if(Match(tokenReader, Symbol.Hash, out var hash, skipWhitespace))
				return null;

			tokenReader.Read();

			if(tokenReader.Peek(false) is not IdentifierToken identifier)
				identifier = new MissingIdentifierToken(tokenReader.LineNumber, tokenReader.ColumnNumber);
			else
				tokenReader.Read();

			return new ClassSelector(hash!, identifier, selectorQualifierParser.Value.Parse(tokenReader));
		}
	}
}
