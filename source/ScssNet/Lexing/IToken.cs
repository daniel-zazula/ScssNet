namespace ScssNet.Lexing
{
	public interface IToken
	{
		int LineNumber { get; }
		int ColumnNumber { get; }
	}
}
