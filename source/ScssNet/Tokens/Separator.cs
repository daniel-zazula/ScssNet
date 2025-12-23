namespace ScssNet.Tokens;

public record Separator
{
	public static Separator Empty = new Separator([]);

	public IReadOnlyCollection<ISeparatorToken> Tokens { get; }

	public Separator(ICollection<ISeparatorToken> tokens)
	{
		Tokens = [..tokens];
	}
}
