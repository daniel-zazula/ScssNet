using ScssNet.Lexing;
using ScssNet.SourceElements;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class ValueParser
{
	internal IValue? Parse(ITokenReader tokenReader)
	{
		return tokenReader.Match<IValueToken>();
		// TBA more comples values like function calls
	}
}
