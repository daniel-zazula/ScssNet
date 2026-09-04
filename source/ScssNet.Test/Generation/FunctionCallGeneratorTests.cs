using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class FunctionCallGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateFromFunctionCallGenerator()
	{
		var functionCallValue = CreateFunctionCall();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<FunctionCallGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(functionCallValue, cssWriter);

		AssertWrittenFunctionCall(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromValueGenerator()
	{
		var functionCallValue = CreateFunctionCall();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<ValueGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(functionCallValue, cssWriter);

		AssertWrittenFunctionCall(provider);
	}

	private static FunctionCall CreateFunctionCall()
	{
		var identifierToken = CreateIdentifierToken("someFunc");
		var openParenthesisToken = CreateSymbolToken(Symbol.OpenParenthesis, predecessor: identifierToken);
		var stringArgument = CreateListItemWithComma(CreateStringToken("\"foo bar\"", predecessor: openParenthesisToken));
		var hashValueArgument = CreateListItemWithComma(CreateHashValueToken("#ff0000", predecessor: stringArgument));
		var unitValueArgument = new ValueListItem(CreateUnitValueToken(1.5m, "em", predecessor: hashValueArgument));
		var closeParenthesisToken = CreateSymbolToken(Symbol.CloseParenthesis, predecessor: unitValueArgument);

		var valueList = new ValueList([stringArgument, hashValueArgument, unitValueArgument]);

		return new FunctionCall(identifierToken, openParenthesisToken, valueList, closeParenthesisToken);

		static ValueListItem CreateListItemWithComma(IValue value)
		{
			var commaToken = CreateSymbolToken(Symbol.Comma, predecessor: value);
			return new ValueListItem(value, commaToken);
		}
	}

	private static void AssertWrittenFunctionCall(ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe("someFunc(\"foo bar\",#ff0000,1.5em)");
	}
}
