using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface IMissingElement
	{

	}

	public class MissingSymbolToken(Symbol symbol, SourceCoordinates start) : SymbolToken(symbol, start, start), IMissingElement
	{
	}

	public class MissingValueToken(SourceCoordinates start) : ValueToken("", start, start), IMissingElement
	{
	}

	public class MissingIdentifierToken(SourceCoordinates start) : IdentifierToken("", start, start), IMissingElement
	{
	}

	public class MissingStringToken(SourceCoordinates start) : StringToken("", start, start), IMissingElement
	{
	}
}
