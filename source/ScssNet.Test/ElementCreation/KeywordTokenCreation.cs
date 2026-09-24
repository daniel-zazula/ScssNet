global using AtKeywordToken = ScssNet.Tokens.KeywordToken<ScssNet.Tokens.AtKeyword>;
global using ValueKeywordToken = ScssNet.Tokens.KeywordToken<ScssNet.Tokens.ValueKeyword>;
global using MediaQueryOperatorKeywordToken = ScssNet.Tokens.KeywordToken<ScssNet.Tokens.MediaQueryOperatorKeyword>;

using System;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.ElementCreation;

internal static class KeywordTokenCreation
{
	extension<T>(KeywordToken<T>) where T : struct, Enum
	{
		internal static KeywordToken<T> Create
		(
			T keyword, string? value = null, ISourceElement? predecessor = null
		)
		{
			value = KeywordToken<T>.CheckOrGetValue(keyword, value);
			var length = value.Length;
			var span = SourceSpan.Create(predecessor, length);

			return new KeywordToken<T>(keyword, value, span, Separator.Empty, Separator.Empty);
		}

		internal static string CheckOrGetValue(T keyword, string? value = null)
		{
			if(value is null)
				return keyword.ToString();

			value.ShouldBe(keyword.ToString(), StringCompareShould.IgnoreCase);
			return value;
		}
	}
}
