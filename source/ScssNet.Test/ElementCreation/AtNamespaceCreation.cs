using System;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class AtNamespaceCreation
{
	extension(AtNamespace)
	{
		internal static AtNamespace Create(string? prefix, Func<ISourceElement, IUrl> createUrl)
		{
			var at = SymbolToken.Create(Symbol.At);
			var keyword = AtKeywordToken.Create(AtKeyword.Namespace, predecessor: at);

			IdentifierToken? prefixToken = null;
			IUrl path;

			if (prefix is not null)
			{
				prefixToken = IdentifierToken.Create(prefix, predecessor: keyword);
				path = createUrl(prefixToken);
			}
			else
			{
				path = createUrl(keyword);
			}

			var semiColon = SymbolToken.Create(Symbol.SemiColon, predecessor: path);

			return new AtNamespace(at, keyword, prefixToken, path, semiColon);
		}
	}
}
