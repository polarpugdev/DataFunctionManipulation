using Nodes;
using Tokens;

public class Compiler
{
    List<Node> nodes = new List<Node>();

    public Compiler(List<Node> Nodes)
    {
        nodes = Nodes;
    }

    public string GetCompiledDFM()
    {
        string C_DFM = "";
        foreach (Node node in nodes)
        {
            C_DFM += Compile(node) + "\n";
        }

        return C_DFM;
    }

    private string Compile(Node node)
    {
        return node switch
        {
            SelectNode s => CompileSelect(s),
            BinaryExpression b => CompileBinary(b),
            UnaryExpression u => CompileUnary(u),
            Keyword k => k.Name,
            NumberLiteral n => n.Value,
            StringLiteral t => $"\"{t.Value}\"",
            _ => throw new Exception($"Node type ({node.GetType()}) not defined in switch case")
        };
    }

    private string CompileSelect(SelectNode s)
    {
        string cols = string.Join(", ", s.Columns);
        string cond = Compile(s.Condition);

        return $"SELECT {cols} FROM {s.Table} WHERE {cond};";
    }

    private string CompileBinary(BinaryExpression b)
    {
        string left = Compile(b.left);
        string right = Compile(b.right);
        string op = OperatorToSql(b.Operator);

        return $"({left} {op} {right})";
    }

    private string CompileUnary(UnaryExpression u)
    {
        string right = Compile(u.Right);
        return $"NOT {right}";
    }

    private string OperatorToSql(TokenType op)
    {
        return op switch
        {
            TokenType.Equal => "=",
            TokenType.GreaterThan => ">",
            TokenType.GreaterThanOrEqual => ">=",
            TokenType.LessThan => "<",
            TokenType.LessThanOrEqual => "<=",
            TokenType.NotEqual => "!=",
            TokenType.And => "AND",
            TokenType.Or => "OR",
            _ => throw new Exception($"Unknown operator: {op}")
        };
    }
}
