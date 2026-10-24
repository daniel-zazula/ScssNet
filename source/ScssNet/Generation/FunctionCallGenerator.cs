using ScssNet.Structures;

namespace ScssNet.Generation;

internal class FunctionCallGenerator(GeneratorContext generatorContext): IGenerator<FunctionCall>
{
	private IGenerator<IValue> ValueGenerator => generatorContext.GetGenerator<IValue>();

	public void Generate(FunctionCall functionCall, CssWriter writer)
	{
		writer.Write(functionCall.Name);
		writer.Write(functionCall.OpenParenthesis);

		if (functionCall.Arguments != null)
			ValueGenerator.Generate(functionCall.Arguments, writer);

		writer.Write(functionCall.CloseParenthesis);
	}
}
