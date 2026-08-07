using System.Diagnostics.Eventing.Reader;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class StatementGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateRuleSetStatement()
	{
		var ruleSet = RuleSetGeneratorTests.CreateRuleSet();

		var provider = GenerateStatement(ruleSet);

		RuleSetGeneratorTests.AssertRuleSet(provider);
	}

	[TestMethod]
	public void ShouldGenerateAtCharsetStatement()
	{
		var atCharset = AtCharsetGeneratorTests.CreateAtCharset();

		var provider = GenerateStatement(atCharset);

		AtCharsetGeneratorTests.AssertAtCharset(provider);
	}

	[TestMethod]
	public void ShouldGenerateAtImportStatement()
	{
		var atImport = AtImportGeneratorTests.CreateAtImport();

		var provider = GenerateStatement(atImport);

		AtImportGeneratorTests.AssertAtImport(provider);
	}

	private static ServiceProvider GenerateStatement(IStatement statement)
	{
		var provider = BuildServiceProvider();
		var ruleSetGenerator = provider.GetRequiredService<StatementGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		ruleSetGenerator.Generate(statement, writer);

		return provider;
	}
}
