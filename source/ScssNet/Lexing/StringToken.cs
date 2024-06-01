using System.Text;

namespace ScssNet.Lexing
{
	public class StringToken: IToken
	{
		public string Text { get; }

		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public ICollection<Issue> Issues => _issues.AsReadOnly();

		private readonly List<Issue> _issues = [];

		internal StringToken(string text, SourceCoordinates start, SourceCoordinates end)
		{
			Text = text;
			Start = start;
			End = end;
		}

		internal static StringToken CreateMissing(SourceCoordinates start)
		{
			var token = new StringToken("", start, start);
			token._issues.Add(new Issue(IssueType.Error, "Expected string"));
			return token;
		}
	}

	internal class StringParser
	{
		public StringToken? Parse(ISourceReader reader)
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

			return new StringToken(sb.ToString(), startCoordinates, reader.GetCoordinates());
		}

		private bool IsStringDelimiter(char c) => c == '\'' || c == '"';
	}
}
