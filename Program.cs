using static System.Console;
using Tokens;
using Nodes;

public class Program
{
    public static void Main(string[] args)
    {

        WriteLine("Debug mode? (y/N)");
        bool DebugMode = ReadKey().Key == ConsoleKey.Y ? true : false;

        Clear();
        string DFM = File.ReadAllText("querys.dfm");
        Lexer lxr = new Lexer(DFM);
        List<Token> tokens = lxr.Lex();
        if (DebugMode)
        {
            foreach (var token in tokens)
                WriteLine(token.ToString());
            WriteLine("Enter To Continue");
            ReadLine();
            Clear();
        }


        Parser parser = new Parser(tokens);
        List<Node> nodes = parser.Parse();
        if (DebugMode)
        {
            foreach (Node node in nodes)
            {
                WriteLine(node.ToString());
            }
            WriteLine("Enter To Continue");
            ReadLine();
            Clear();
        }


        Compiler compiler = new Compiler(nodes);
        string SQL = compiler.GetCompiledDFM();

        WriteLine("\n\nFROM:");
        WriteLine(DFM);
        WriteLine("\nTO:");
        WriteLine(SQL);
    }
}