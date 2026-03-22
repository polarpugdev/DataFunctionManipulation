namespace Tokens;

public enum TokenType
{
    Keyword,
    LeftParenthesis,
    RightParenthesis,
    Colon,
    SemiColon,
    Comma,
    Text,
    Number,
    GreaterThan,
    LessThan,
    GreaterThanOrEqual,
    LessThanOrEqual,
    Equal,
    And,
    Or,
    Not,
    NotEqual,
    EOF
}

public record Token(TokenType Type, string Value);