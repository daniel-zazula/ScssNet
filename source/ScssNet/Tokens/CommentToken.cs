namespace ScssNet.Tokens;

public record CommentToken: IToken, ISeparatorToken
{
	public string Text { get; }

	public SourceSpan Span { get; }
	public IEnumerable<Issue> Issues => [];

	internal CommentToken(string text, SourceSpan span)
	{
		Text = text;
		Span = span;
	}
}
