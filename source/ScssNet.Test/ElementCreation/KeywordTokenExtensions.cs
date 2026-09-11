using System;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.ElementCreation;

internal static class KeywordTokenExtensions
{
	extension<T>(KeywordToken<T>) where T : Enum
	{
		internal static string CheckOrGetValue(T keyword, string? value = null)
		{
			if(value is null)
				return keyword.ToString();

			value.ShouldBe(keyword.ToString(), StringCompareShould.IgnoreCase);
			return value;
		}
	}
}
