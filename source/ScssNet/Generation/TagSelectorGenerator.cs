﻿using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class TagSelectorGenerator(Lazy<CompoundSelectorGenerator> compoundSelectorGenerator)
{
	public void Generate(TagSelector tagSelector, TextWriter writer)
	{
		writer.Write(tagSelector.Identifier);
		if(tagSelector.Qualifier is not null)
			compoundSelectorGenerator.Value.Generate(tagSelector.Qualifier, writer);
	}
}
