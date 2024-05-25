using System.Text;

namespace ScssNet.Lexing
{
	public class IdentifierToken: IToken
	{
		public string Text { get; }

		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public ICollection<Issue> Issues => _issues.AsReadOnly();

		private readonly List<Issue> _issues = [];

		internal IdentifierToken(string text, SourceCoordinates start, SourceCoordinates end)
		{
			Start = start;
			End = end;
			Text = text;
		}

		internal static IdentifierToken CreateMissing(SourceCoordinates start)
		{
			var token = new IdentifierToken("", start, start);
			token._issues.Add(new Issue(IssueType.Error, "Expected identifier"));
			return token;
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
