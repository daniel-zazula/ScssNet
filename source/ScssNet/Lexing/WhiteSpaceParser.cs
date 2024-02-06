namespace ScssNet.Lexing
{
	public class WhiteSpaceToken: IToken
	{
		public int LineNumber { get; }
		public int ColumnNumber { get; }

		internal WhiteSpaceToken(int lineNumber, int columnNumber)
		{
			LineNumber = lineNumber;
			ColumnNumber = columnNumber;
		}
	}

	internal class WhiteSpaceParser
	{
		public WhiteSpaceToken? Parse(SourceReader reader)
		{
			if (reader.End || !char.IsWhiteSpace(reader.Peek()))
				return null;

			var lineNumber = reader.LineNumber;
			var columnNumber = reader.ColumnNumber;

			reader.Read();
			while(!reader.End && char.IsWhiteSpace(reader.Peek()))
				reader.Read();

			return new WhiteSpaceToken(lineNumber, columnNumber);
		}
	}
}
