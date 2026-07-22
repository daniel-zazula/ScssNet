using ScssNet.Structures;

namespace ScssNet.Generation;

internal class FunctionCallGenerator(Lazy<ValueGenerator> valueGenerator)
{
	public void Generate(FunctionCall functionCall, CssWriter writer)
	{
		writer.Write(functionCall.Name);
		writer.Write(functionCall.OpenParenthesis);

		if (functionCall.Arguments != null)
			valueGenerator.Value.Generate(functionCall.Arguments, writer);

		writer.Write(functionCall.CloseParenthesis);
	}
}
