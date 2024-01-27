using System.Text;

namespace ScssNet.Lexing
{
	public class ValueToken
	{
		public readonly string Text;

		public ValueToken(string text)
		{
			Text = text;
		}
	}

	internal class ValueParser
	{
		public ValueToken? Parse(SourceReader reader)
		{
			if (reader.End || !char.IsDigit(reader.Peek()))
				return null;

			var sb = new StringBuilder(reader.Read());
			while(!reader.End && char.IsDigit(reader.Peek()))
				sb.Append(reader.Read());

			if(!reader.End || reader.Peek() == '%')
				sb.Append(reader.Read());
			else
				while(!reader.End && char.IsLetter(reader.Peek()))
					sb.Append(reader.Read());

			return sb.Length > 0 ? new ValueToken(sb.ToString()) : null;
		}
	}
}
