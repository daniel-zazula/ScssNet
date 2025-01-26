namespace ScssNet.Tokens
{
	public class CommentToken : IToken
	{
		public string Text { get; }

		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public IEnumerable<Issue> Issues => [];

		internal CommentToken(string text, SourceCoordinates start, SourceCoordinates end)
		{
			Text = text;
			Start = start;
			End = end;
		}
	}
}
