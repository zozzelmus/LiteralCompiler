using LiteralCompiler;

while (true)
{
    Console.WriteLine("Enter Math Equation");
    string input = Console.ReadLine() ?? throw new Exception("Value not passed");
    int result = Interpreter.Evaluate(input);
    Console.WriteLine($"Result: {result}");
}



//Console.WriteLine(3 + 3);