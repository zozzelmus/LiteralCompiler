namespace LiteralCompiler;

public class Interpreter
{
    public static int Evaluate(string input)
    {
        var lexer = new Lexer(input);
        var parser = new Parser(lexer);
        Expression ast = parser.Parse();
        return ast.Evaluate();
    }
}