using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.ElementCreation;

internal static class HashValueTokenCreation
{
	extension(HashValueToken)
	{
		internal static HashValueToken Create(string value, ISourceElement? predecessor = null)
		{
			value.ShouldStartWith("#");

			var length = value.Length;
			var span = SourceSpan.Create(predecessor, length);

			return new HashValueToken(value, span, Separator.Empty, Separator.Empty);
		}
	}
}
