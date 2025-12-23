using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class SymbolParser
{
	public SymbolToken? Parse(ISourceReader reader, Separator? leadingSeparator, Func<Separator?> getTrailingSeparator)
	{
		if (reader.End)
			return null;

		var symbol = ParseTwoCharacterSymbol(reader) ?? ParseOneCharacterSymbol(reader);

		if (symbol is null)
			return null;

		var startCoordinates = reader.GetCoordinates();
		reader.Read(symbol >= Symbol.ContainsWord ? 2 : 1);

		return new SymbolToken
		(
			symbol.Value, startCoordinates, reader.GetCoordinates(), leadingSeparator, getTrailingSeparator()
		);
	}

	private Symbol? ParseTwoCharacterSymbol(ISourceReader reader)
	{
		return reader.Peek(2) switch
		{
			"~=" => Symbol.ContainsWord,
			"|=" => Symbol.StartsWithWord,
			"^=" => Symbol.StartsWith,
			"$=" => Symbol.EndsWith,
			"*=" => Symbol.Contains,
			_ => null
		};
	}

	private Symbol? ParseOneCharacterSymbol(ISourceReader reader)
	{
		return reader.Peek() switch
		{
			',' => Symbol.Comma,
			'.' => Symbol.Dot,
			'#' => Symbol.Hash,
			':' => Symbol.Colon,
			';' => Symbol.SemiColon,
			'{' => Symbol.OpenBrace,
			'}' => Symbol.CloseBrace,
			'[' => Symbol.OpenBracket,
			']' => Symbol.CloseBracket,
			'=' => Symbol.Equals,
			_ => null
		};
	}
}
