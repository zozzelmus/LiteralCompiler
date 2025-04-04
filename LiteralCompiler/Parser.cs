namespace LiteralCompiler;

class Parser
{
    private readonly Lexer _lexer;
    private Token _currentToken;

    public Parser(Lexer lexer)
    {
        _lexer = lexer;
        _currentToken = _lexer.GetNextToken();
    }

    private void Eat(TokenType type)
    {
        if (_currentToken.Type == type)
            _currentToken = _lexer.GetNextToken();
        else
            throw new Exception($"Unexpected token: {_currentToken.Type}, expected: {type}");
    }

    private Expression ParsePrimary()
    {
        if (_currentToken.Type == TokenType.Number)
        {
            var expr = new NumberExpression(int.Parse(_currentToken.Value));
            Eat(TokenType.Number);
            return expr;
        }
        if (_currentToken.Type == TokenType.OpenParen)
        {
            Eat(TokenType.OpenParen);
            var expr = ParseExpression();
            Eat(TokenType.CloseParen);
            return expr;
        }
        throw new Exception("unexpected token");
    }

    private Expression ParseTerm()
    {
        Expression left = ParsePrimary();

        while (_currentToken.Type == TokenType.Multiply || _currentToken.Type == TokenType.Divide)
        {
            var op = _currentToken.Type;
            Eat(op);
            Expression right = ParsePrimary();
            left = new BinaryExpression(left, op, right);

        }

        return left;
    }

    private Expression ParseExpression()
    {
        Expression left = ParseTerm();

        while (_currentToken.Type == TokenType.Plus || _currentToken.Type == TokenType.Minus)
        {
            var op = _currentToken.Type;
            Eat(op);
            Expression right = ParseTerm();
            left = new BinaryExpression(left, op, right);
        }

        return left;
    }

    public Expression Parse() => ParseExpression();
}
