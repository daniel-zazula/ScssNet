using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

internal static class UrlAssertion
{
	private const string FunctionName = "url";

	internal static void AssertUrl(this IUrl url, string urlSource)
	{
		if (urlSource.StartsWith(FunctionName))
			url.AssertFunctionUrl(urlSource);
		else
			url.AssertStringUrl(urlSource);
	}

	private static void AssertFunctionUrl(this IUrl url, string urlSource)
	{
		var functionPathString = urlSource[4..^1]; //removes the "url(" and ")"

		var urlFunction = url.ShouldBeOfType<FunctionUrl>();
		var function = urlFunction.Value.ShouldBeOfType<FunctionCall>();
		function.Name.Text.ShouldBe(FunctionName);

		var urlValue = function.Arguments.ShouldNotBeNull();
		var token = urlValue.ShouldBeOfType<StringToken>();
		token.Text.ShouldBe(functionPathString);
	}

	private static void AssertStringUrl(this IUrl value, string path)
	{
		var stringUrl = value.ShouldBeOfType<StringUrl>();
		var token = stringUrl.Value.ShouldBeOfType<StringToken>();
		token.Text.ShouldBe(path);
	}
}
