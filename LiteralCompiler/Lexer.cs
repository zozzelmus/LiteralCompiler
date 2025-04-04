namespace LiteralCompiler;

public enum TokenType
{
    Number,
    Plus,
    Minus,
    Multiply,
    Divide,
    OpenParen,
    CloseParen,
    EndOfInput
}

public class Token
{
    public TokenType Type { get; }
    public string Value { get; }
    public Token(TokenType type, string value = "")
    {
        Type = type;
        Value = value;
    }
    public override string ToString()
    {
        return $"Token({Type}, {Value})";
    }
}

public class Lexer
{
    private readonly string _input;
    private int _position;

    public Lexer(string input)
    {
        _input = input;
    }

    public Token GetNextToken()
    {
        while (_position < _input.Length)
        {
            char current = _input[_position];

            if (char.IsWhiteSpace(current))
            {
                _position++;
                continue;
            }

            if (char.IsDigit(current))
                return ReadNumber();

            _position++;
            return current switch
            {
                '+' => new Token(TokenType.Plus),
                '-' => new Token(TokenType.Minus),
                '*' => new Token(TokenType.Multiply),
                '/' => new Token(TokenType.Divide),
                '(' => new Token(TokenType.OpenParen),
                ')' => new Token(TokenType.CloseParen),
                _ => throw new Exception($"Unexpected character {current}")
            };
        }
        return new Token(TokenType.EndOfInput);
    }

    private Token ReadNumber()
    {
        int start = _position;
        while (_position < _input.Length && char.IsDigit(_input[_position]))
            _position++;

        return new Token(TokenType.Number, _input[start.._position]);
    }
}