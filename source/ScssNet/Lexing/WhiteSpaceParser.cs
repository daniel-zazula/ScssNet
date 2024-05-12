using System.Text;

namespace ScssNet.Lexing
{
	public class WhiteSpaceToken: IToken
	{
		public string Text { get; }
		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }

		internal WhiteSpaceToken(string text, SourceCoordinates start, SourceCoordinates end)
		{
			Text = text;
			Start = start;
			End = end;
		}
	}

	internal class WhiteSpaceParser
	{
		public WhiteSpaceToken? Parse(ISourceReader reader)
		{
			if (reader.End || !char.IsWhiteSpace(reader.Peek()))
				return null;

			var startCoordinates = reader.GetCoordinates();

			var sb = new StringBuilder();
			sb.Append(reader.Read());
			while(!reader.End && char.IsWhiteSpace(reader.Peek()))
				sb.Append(reader.Read());

			return new WhiteSpaceToken(sb.ToString(), startCoordinates, reader.GetCoordinates());
		}
	}
}
