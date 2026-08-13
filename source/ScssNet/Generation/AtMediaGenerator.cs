using ScssNet.Structures;

namespace ScssNet.Generation;

internal class AtMediaGenerator(Lazy<ValueGenerator> valueGenerator, Lazy<BlockGenerator> blockGenerator)
{
	public void Generate(AtMedia atMedia, CssWriter writer)
	{
		writer.Write(atMedia.AtSign);
		writer.Write(atMedia.Media);
		writer.Write(" ");
		valueGenerator.Value.Generate(atMedia.MediaQuery, writer);
		writer.Write(" ");
		blockGenerator.Value.Generate(atMedia.Block, writer);
	}
}
