using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class ValueGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateIdentifierValue()
	{
		const string identifier = "bar";
		var valueToken = CreateIdentifierToken(identifier);

		var writtenValue = GenerateValue(valueToken);

		writtenValue.ShouldBe(identifier);
	}

	[TestMethod]
	public void ShouldGenerateStringValue()
	{
		const string str = "\"foo bar\"";
		var valueToken = CreateStringToken(str);

		var writtenValue = GenerateValue(valueToken);

		writtenValue.ShouldBe(str);
	}

	[TestMethod]
	public void ShouldGenerateHashValue()
	{
		const string hashValue = "#ff0000";
		var valueToken = CreateHashValueToken(hashValue);

		var writtenValue = GenerateValue(valueToken);

		writtenValue.ShouldBe(hashValue);
	}

	[TestMethod]
	public void ShouldGenerateUnitValue()
	{
		var valueToken = CreateUnitValueToken(1.5m, "em");

		var writtenValue = GenerateValue(valueToken);

		writtenValue.ShouldBe("1.5em");
	}

	private static string GenerateValue(IValue value)
	{
		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<ValueGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(value, cssWriter);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		return stringWriter.ToString();
	}
}
