using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class UnitValueTokenCreation
{
	extension(UnitValueToken)
	{
		internal static UnitValueToken Create
		(
			decimal amount, string unit, ISourceElement? predecessor = null
		)
		{
			var valueString = $"{amount}{unit}";
			var length = valueString.Length;
			var span = SourceSpan.Create(predecessor, length);

			return new UnitValueToken(amount, unit, span, Separator.Empty, Separator.Empty);
		}
	}
}
