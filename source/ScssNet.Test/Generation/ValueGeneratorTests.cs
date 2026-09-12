using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Test.ElementCreation;
using ScssNet.Tokens;

namespace ScssNet.Test.Generation;

[TestClass]
public class ValueGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateIdentifierValue()
	{
		const string identifier = "bar";
		var valueToken = IdentifierToken.Create(identifier);

		TestGeneratedValue(valueToken, identifier);
	}

	[TestMethod]
	public void ShouldGenerateStringValue()
	{
		const string str = "\"foo bar\"";
		var valueToken = StringToken.Create(str);

		TestGeneratedValue(valueToken, str);
	}

	[TestMethod]
	public void ShouldGenerateHashValue()
	{
		const string hashValue = "#ff0000";
		var valueToken = HashValueToken.Create(hashValue);

		TestGeneratedValue(valueToken, hashValue);
	}

	[TestMethod]
	public void ShouldGenerateUnitValue()
	{
		var valueToken = UnitValueToken.Create(1.5m, "em");

		TestGeneratedValue(valueToken, "1.5em");
	}

	private static void TestGeneratedValue(IValue value, string expected)
	{
		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<ValueGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(value, cssWriter);

		provider.GetStringWriter().ShouldContain(expected);
	}
}
