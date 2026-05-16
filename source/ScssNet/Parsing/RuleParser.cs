using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class RuleParser(Lazy<ValueParser> valueParser)
{
	internal Rule? Parse(ITokenReader tokenReader)
	{
		var property = tokenReader.Match<IdentifierToken>();
		if(property is null)
			return null;

		var colon = tokenReader.Require(Symbol.Colon);
		var value = valueParser.Value.Parse(tokenReader) ?? throw new NotImplementedException("Handle missing value");
		var semiColon = tokenReader.Match(Symbol.SemiColon);

		return new Rule(property, colon, value, semiColon);
	}
}
