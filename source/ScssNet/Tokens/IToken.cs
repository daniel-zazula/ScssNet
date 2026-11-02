namespace ScssNet.Tokens;

public interface IToken : ISourceElement { }

public interface IValueToken : IValue, IToken { }

public interface ISeparatorToken { }

public interface IMissingToken: IToken { }
