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
		
		return (IAtRule?)ParseAtCharset(atSign, identifier, tokenReader)
			?? ParseAtImport(atSign, identifier, tokenReader)
			?? throw new NotImplementedException($"At-rule not implemented {identifier.Text}");
	}

	internal AtCharset? ParseAtCharset(SymbolToken atSign, IdentifierToken identifier, ITokenReader tokenReader)
	{
		if(!IdentifierMatch(identifier, "charset"))
			return null;

		var charsetName = tokenReader.RequireString();
		var semiColon = tokenReader.Match(Symbol.SemiColon);

		return new AtCharset(atSign, identifier, charsetName, semiColon);
	}

	internal AtImport? ParseAtImport(SymbolToken atSign, IdentifierToken identifier, ITokenReader tokenReader)
	{
		if(!IdentifierMatch(identifier, "import"))
			return null;

		var importPath = tokenReader.RequireString();
		var semiColon = tokenReader.Match(Symbol.SemiColon);

		return new AtImport(atSign, identifier, importPath, semiColon);
	}

	private bool IdentifierMatch(IdentifierToken identifier, string text)
	{
		return string.Equals(identifier.Text, text, StringComparison.OrdinalIgnoreCase);
	}
}
