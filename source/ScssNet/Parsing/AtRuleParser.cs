using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class AtRuleParser
{
	internal IAtRule? Parse(ITokenReader tokenReader)
	{
		var atSign = tokenReader.Match(Symbol.At);
		if(atSign is null)
			return null;

		var identifier = tokenReader.RequireIdentifier();
		
		return ParseAtCharset(atSign, identifier, tokenReader)
			?? throw new NotImplementedException($"At-rule not implemented {identifier.Text}");
	}

	internal AtCharset? ParseAtCharset(SymbolToken atSign, IdentifierToken identifier, ITokenReader tokenReader)
	{
		if(!IdentifierMatch(identifier, "charset"))
			return null;

		var strToken = tokenReader.RequireString();
		var semiColon = tokenReader.Match(Symbol.SemiColon);

		return new AtCharset(atSign, identifier, strToken, semiColon);
	}

	private bool IdentifierMatch(IdentifierToken identifier, string text)
	{
		return string.Equals(identifier.Text, text, StringComparison.OrdinalIgnoreCase);
	}
}
