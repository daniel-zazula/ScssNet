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
	public void ShouldWriteFunctionCall()
	{
		var functionCallValue = CreateFunctionCall();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<FunctionCallGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(functionCallValue, cssWriter);

		AssertWrittenFunctionCall(provider);
	}

	[TestMethod]
	public void ShouldWriteFunctionCallValue()
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
		var openParenthesisToken = CreateSymbolToken(Symbol.OpenParenthesis, columnNumber: identifierToken.End.ColumnNumber + 1);
		var stringArgument = CreateListItemWithComma(CreateStringToken("\"foo bar\"", columnNumber: openParenthesisToken.End.ColumnNumber + 1));
		var hashValueArgument = CreateListItemWithComma(CreateHashValueToken("#ff0000", columnNumber: stringArgument.End.ColumnNumber + 1));
		var unitValueArgument = new ValueListItem(CreateUnitValueToken(1.5m, "em", columnNumber: hashValueArgument.End.ColumnNumber + 1));
		var closeParenthesisToken = CreateSymbolToken(Symbol.CloseParenthesis, columnNumber: unitValueArgument.End.ColumnNumber + 1);

		var valueList = new ValueList([stringArgument, hashValueArgument, unitValueArgument]);

		return new FunctionCall(identifierToken, openParenthesisToken, valueList, closeParenthesisToken);

		static ValueListItem CreateListItemWithComma(IValue value)
		{
			var commaToken = CreateSymbolToken(Symbol.Comma, columnNumber: value.End.ColumnNumber + 1);
			return new ValueListItem(value, commaToken);
		}
	}

	private static void AssertWrittenFunctionCall(ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe("someFunc(\"foo bar\",#ff0000,1.5em)");
	}
}
