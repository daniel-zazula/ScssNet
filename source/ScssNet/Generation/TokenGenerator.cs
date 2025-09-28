using System.IO;
using ScssNet.Tokens;

namespace ScssNet.Generation;

internal class TokenGenerator
{
	public void Generate(SymbolToken symbolToken, TextWriter writer)
	{
		writer.Write(symbolToken.ToChars());
	}

	public void Generate(IdentifierToken identifierToken, TextWriter writer)
	{
		writer.Write(identifierToken.Text);
	}
}
