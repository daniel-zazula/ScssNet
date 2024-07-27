using System.Text;
using ScssNet.Parsing;

namespace ScssNet.Lexing
{
	public class IdentifierToken: IToken, IValue
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
