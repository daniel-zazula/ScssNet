using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class AttributteSelectorGenerator(TokenGenerator tokenGenerator)
{
	public void Generate(AttributteSelector classSelector, TextWriter writer)
	{
		tokenGenerator.Generate(classSelector.OpenBracket, writer);
		tokenGenerator.Generate(classSelector.Attribute, writer);

		if (classSelector.Operator is not null)
		{
			tokenGenerator.Generate(classSelector.Operator, writer);

			if (classSelector.Value is not null)
			{
				tokenGenerator.Generate(classSelector.Value, writer);

				if (classSelector.Modifier is not null)
				{
					tokenGenerator.Generate(classSelector.Modifier, writer);
				}
			}
		}

		tokenGenerator.Generate(classSelector.CloseBracket, writer);
	}
}
