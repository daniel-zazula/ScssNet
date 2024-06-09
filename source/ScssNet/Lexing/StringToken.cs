using System.Text;

namespace ScssNet.Lexing
{
	public class StringToken: IToken
	{
		public string Text { get; }

		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public IEnumerable<Issue> Issues => _issues;

		private readonly ICollection<Issue> _issues = [];

		internal StringToken(string text, SourceCoordinates start, SourceCoordinates end): this(text, start, end, []) { }

		private StringToken(string text, SourceCoordinates start, SourceCoordinates end, ICollection<Issue> issues)
		{
			Text = text;
			Start = start;
			End = end;
			_issues = issues;
		}

		internal static StringToken CreateMissing(SourceCoordinates start)
		{
			return new StringToken("", start, start, [new Issue(IssueType.Error, "Expected string")]);
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
