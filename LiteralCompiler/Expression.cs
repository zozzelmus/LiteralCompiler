namespace LiteralCompiler;

public abstract class Expression
{
    public abstract int Evaluate();
}

public class NumberExpression : Expression
{
    public int Value { get; }
    public NumberExpression(int value) => Value = value;
    public override int Evaluate() => Value;
}

public class BinaryExpression : Expression
{
    public Expression Left { get; }
    public TokenType Operator { get; }
    public Expression Right { get; }
    public BinaryExpression(Expression left, TokenType op, Expression right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
    public override int Evaluate() => Operator switch
    {
        TokenType.Plus => Left.Evaluate() + Right.Evaluate(),
        TokenType.Minus => Left.Evaluate() - Right.Evaluate(),
        TokenType.Multiply => Left.Evaluate() * Right.Evaluate(),
        TokenType.Divide => Left.Evaluate() / Right.Evaluate(),
        _ => throw new Exception("Unknown Operator")
    };
}