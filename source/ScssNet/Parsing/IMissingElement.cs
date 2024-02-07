using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface IMissingElement
	{

	}

	public class MissingSymbolToken(Symbol symbol, int lineNumber, int columnNumber) : SymbolToken(symbol, lineNumber, columnNumber), IMissingElement
	{
	}

	public class MissingValueToken(int lineNumber, int columnNumber) : ValueToken("", lineNumber, columnNumber), IMissingElement
	{
	}

	public class MissingIdentifierToken(int lineNumber, int columnNumber) : IdentifierToken("", lineNumber, columnNumber), IMissingElement
	{
	}
}
