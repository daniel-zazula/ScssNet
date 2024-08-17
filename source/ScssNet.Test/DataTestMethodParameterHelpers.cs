namespace ScssNet.Test
{
	internal static class DataTestMethodParameterHelpers
	{
		internal static IEnumerable<object[]> ToParams(this IEnumerable<string> sources) => sources.Select(vi => new object[] { vi });
	}
}
