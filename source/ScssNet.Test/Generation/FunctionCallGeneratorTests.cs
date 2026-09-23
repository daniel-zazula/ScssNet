using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;
using ScssNet.Tokens;

namespace ScssNet.Test.Generation;

[TestClass]
public class FunctionCallGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateFromFunctionCallGenerator()
	{
		var functionCallValue = FunctionCall.Create("someFunc", CreateArguments);

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<FunctionCallGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(functionCallValue, cssWriter);

		AssertWrittenFunctionCall(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromValueGenerator()
	{
		var functionCallValue = FunctionCall.Create("someFunc", CreateArguments);

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<ValueGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(functionCallValue, cssWriter);

		AssertWrittenFunctionCall(provider);
	}

	private static IValue CreateArguments(ISourceElement predecessor)
	{
		var stringArgument = ValueList.CreateItem(StringToken.Create("\"foo bar\"", predecessor: predecessor), withComma: true);
		var hashValueArgument = ValueList.CreateItem(HashValueToken.Create("#ff0000", predecessor: stringArgument), withComma: true);
		var unitValueArgument = ValueList.CreateItem(UnitValueToken.Create(1.5m, "em", predecessor: hashValueArgument), withComma: false);
		return ValueList.Create([stringArgument, hashValueArgument, unitValueArgument]);
	}

	private static void AssertWrittenFunctionCall(ServiceProvider provider)
	{
		provider.GetStringWriter().ShouldContain("someFunc(\"foo bar\",#ff0000,1.5em)");
	}
}
