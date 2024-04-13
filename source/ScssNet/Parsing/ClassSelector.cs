using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class ClassSelector(SymbolToken dot, IdentifierToken identifier, ICompoundSelector? qualifier): ICompoundSelector
	{
		public SymbolToken Dot => dot;
		public IdentifierToken Identifier => identifier;
		public ICompoundSelector? Qualifier => qualifier;
	}

	internal class ClassSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser): ParserBase
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

			return new ClassSelector(hash!, identifier, compoundSelectorParser.Value.Parse(tokenReader));
		}
	}
}
