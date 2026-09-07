using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class AtRuleParser
(
	Lazy<ValueParser> valueParser, Lazy<BlockParser> blockParser, Lazy<MediaQueryParser> mediaQueryParser
)
{
	internal IAtRule? Parse(TokenReader tokenReader)
	{
		var atSign = tokenReader.Match(Symbol.At);
		if(atSign is null)
			return null;

		var atKeywordToken = tokenReader.RequireKeyword<AtKeywordToken>();

		return atKeywordToken.Keyword switch
		{
			AtKeyword.Charset => ParseAtCharset(atSign, atKeywordToken, tokenReader),
			AtKeyword.Import => ParseAtImport(atSign, atKeywordToken, tokenReader),
			AtKeyword.Media => ParseAtMedia(atSign, atKeywordToken, tokenReader),
			_ => throw new NotImplementedException($"At-rule '{atKeywordToken.Keyword}' is not implemented.")
		};
	}

	internal AtCharset ParseAtCharset(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		var charsetName = tokenReader.RequireString();
		var semiColon = tokenReader.Match(Symbol.SemiColon);

		return new AtCharset(atSign, atKeywordToken, charsetName, semiColon);
	}

	internal AtImport ParseAtImport(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		var importPath = valueParser.Value.Parse(tokenReader) ?? tokenReader.RequireString();
		var semiColon = tokenReader.Match(Symbol.SemiColon);

		return new AtImport(atSign, atKeywordToken, importPath, semiColon);
	}

	internal AtMedia? ParseAtMedia(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		var mediaQuery = mediaQueryParser.Value.Require(tokenReader);
		var block = blockParser.Value.Require(tokenReader);

		return new AtMedia(atSign, atKeywordToken, mediaQuery, block);
	}
}
