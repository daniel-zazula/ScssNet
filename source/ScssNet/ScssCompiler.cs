using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Lexing;
using ScssNet.Parsing;

namespace ScssNet;

public class ScssCompiler
{
	public string CompileSource(string source)
	{
		var scssReader = new StringReader(source);
		var cssWriter = new StringWriter();
		Compile(scssReader, cssWriter);
		return cssWriter.ToString();
	}

	private void Compile(TextReader textReader, TextWriter textWriter)
	{
		var services = new ServiceCollection();
		services.AddSingleton(textReader);
		services.AddReaders();
		services.AddTokenParsers();
		services.AddParsers();

		var provider = services.BuildServiceProvider();
		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var statementParser = provider.GetRequiredService<StatementParser>();
		var statementGenerator = provider.GetRequiredService<StatementGenerator>();
		var cssWriter = new CssWriter(textWriter);

		var element = statementParser.Parse(tokenReader);
		while(element != null)
		{
			statementGenerator.Generate(element, cssWriter);
			element = statementParser.Parse(tokenReader);
		}
	}
}
