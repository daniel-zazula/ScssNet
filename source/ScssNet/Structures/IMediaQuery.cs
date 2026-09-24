namespace ScssNet.Structures;

public interface IMediaQuery : ISyntaxStructure
{
}

public interface IMediaQueryExpression : ISyntaxStructure, IMediaQuery
{
}

public interface IMediaQueryValue : ISyntaxStructure, IMediaQuery, IMediaQueryExpression
{
}
