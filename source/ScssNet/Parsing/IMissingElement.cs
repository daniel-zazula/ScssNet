using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface IMissingElement
	{

	}

	public class MissingSymbolToken(Symbol symbol, int lineNumber, int columnNumber) : SymbolToken(symbol, lineNumber, columnNumber), IMissingElement
	{
	}
}
