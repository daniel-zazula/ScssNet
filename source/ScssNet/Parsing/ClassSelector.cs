using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class ClassSelector(SymbolToken dot, IdentifierToken identifier, ICompoundSelector? qualifier): ISelector, ICompoundSelector
	{
		public SymbolToken Dot => dot;
		public IdentifierToken Identifier => identifier;
		public ICompoundSelector? Qualifier => qualifier;
	}

	internal class ClassSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser): ParserBase
	{
		internal ClassSelector? Parse(TokenReader tokenReader, bool skipWhitespace = true)
		{
			var dot = Match(tokenReader, Symbol.Hash, skipWhitespace);
			if(dot is null)
				return null;

			var identifier = RequireIdentifier(tokenReader, false);
			var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

			return new ClassSelector(dot, identifier, compoundSelector);
		}
	}
}
