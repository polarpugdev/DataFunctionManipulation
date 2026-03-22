namespace Nodes;

using Tokens;

public abstract record Node { }
public record SelectNode(string Table, List<string> Columns, Node Condition) : Node { }
public record BinaryExpression(Node left, TokenType Operator, Node right) : Node { }
public record UnaryExpression(TokenType Operator, Node Right) : Node;
public record Keyword(string Name) : Node { }
public record NumberLiteral(string Value) : Node { }
public record StringLiteral(string Value) : Node { }