using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace ScssNet.Test.Generation;

internal static class AssertionHelpers
{
	internal static StringWriter GetStringWriter(this ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		return stringWriter;
	}

	internal static StringWriter ShouldContain(this StringWriter stringWriter, string expected)
	{
		stringWriter.ToString().ShouldBe(expected);
		return stringWriter;
	}

	internal static StringWriter ShouldContain
	(
		this StringWriter stringWriter, string expected, StringCompareShould compareOptions
	)
	{
		stringWriter.ToString().ShouldBe(expected, compareOptions);
		return stringWriter;
	}
}
