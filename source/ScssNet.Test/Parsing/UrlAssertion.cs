using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

internal static class UrlAssertion
{
	private const string FunctionName = "url";

	internal static void AssertUrl(this IValue url, string urlSource)
	{
		if (urlSource.StartsWith(FunctionName))
			url.AssertFunctionUrl(urlSource);
		else
			url.AssertStringUrl(urlSource);
	}

	private static void AssertFunctionUrl(this IValue value, string urlSource)
	{
		var functionPathString = urlSource[4..^1]; //removes the "url(" and ")"

		var urlFunction = value.ShouldBeOfType<FunctionCall>();
		urlFunction.Name.Text.ShouldBe(FunctionName);

		var functionValue = urlFunction.Arguments.ShouldNotBeNull();
		functionValue.AssertStringUrl(functionPathString);
	}

	private static void AssertStringUrl(this IValue value, string path)
	{
		var token = value.ShouldBeOfType<StringToken>();
		token.Text.ShouldBe(path);
	}
}
