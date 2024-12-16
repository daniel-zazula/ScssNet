using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class Rule(IdentifierToken property, SymbolToken colon, IValue value, SymbolToken semiColon) : ISourceElement
	{
		public IdentifierToken Property => property;
		public SymbolToken Colon => colon;
		public IValue Value => value;
		public SymbolToken SemiColon => semiColon;

		public IEnumerable<Issue> Issues => SourceElement.List(Property, Colon, Value, SemiColon).ConcatIssues();

		public SourceCoordinates Start => Property.Start;

		public SourceCoordinates End => SemiColon.End;
	}

	internal class RuleParser(Lazy<ValueParser> valueParser)
	{
		internal Rule? Parse(TokenReader tokenReader)
		{
			var property = tokenReader.Match<IdentifierToken>();
			if(property is null)
				return null;

			var colon = tokenReader.Require(Symbol.Colon);
			var value = valueParser.Value.Parse(tokenReader) ?? new MissingValue(tokenReader.GetCoordinates());
			var semiColon = tokenReader.Require(Symbol.SemiColon);

			return new Rule(property, colon, value, semiColon);
		}
	}
}
