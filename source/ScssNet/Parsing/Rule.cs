using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class Rule(IdentifierToken property, SymbolToken colon, ValueToken value, SymbolToken semiColon)
	{
		public IdentifierToken Property => property;
		public SymbolToken Colon => colon;
		public ValueToken Value => value;
		public SymbolToken SemiColon => semiColon;
	}

	internal class RuleParser: ParserBase
	{
		internal Rule? Parse(TokenReader tokenReader)
		{
			var property = MatchIdentifier(tokenReader);
			if(property is null)
				return null;

			var colon = Require(tokenReader, Symbol.Colon);

			if(tokenReader.Peek() is not ValueToken value)
				value = new MissingValueToken(tokenReader.LineNumber, tokenReader.ColumnNumber);
			else
				tokenReader.Read();

			var semiColon = Require(tokenReader, Symbol.SemiColon);

			return new Rule(property, colon, value, semiColon);
		}
	}
}
