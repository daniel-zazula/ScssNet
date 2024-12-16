using System.Text;
using ScssNet.Parsing;

namespace ScssNet.Lexing
{
	public class IdentifierToken: IValueToken
	{
		public string Text { get; }

		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public IEnumerable<Issue> Issues => _issues;

		private readonly ICollection<Issue> _issues = [];

		internal IdentifierToken(string text, SourceCoordinates start, SourceCoordinates end): this(text, start, end, []) { }

		private IdentifierToken(string text, SourceCoordinates start, SourceCoordinates end, ICollection<Issue> issues)
		{
			Text = text;
			Start = start;
			End = end;
			_issues = issues;
		}

		internal static IdentifierToken CreateMissing(SourceCoordinates start)
		{
			return new IdentifierToken("", start, start, [new Issue(IssueType.Error, "Expected identifier")]);
		}
	}

	internal class IdentifierParser
	{
		public IdentifierToken? Parse(ISourceReader reader)
		{
			if(reader.End || !IsIdentifierStart(reader.Peek(3)))
				return null;

			var startCoordinates = reader.GetCoordinates();

			var sb = new StringBuilder();
			sb.Append(reader.Read());
			while(!reader.End && IsIdentifierChar(reader.Peek()))
				sb.Append(reader.Read());

			return new IdentifierToken(sb.ToString(), startCoordinates, reader.GetCoordinates());
		}

		private bool IsIdentifierStart(string peeked)
		{
			var firstChar = peeked[0];
			return char.IsLetter(firstChar) || firstChar == '_'
				|| (peeked.Length > 1 && firstChar == '-' && char.IsLetter(peeked[1]))
				|| (peeked.Length > 2 && peeked.Substring(2) == "--" && char.IsLetter(peeked[2]));
		}

		private bool IsIdentifierChar(char c)
		{
			return char.IsLetter(c) || c == '_' || c == '-';
		}
	}
}
