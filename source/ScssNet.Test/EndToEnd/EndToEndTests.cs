using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Test.Parsing;
using Shouldly;

namespace ScssNet.Test.EndToEnd;

[TestClass]
public class EndToEndTests : ParserTestBase
{
	[TestMethod]
	[DataRow("css1.css")]
	[DataRow("css2.1.css")]
	[DataRow("css3AndLater.css")]
	public void ShouldParseCssFile(string fileName)
	{
		var path = Path.Combine(AppContext.BaseDirectory, "EndToEnd", fileName);
		var source = File.ReadAllText(path);

		var provider = BuildServiceProvider(source);

		var sourceReader = provider.GetRequiredService<ISourceReader>();
		var tokenReader = provider.GetRequiredService<TokenReader>();
		var stylesheetParser = provider.GetRequiredService<StatementParser>();

		var issues = new List<Issue>();

		try
		{
			var element = stylesheetParser.Parse(tokenReader);
			while(element != null)
			{
				issues.AddRange(element.Issues);
				element = stylesheetParser.Parse(tokenReader);
			}

			issues.ShouldBeEmpty();
			tokenReader.End.ShouldBeTrue();
		}
		catch(Exception ex)
		{
			var lines = GetNextLines(sourceReader, 5);
			var message = ex.Message + "\nNext lines: \n" + lines;
			throw new Exception(message, ex);
		}
	}

	private string GetNextLines(ISourceReader sourceReader, int lineCount)
	{
		var lines = new StringBuilder();
		while(lineCount > 0)
		{
			var read = sourceReader.Read();
			lines.Append(read);

			if(read == char.MaxValue)
			{
				break;
			}

			if (read == '\r')
			{
				if (sourceReader.Peek() == '\n')
				{
					lines.Append(sourceReader.Read());
				}
				lineCount--;
			}
			else if(read == '\n')
			{
				lineCount--;
			}
		}
		return lines.ToString();
	}
}
