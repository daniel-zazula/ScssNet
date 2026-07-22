using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class FunctionCallParserTests : ParserTestBase
{
	[TestMethod]
	public void ShouldParseFunctionCallWithoutArguments()
	{
		var provider = BuildServiceProvider("func()");

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var valueParser = provider.GetRequiredService<ValueParser>();

		var value = valueParser.Parse(tokenReader);

		value.ShouldNotBeNull();
		var functionCall = value.ShouldBeOfType<FunctionCall>();

		functionCall.Arguments.ShouldBeNull();
		functionCall.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}

	[TestMethod]
	public void ShouldParseFunctionCallWithArguments()
	{
		var provider = BuildServiceProvider("func(1cm, \"hello\")");

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var valueParser = provider.GetRequiredService<ValueParser>();

		var value = valueParser.Parse(tokenReader);

		value.ShouldNotBeNull();
		var functionCall = value.ShouldBeOfType<FunctionCall>();

		var arguments = functionCall.Arguments;
		arguments.ShouldNotBeNull();
		arguments!.Items.Count.ShouldBe(2);

		functionCall.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
