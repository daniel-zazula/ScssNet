namespace ScssNet;

internal static class StringExtensions
{
	internal static bool EqualsIgnoreCase(this string? str, string? other)
	{
		return string.Equals(str, other, StringComparison.InvariantCultureIgnoreCase);
	}
}
