using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class AttributteSelectorGenerator
{
	public void Generate(AttributteSelector classSelector, CssWriter writer)
	{
		writer.Write(classSelector.OpenBracket);
		writer.Write(classSelector.Attribute);

		if (classSelector.Operator is not null)
		{
			writer.Write(classSelector.Operator);

			if (classSelector.Value is not null)
			{
				writer.Write(classSelector.Value);

				if (classSelector.Modifier is not null)
				{
					writer.Write(classSelector.Modifier);
				}
			}
		}

		writer.Write(classSelector.CloseBracket);
	}
}
