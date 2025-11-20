namespace ScssNet.Tokens;

public interface ISeparatedToken: IToken
{
	Separator? LeadingSeparator { get; }

	Separator? TrailingSeparator { get; }
}
