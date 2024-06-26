﻿using ScssNet.Lexing;

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

	internal class RuleParser(Lazy<ValueParser> valueParser) : ParserBase
	{
		internal Rule? Parse(TokenReader tokenReader)
		{
			var property = MatchIdentifier(tokenReader);
			if(property is null)
				return null;

			var colon = Require(tokenReader, Symbol.Colon);
			var value = valueParser.Value.Parse(tokenReader) ?? new MissingValue(tokenReader.GetCoordinates());
			var semiColon = Require(tokenReader, Symbol.SemiColon);

			return new Rule(property, colon, value, semiColon);
		}
	}
}
