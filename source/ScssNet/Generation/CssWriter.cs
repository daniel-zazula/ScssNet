using System.IO;
using ScssNet.Tokens;

namespace ScssNet.Generation;

internal class CssWriter(TextWriter textWriter)
{
	public void Write(string css)
	{
		textWriter.Write(css);
	}

	public void Write(SymbolToken symbolToken)
	{
		textWriter.Write(symbolToken.ToChars());
	}

	public void Write(IdentifierToken identifierToken)
	{
		textWriter.Write(identifierToken.Text);
	}

	public void Write(StringToken stringToken)
	{
		textWriter.Write(stringToken.Text);
	}

	internal void Write(HexValueToken hexValueToken)
	{
		textWriter.Write(hexValueToken.Value);
	}
}
