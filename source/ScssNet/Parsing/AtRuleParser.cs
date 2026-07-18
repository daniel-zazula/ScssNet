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
		
		return (IAtRule?)ParseAtCharset(atSign, tokenReader)
			?? ParseAtImport(atSign, tokenReader)
			?? throw new NotImplementedException($"At-rule does not match any known keywords.");
	}

	internal AtCharset? ParseAtCharset(SymbolToken atSign, ITokenReader tokenReader)
	{
		var keyword = tokenReader.Match(Keyword.Charset);
		if(keyword is null)
			return null;

		var charsetName = tokenReader.RequireString();
		var semiColon = tokenReader.Match(Symbol.SemiColon);

		return new AtCharset(atSign, keyword, charsetName, semiColon);
	}

	internal AtImport? ParseAtImport(SymbolToken atSign, ITokenReader tokenReader)
	{
		var keyword = tokenReader.Match(Keyword.Import);
		if(keyword is null)
			return null;

		var importPath = tokenReader.RequireString();
		var semiColon = tokenReader.Match(Symbol.SemiColon);

		return new AtImport(atSign, keyword, importPath, semiColon);
	}
}
