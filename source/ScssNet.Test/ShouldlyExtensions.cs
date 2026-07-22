using Shouldly;

namespace ScssNet.Test;

internal static class ShouldlyExtensions
{
	public static void ShouldHaveCount<T>(this ICollection<T> collection, int count)
	{
		collection.Count.ShouldBe(count);
	}

	public static void ShouldHaveCount<T>(this IReadOnlyCollection<T> collection, int count)
	{
		collection.Count.ShouldBe(count);
	}
}
