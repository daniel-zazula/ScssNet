using System.IO;

namespace ScssNet
{
	public class ScssCompiler
	{
		public string CompileSource(string source)
		{
			var provider = DependencyRegistry.CreateServiceProvider(new StringReader(source));

			return "";
		}
	}
}
