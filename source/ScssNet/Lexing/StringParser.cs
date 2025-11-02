using System.Text;
using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class StringParser
{
	public StringToken? Parse(ISourceReader reader, Separator? leadingSeparator, Func<Separator?> getTrailingSeparator)
	{
		if(reader.End || !IsStringDelimiter(reader.Peek()))
			return null;

		var startCoordinates = reader.GetCoordinates();

		var sb = new StringBuilder();
		var startingDelimiter = reader.Read();
		sb.Append(startingDelimiter);
		var previousChar = startingDelimiter;
		while(!reader.End)
		{
			if (reader.Peek() == startingDelimiter && previousChar != '\\')
			{
				sb.Append(reader.Read());
				break;
			}
			previousChar = reader.Read();
			sb.Append(previousChar);
		};

		return new StringToken
		(
			sb.ToString(), startCoordinates, reader.GetCoordinates(), leadingSeparator, getTrailingSeparator()
		);
	}

	private bool IsStringDelimiter(char c) => c == '\'' || c == '"';
}
