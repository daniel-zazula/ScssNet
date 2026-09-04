namespace ScssNet.Tokens;

public record WhiteSpaceToken: IToken, ISeparatorToken
{
	public string Text { get; }

	public SourceSpan Span { get; }
	public IEnumerable<Issue> Issues => [];

	internal WhiteSpaceToken(string text, SourceSpan span)
	{
		Text = text;
		Span = span;
	}
}
