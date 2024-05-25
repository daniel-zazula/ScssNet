using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface IValue
	{
	}

	internal class ValueParser : ParserBase
	{
		internal IValue? Parse(TokenReader tokenReader)
		{
			var peeked = tokenReader.Peek();
			if (peeked is not IValue)
				return null;

			return (IValue)tokenReader.Read()!;
		}
	}
}
