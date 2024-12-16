using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface IValue : ISourceElement { }

	public class MissingValue: IValue
	{
		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public IEnumerable<Issue> Issues { get; }

		internal MissingValue(SourceCoordinates start)
		{
			Start = start;
			End = start;
			Issues = [new Issue(IssueType.Error, "Missing value (measure unit, string or function)")];
		}
	}

	internal class ValueParser
	{
		internal IValue? Parse(TokenReader tokenReader)
		{
			return tokenReader.Match<IValueToken>();
			// TBA more comples values like function calls
		}
	}
}
