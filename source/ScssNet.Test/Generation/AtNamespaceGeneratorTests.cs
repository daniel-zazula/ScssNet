using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class AtNamespaceGeneratorTests : GeneratorTestBase
{
	private const string XhtmlPath = "\"http://www.w3.org/1999/xhtml\"";
	private const string SvgPath = "\"http://www.w3.org/2000/svg\"";
	private const string Prefix = "svg";

	public enum UrlType { StringPath, FunctionPath };

	public static readonly IEnumerable<object[]> UrlCombinations =
	[
		[ "", UrlType.StringPath ],
		[ "", UrlType.FunctionPath ],
		[ Prefix, UrlType.StringPath ],
		[ Prefix, UrlType.FunctionPath ],
	];

	[TestMethod]
	[DynamicData(nameof(UrlCombinations))]
	public void ShouldGenerateFromAtRuleGenerator(string prefix, UrlType urlType)
	{
		var atNamespace = AtNamespace.Create(prefix, GetUrlCreator(prefix, urlType));

		var provider = BuildServiceProvider();
		var atRuleGenerator = provider.GetRequiredService<AtRuleGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atRuleGenerator.Generate(atNamespace, writer);

		AssertAtNamespace(provider, prefix, urlType);
	}

	[TestMethod]
	[DynamicData(nameof(UrlCombinations))]
	public void ShouldGenerateFromAtNamespaceGenerator(string prefix, UrlType urlType)
	{
		var atNamespace = AtNamespace.Create(prefix, GetUrlCreator(prefix, urlType));

		var provider = BuildServiceProvider();
		var atNamespaceGenerator = provider.GetRequiredService<AtNamespaceGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atNamespaceGenerator.Generate(atNamespace, writer);

		AssertAtNamespace(provider, prefix, urlType);
	}

	internal static void AssertAtNamespace(ServiceProvider provider, string prefix, UrlType urlType)
	{
		var path = GetPath(prefix);
		var url = urlType switch
		{
			UrlType.StringPath => path,
			UrlType.FunctionPath => $"url({path})",
			_ => throw new ArgumentOutOfRangeException(nameof(urlType), urlType, null)
		};

		provider.GetStringWriter().ShouldContain($"@namespace {prefix} {url};", StringCompareShould.IgnoreCase);
	}

	private static string GetPath(string prefix)
	{
		return prefix is not null ? SvgPath : XhtmlPath;
	}

	private static Func<ISourceElement, IValue> GetUrlCreator(string prefix, UrlType urlType)
	{
		var path = GetPath(prefix);

		return urlType switch
		{
			UrlType.StringPath => CreateStringPath,
			UrlType.FunctionPath => CreateFunctionCall,
			_ => throw new ArgumentOutOfRangeException(nameof(urlType), urlType, null)
		};

		StringToken CreateStringPath(ISourceElement predecessor) => StringToken.Create(path, predecessor);

		FunctionCall CreateFunctionCall(ISourceElement predecessor)
		{
			return FunctionCall.Create("url", CreateStringPath, predecessor: predecessor);
		}
	}
}
