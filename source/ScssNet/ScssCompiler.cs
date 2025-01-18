using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Lexing;
using ScssNet.Parsing;

namespace ScssNet
{
	public class ScssCompiler
	{
		public string CompileSource(string source)
		{
			var scssReader = new StringReader(source);
			var cssWriter = new StringWriter();
			Compile(scssReader, cssWriter);
			return cssWriter.ToString();
		}

		private void Compile(TextReader scssReader, TextWriter cssWriter)
		{
			var services = new ServiceCollection();
			services.AddSingleton(scssReader);
			services.AddReaders();
			services.AddTokenParsers();
			services.AddParsers();

			var provider = services.BuildServiceProvider();
			var tokenReader = provider.GetRequiredService<TokenReader>();
			var ruleSetParser = provider.GetRequiredService<RuleSetParser>();
			var ruleSetGenerator = provider.GetRequiredService<RuleSetGenerator>();

			var ruleSet = ruleSetParser.Parse(tokenReader);
			while(ruleSet != null)
			{
				ruleSetGenerator.Generate(ruleSet, cssWriter);
				ruleSet = ruleSetParser.Parse(tokenReader);
			}
		}
	}
}
