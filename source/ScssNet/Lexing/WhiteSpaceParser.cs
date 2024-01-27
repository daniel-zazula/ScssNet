namespace ScssNet.Lexing
{
	public class WhiteSpaceToken: IToken
	{
		internal static readonly WhiteSpaceToken Instance = new();

		private WhiteSpaceToken()
		{

		}
	}

	internal class WhiteSpaceParser
	{
		public WhiteSpaceToken? Parse(SourceReader reader)
		{
			if (reader.End || !char.IsWhiteSpace(reader.Peek()))
				return null;

			reader.Read();
			while(!reader.End && char.IsWhiteSpace(reader.Peek()))
				reader.Read();

			return WhiteSpaceToken.Instance;
		}
	}
}
