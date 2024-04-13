using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class Rule(IdentifierToken property, SymbolToken colon, ValueToken value)
	{
		public IdentifierToken Property => property;
		public SymbolToken Colon => colon;
		public ValueToken Value => value;
	}

	internal class RuleParser: ParserBase
	{
		internal Rule? Parse(TokenReader tokenReader)
		{
			if(tokenReader.Peek() is not IdentifierToken property)
				return null;

			tokenReader.Read();
			var colon = Require(tokenReader, Symbol.Colon);

			if(tokenReader.Peek() is not ValueToken value)
				value = new MissingValueToken(tokenReader.LineNumber, tokenReader.ColumnNumber);
			else
				tokenReader.Read();

			return new Rule(property, colon, value);
		}
	}
}
