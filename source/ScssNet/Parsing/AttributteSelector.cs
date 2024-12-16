using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class AttributteSelector
	(
		SymbolToken openBracket, IdentifierToken attribute, SymbolToken? @operator, StringToken? value, IdentifierToken? modifier, SymbolToken closeBracket,
		ICompoundSelector? qualifier
	) : ISourceElement, ISelector, ICompoundSelector
	{
		public SymbolToken OpenBracket => openBracket;
		public IdentifierToken Attribute => attribute;
		public SymbolToken? Operator => @operator;
		public StringToken? Value => value;
		public IdentifierToken? Modifier => modifier;
		public SymbolToken CloseBracket => closeBracket;
		public ICompoundSelector? Qualifier => qualifier;

		public IEnumerable<Issue> Issues => SourceElement.List(openBracket, attribute, @operator, value, modifier, closeBracket, qualifier).ConcatIssues();

		public SourceCoordinates Start => openBracket.Start;

		public SourceCoordinates End => SourceElement.List(closeBracket, qualifier).LastEnd();
	}

	internal class AttributteSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser)
	{
		internal AttributteSelector? Parse(TokenReader tokenReader, bool skipWhitespace = true)
		{
			var openBracket = tokenReader.Match(Symbol.OpenBracket, skipWhitespace);
			if(openBracket is null)
				return null;

			var attribute = tokenReader.RequireIdentifier();

			var operatorSymbols = new[] { Symbol.Equals, Symbol.ContainsWord, Symbol.StartsWithWord, Symbol.StartsWith, Symbol.EndsWith, Symbol.Contains };
			var @operator = tokenReader.Match(operatorSymbols);
			StringToken? value = null;
			IdentifierToken? modifier = null;
			if(@operator != null)
			{
				value = tokenReader.RequireString();
				modifier = tokenReader.RequireIdentifier();
			}

			var closeBracket = tokenReader.Require(Symbol.CloseBracket);
			var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

			return new AttributteSelector(openBracket, attribute, @operator, value, modifier, closeBracket, compoundSelector);
		}
	}
}
