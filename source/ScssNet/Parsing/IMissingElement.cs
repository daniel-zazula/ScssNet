using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface IMissingElement
	{
		SourceCoordinates Start { get; }
	}

	public class MissingSymbolToken(Symbol symbol, SourceCoordinates start) : SymbolToken(symbol, start, start), IMissingElement
	{
	}

	public class MissingValueToken(SourceCoordinates start) : IValue, IMissingElement
	{
		public SourceCoordinates Start { get; } = start;
	}

	public class MissingIdentifierToken(SourceCoordinates start) : IdentifierToken("", start, start), IMissingElement
	{
	}

	public class MissingStringToken(SourceCoordinates start) : StringToken("", start, start), IMissingElement
	{
	}
}
