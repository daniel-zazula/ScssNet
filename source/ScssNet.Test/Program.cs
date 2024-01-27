namespace ScssNet.Test
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var test = new ParsingTest("D:\\Projetos\\Usherpa\\LOWeb\\UsherpaLO.Web.UI\\Content\\site.scss");
			var line = test.DescribleNextToken();
			while (line != null)
			{
				Console.WriteLine(line);
				line = test.DescribleNextToken();
			}
			Console.WriteLine("End");
		}
	}
}
