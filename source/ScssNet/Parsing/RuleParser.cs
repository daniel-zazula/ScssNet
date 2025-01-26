using ScssNet.Lexing;
using ScssNet.SourceElements;
using ScssNet.Tokens;

namespace ScssNet.Parsing
{
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
