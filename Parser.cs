using Nodes;
using Tokens;

public class Parser
{
    List<Token> tokens;
    int position = 0;

    public Parser(List<Token> Tokens)
    {
        tokens = Tokens;
    }

    public List<Node> Parse()
    {
        var nodes = new List<Node>();

        while (Current().Type != TokenType.EOF)
            nodes.Add(ParseStatement());

        return nodes;
    }

    public Node ParseStatement()
    {
        if (Current().Type == TokenType.Keyword && Current().Value == "Select")
            return ParseSelect();

        throw new Exception("Unknown statement");
    }

    private Node ParseSelect()
    {
        Expect(TokenType.Keyword);
        Expect(TokenType.LeftParenthesis);

        string table = Expect(TokenType.Keyword).Value;

        Expect(TokenType.Colon);

        List<string> columns = ParseColumnList();

        Expect(TokenType.Colon);

        Node condition = ParseExpression();

        Expect(TokenType.RightParenthesis);
        Expect(TokenType.SemiColon);

        return new SelectNode(table, columns, condition);
    }

    private List<string> ParseColumnList()
    {
        var list = new List<string>();

        list.Add(Expect(TokenType.Keyword).Value);

        while (Match(TokenType.Comma))
            list.Add(Expect(TokenType.Keyword).Value);

        return list;
    }

    private Node ParseExpression()
    {
        Node left = ParsePrimary();

        while (IsOperator(Current().Type))
        {
            TokenType op = Next().Type;
            Node right = ParsePrimary();
            left = new BinaryExpression(left, op, right);
        }

        return left;
    }

    private Node ParsePrimary()
    {
        if (Match(TokenType.Not))
        {
            Node right = ParsePrimary();
            return new UnaryExpression(TokenType.Not, right);
        }

        if (Current().Type == TokenType.Keyword)
            return new Keyword(Next().Value);

        if (Current().Type == TokenType.Number)
            return new NumberLiteral(Next().Value);

        if (Current().Type == TokenType.Text)
            return new StringLiteral(Next().Value);

        if (Match(TokenType.LeftParenthesis))
        {
            Node expr = ParseExpression();
            Expect(TokenType.RightParenthesis);
            return expr;
        }

        throw new Exception("Unexpected token");
    }

    private bool IsOperator(TokenType type)
    {
        return type == TokenType.Equal
            || type == TokenType.GreaterThan
            || type == TokenType.GreaterThanOrEqual
            || type == TokenType.LessThan
            || type == TokenType.LessThanOrEqual
            || type == TokenType.NotEqual
            || type == TokenType.And
            || type == TokenType.Not
            || type == TokenType.Or;
    }

    private Token Current()
    {
        return tokens[position];
    }

    private Token Next()
    {
        return tokens[position++];
    }

    private bool Match(TokenType type)
    {
        if (Current().Type == type)
        {
            Next();
            return true;
        }

        return false;
    }

    private Token Expect(TokenType type)
    {
        if (Current().Type != type)
            throw new Exception("Unexpected token");

        return Next();
    }

}