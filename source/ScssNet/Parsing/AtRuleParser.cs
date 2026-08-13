using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class AtRuleParser(Lazy<ValueParser> valueParser, Lazy<BlockParser> blockParser)
{
	internal IAtRule? Parse(TokenReader tokenReader)
	{
		var atSign = tokenReader.Match(Symbol.At);
		if(atSign is null)
			return null;

		return (IAtRule?)ParseAtCharset(atSign, tokenReader)
			?? (IAtRule?)ParseAtImport(atSign, tokenReader)
			?? (IAtRule?)ParseAtMedia(atSign, tokenReader)
			?? throw new NotImplementedException($"Token after at-sign does not match any known at-rules.");
	}

	internal AtCharset? ParseAtCharset(SymbolToken atSign, TokenReader tokenReader)
	{
		var keyword = tokenReader.Match(Keyword.Charset);
		if(keyword is null)
			return null;

		var charsetName = tokenReader.RequireString();
		var semiColon = tokenReader.Match(Symbol.SemiColon);

		return new AtCharset(atSign, keyword, charsetName, semiColon);
	}

	internal AtImport? ParseAtImport(SymbolToken atSign, TokenReader tokenReader)
	{
		var keyword = tokenReader.Match(Keyword.Import);
		if(keyword is null)
			return null;

		var importPath = valueParser.Value.Parse(tokenReader) ?? tokenReader.RequireString();
		var semiColon = tokenReader.Match(Symbol.SemiColon);

		return new AtImport(atSign, keyword, importPath, semiColon);
	}

	internal AtMedia? ParseAtMedia(SymbolToken atSign, TokenReader tokenReader)
	{
		var keyword = tokenReader.Match(Keyword.Media);
		if(keyword is null)
			return null;

		var mediaQuery = valueParser.Value.Parse(tokenReader) ?? tokenReader.RequireIdentifier();

		var block = blockParser.Value.Require(tokenReader);

		return new AtMedia(atSign, keyword, mediaQuery, block);
	}
}
