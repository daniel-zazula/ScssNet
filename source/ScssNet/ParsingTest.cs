using System.IO;
using System.Text;
using ScssNet.Lexing;

namespace ScssNet
{
    public class ParsingTest
	{
		private readonly SourceReader SourceReader;
		private readonly TokenReader TokenReader;

		public ParsingTest(string path)
		{
			SourceReader = new SourceReader(new StreamReader(path));
			TokenReader = new TokenReader(SourceReader);
		}

		public string? DescribleNextToken()
		{
			var token = TokenReader.Read();
			while (token != null)
			{
				if(token is IdentifierToken identifier)
					return $"Identifier: {identifier.Text}";

				if(token is ValueToken value)
					return $"Value: {value.Text}";

				if(token is SymbolToken symbol)
					return $"Symbol: {symbol}";

				if(token is StringToken stringToken)
					return $"String: {stringToken.Text}";

				if(token is CommentToken commentToken)
					return $"Comment: {commentToken.Text}";

				if(token is WhiteSpaceToken)
					return "Spacing";
			}

			if(!SourceReader.End)
			{
				var sb = new StringBuilder();
				for (int i = 0; i < 20 && !SourceReader.End; i++)
					sb.Append(SourceReader.Read());

				while(!SourceReader.End)
					SourceReader.Read();

				return "Could not parse more tokens, remaining text: " + sb.ToString();
			}

			return null;
		}
	}
}
