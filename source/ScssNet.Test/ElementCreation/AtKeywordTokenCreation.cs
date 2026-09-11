using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class AtKeywordTokenCreation
{
	extension(AtKeywordToken)
	{
		internal static AtKeywordToken Create
		(
			AtKeyword atKeyword, string? value = null, ISourceElement? predecessor = null
		)
		{
			value = AtKeywordToken.CheckOrGetValue(atKeyword, value);
			var length = value.Length;
			var span = SourceSpan.Create(predecessor, length);

			return new AtKeywordToken(atKeyword, value, span, Separator.Empty, Separator.Empty);
		}
	}
}
