using ScssNet.Structures;

namespace ScssNet.Tokens;

public interface IToken : ISourceElement { }

public interface IValueToken : IValue, IToken { }

public interface ISeparatorToken { }
