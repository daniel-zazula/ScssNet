using System.Text;

namespace ScssNet.Lexing
{
	public class IdentifierToken: IToken
	{
		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public string Text { get; }

		internal IdentifierToken(string text, SourceCoordinates start, SourceCoordinates end)
		{
			Start = start;
			End = end;
			Text = text;
		}
	}

	internal class IdentifierParser
	{
		public IdentifierToken? Parse(ISourceReader reader)
		{
			if (reader.End || !IsValidIdentifierChar(reader.Peek()))
				return null;

			var startCoordinates = reader.GetCoordinates();

			var sb = new StringBuilder();
			sb.Append(reader.Read());
			while(!reader.End && IsValidIdentifierChar(reader.Peek()))
				sb.Append(reader.Read());

			return new IdentifierToken(sb.ToString(), startCoordinates, reader.GetCoordinates());
		}

		private bool IsValidIdentifierChar(char c) => char.IsLetter(c) || c == '_' || c == '-';
	}
}
