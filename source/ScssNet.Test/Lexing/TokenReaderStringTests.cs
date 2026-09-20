using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class TokenReaderStringTests : TokenReaderTestBase
{
	public static IEnumerable<object[]> StringParams => AddSpacing(TokensTestData.Strings);

	[TestMethod]
	[DynamicData(nameof(StringParams))]
	public void ShouldMatchString(string source, string expected)
	{
		var stringToken = TestTokenMatch<StringToken>(source);
		stringToken.Text.ShouldBe(expected);
		stringToken.Issues.ShouldBeEmpty();
	}

	[TestMethod]
	[DynamicData(nameof(StringParams))]
	public void ShouldRequireStringWithoutIssues(string source, string expected)
	{
		var tokenReader = SetupTokenReader(source);

		var stringToken = tokenReader.RequireString();

		stringToken.Text.ShouldBe(expected);
		stringToken.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonStringParams => TokensTestData.OneOfEach
		.Except(TokensTestData.Strings).ToParams();

	[TestMethod]
	[DynamicData(nameof(NonStringParams))]
	public void ShouldRequireNonStringWithIssue(string source)
	{
		var tokenReader = SetupTokenReader(source);

		var stringToken = tokenReader.RequireString();
		stringToken.Text.ShouldBe("");
		AssertExpectedTokenIssue(stringToken);

		tokenReader.End.ShouldBeFalse();
	}
}
