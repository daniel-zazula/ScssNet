using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class AttributteSelector
	(
		SymbolToken openBracket, IdentifierToken attribute, SymbolToken? @operator, StringToken? value, IdentifierToken? modifier, SymbolToken closeBracket,
		ICompoundSelector? qualifier
	) : ISelector, ICompoundSelector
	{
		public SymbolToken OpenBracket => openBracket;
		public IdentifierToken Attribute => attribute;
		public SymbolToken? Operator => @operator;
		public StringToken? Value => value;
		public IdentifierToken? Modifier => modifier;
		public SymbolToken CloseBracket => closeBracket;
		public ICompoundSelector? Qualifier => qualifier;
	}

	internal class AttributteSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser) : ParserBase
	{
		internal AttributteSelector? Parse(TokenReader tokenReader, bool skipWhitespace = true)
		{
			var openBracket = Match(tokenReader, Symbol.OpenBracket, skipWhitespace);
			if(openBracket is null)
				return null;

			var attribute = RequireIdentifier(tokenReader);

			var operatorSymbols = new[] { Symbol.Equals, Symbol.ContainsWord, Symbol.StartsWithWord, Symbol.StartsWith, Symbol.EndsWith, Symbol.Contains };
			var @operator = Match(tokenReader, operatorSymbols);
			StringToken? value = null;
			IdentifierToken? modifier = null;
			if(@operator != null)
			{
				value = RequireString(tokenReader);
				modifier = RequireIdentifier(tokenReader);
			}

			var closeBracket = Require(tokenReader, Symbol.CloseBracket);
			var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

			return new AttributteSelector(openBracket, attribute, @operator, value, modifier, closeBracket, compoundSelector);
		}
	}
}
