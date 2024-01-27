using System.Text;

namespace ScssNet.Lexing
{
	public class IdentifierToken: IToken
	{
		public readonly string Text;

		internal IdentifierToken(string text)
		{
			Text = text;
		}
	}

	internal class IdentifierParser
	{
		public IdentifierToken? Parse(SourceReader reader)
		{
			if (reader.End || !IsValidIdentifierChar(reader.Peek()))
				return null;

			var sb = new StringBuilder(reader.Read());
			while(!reader.End && IsValidIdentifierChar(reader.Peek()))
				sb.Append(reader.Read());

			return new IdentifierToken(sb.ToString());
		}

		private bool IsValidIdentifierChar(char c) => char.IsLetter(c) || c == '_' || c == '-';
	}
}
