using System.Text;

namespace ScssNet.Lexing
{
	public class StringToken: IToken
	{
		public readonly string Text;

		public StringToken(string text)
		{
			Text = text;
		}
	}

	internal class StringParser
	{
		public StringToken? Parse(SourceReader reader)
		{
			if(reader.End || reader.Peek() != '\'')
				return null;

			var sb = new StringBuilder(reader.Read());
			while(!reader.End)
			{
				if (reader.Peek() == '\'' )
				{
					sb.Append(reader.Read());
					break;
				}
				sb.Append(reader.Read());
			};

			return new StringToken(sb.ToString());
		}
	}
}
