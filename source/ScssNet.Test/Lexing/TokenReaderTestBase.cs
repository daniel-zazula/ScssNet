using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

public abstract class TokenReaderTestBase
{
	protected static readonly string[] templates = ["{0}", " {0}", "{0} ", " {0} "];

	protected static IEnumerable<object[]> AddSpacing(IEnumerable<string> sources)
	{
		while(sources.Count() < templates.Length)
		{
			sources = sources.Concat(sources);
		}

		for(int i = 0; i < templates.Length; i++)
		{
			var source = sources.Skip(i).First();
			yield return new object[] { string.Format(templates[i], source), source };
		}
	}

	internal static TokenReader SetupTokenReader(string source)
	{
		var reader = new StringReader(source);

		var services = new ServiceCollection();
		services.AddSingleton<TextReader>(reader);
		services.AddReaders();
		services.AddTokenParsers();

		var provider = services.BuildServiceProvider();
		return provider.GetRequiredService<TokenReader>();
	}

	protected static void AssertSeparators(string source, Separator leadingSeparator, Separator trailingSeparator)
	{
		if(source.StartsWith(' '))
		{
			AssertSingleSpaceSeparator(leadingSeparator);
		}

		if(source.EndsWith(' '))
		{
			AssertSingleSpaceSeparator(trailingSeparator);
		}
	}

	private static void AssertSingleSpaceSeparator(Separator separator)
	{
		separator.ShouldNotBeNull();
		var token = separator!.Tokens.ShouldHaveSingleItem();
		var whiteSpaceToken = token.ShouldBeOfType<WhiteSpaceToken>();
		whiteSpaceToken.Text.ShouldBe(" ");
	}

	protected static void AssertExpectedTokenIssue(IToken token)
	{
		var issue = token.Issues.ShouldHaveSingleItem();
		issue.Type.ShouldBe(IssueType.Error);
		issue.Message.ShouldContain("Expected");
	}
}
