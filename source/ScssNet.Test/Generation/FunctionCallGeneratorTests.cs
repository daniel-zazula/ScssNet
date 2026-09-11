using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class FunctionCallGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateFromFunctionCallGenerator()
	{
		var functionCallValue = FunctionCall.Create(CreateArguments);

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<FunctionCallGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(functionCallValue, cssWriter);

		AssertWrittenFunctionCall(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromValueGenerator()
	{
		var functionCallValue = FunctionCall.Create(CreateArguments);

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<ValueGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(functionCallValue, cssWriter);

		AssertWrittenFunctionCall(provider);
	}

	private static ICollection<ValueListItem> CreateArguments(ISourceElement predecessor)
	{
		var stringArgument = ValueList.CreateItem(StringToken.Create("\"foo bar\"", predecessor: predecessor), withComma: true);
		var hashValueArgument = ValueList.CreateItem(HashValueToken.Create("#ff0000", predecessor: stringArgument), withComma: true);
		var unitValueArgument = ValueList.CreateItem(UnitValueToken.Create(1.5m, "em", predecessor: hashValueArgument), withComma: false);
		return [stringArgument, hashValueArgument, unitValueArgument];
	}

	private static void AssertWrittenFunctionCall(ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe("someFunc(\"foo bar\",#ff0000,1.5em)");
	}
}
