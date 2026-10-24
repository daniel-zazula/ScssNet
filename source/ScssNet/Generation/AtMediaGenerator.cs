using ScssNet.Structures;

namespace ScssNet.Generation;

internal class AtMediaGenerator(GeneratorContext generatorContext): IGenerator<AtMedia>
{
	private IGenerator<IMediaQuery> MediaQueryGenerator => generatorContext.GetGenerator<IMediaQuery>();
	private IGenerator<Block> BlockGenerator => generatorContext.GetGenerator<Block>();

	public void Generate(AtMedia atMedia, CssWriter writer)
	{
		writer.Write(atMedia.AtSign);
		writer.Write(atMedia.Media);
		writer.Write(" ");
		MediaQueryGenerator.Generate(atMedia.MediaQuery, writer);
		writer.Write(" ");
		BlockGenerator.Generate(atMedia.Block, writer);
	}
}
