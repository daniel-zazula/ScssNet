using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class ValueGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldWriteIdentifierValue()
	{
		const string identifier = "bar";
		var valueToken = CreateIdentifierToken(identifier);

		var writtenValue = WriteValue(valueToken);

		writtenValue.ShouldBe(identifier);
	}

	[TestMethod]
	public void ShouldWriteStringValue()
	{
		const string str = "\"foo bar\"";
		var valueToken = CreateStringToken(str);

		var writtenValue = WriteValue(valueToken);

		writtenValue.ShouldBe(str);
	}

	[TestMethod]
	public void ShouldWriteHashValue()
	{
		const string hashValue = "#ff0000";
		var valueToken = CreateHashValueToken(hashValue);

		var writtenValue = WriteValue(valueToken);

		writtenValue.ShouldBe(hashValue);
	}

	[TestMethod]
	public void ShouldWriteUnitValue()
	{
		var valueToken = CreateUnitValueToken(1.5m, "em");

		var writtenValue = WriteValue(valueToken);

		writtenValue.ShouldBe("1.5em");
	}

	[TestMethod]
	public void ShouldWriteFunctionCallValue()
	{
		const string identifier = "someFunc";
		const string str = "\"foo bar\"";
		const string hashValue = "#ff0000";

		var identifierToken = CreateIdentifierToken(identifier);
		var openParenthesisToken = CreateSymbolToken(Symbol.OpenParenthesis, columnNumber: identifierToken.End.ColumnNumber + 1);
		var stringArgument = CreateListItemWithComma(CreateStringToken(str, columnNumber: openParenthesisToken.End.ColumnNumber + 1));
		var hashValueArgument = CreateListItemWithComma(CreateHashValueToken(hashValue, columnNumber: stringArgument.End.ColumnNumber + 1));
		var unitValueArgument = new ValueListItem(CreateUnitValueToken(1.5m, "em", columnNumber: hashValueArgument.End.ColumnNumber + 1));
		var closeParenthesisToken = CreateSymbolToken(Symbol.CloseParenthesis, columnNumber: unitValueArgument.End.ColumnNumber + 1);

		var valueList = new ValueList([stringArgument, hashValueArgument, unitValueArgument]);

		var functionCall = new FunctionCall(identifierToken, openParenthesisToken, valueList, closeParenthesisToken);

		var writtenValue = WriteValue(functionCall);

		writtenValue.ShouldBe("someFunc(\"foo bar\",#ff0000,1.5em)");

		static ValueListItem CreateListItemWithComma(IValue value)
		{
			var commaToken = CreateSymbolToken(Symbol.Comma, columnNumber: value.End.ColumnNumber + 1);
			return new ValueListItem(value, commaToken);
		}
	}

	private static string WriteValue(IValue value)
	{
		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<ValueGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(value, cssWriter);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		return stringWriter.ToString();
	}
}
