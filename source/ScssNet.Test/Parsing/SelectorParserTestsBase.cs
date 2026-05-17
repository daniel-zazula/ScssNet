using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

public abstract class SelectorParserTestsBase : ParserTestBase
{
	protected static T ShouldParseSelector<T>(string source)
	{
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var selectorParser = provider.GetRequiredService<SelectorParser>();

		var selector = selectorParser.Parse(tokenReader).ShouldNotBeNull();

		selector.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();

		return selector.ShouldBeOfType<T>();
	}
}
