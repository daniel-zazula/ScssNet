using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class AtCharsetGeneratorTests : GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateFromAtRuleGenerator()
	{
		var atCharset = AtCharset.Create();

		var provider = BuildServiceProvider();
		var atRuleGenerator = provider.GetRequiredService<AtRuleGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atRuleGenerator.Generate(atCharset, writer);

		AssertAtCharset(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromAtCharsetGenerator()
	{
		var atCharset = AtCharset.Create();

		var provider = BuildServiceProvider();
		var atCharsetGenerator = provider.GetRequiredService<AtCharsetGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atCharsetGenerator.Generate(atCharset, writer);

		AssertAtCharset(provider);
	}

	internal static void AssertAtCharset(ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe("@charset \"utf-8\";", StringCompareShould.IgnoreCase);
	}
}
