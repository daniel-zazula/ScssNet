using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.ElementCreation;

internal static class StringTokenCreation
{
	extension(StringToken)
	{
		internal static StringToken Create(string value, ISourceElement? predecessor = null)
		{
			var startChar = value[0];
			var endChar = value[^1];

			startChar.ShouldBeOneOf('"', '\'');
			endChar.ShouldBe(startChar);

			var length = value.Length;
			var span = SourceSpan.Create(predecessor, length);

			return new StringToken(value, span, Separator.Empty, Separator.Empty);
		}
	}
}
