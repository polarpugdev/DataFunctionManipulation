using Tokens;

public class Lexer
{
    private readonly string _input;
    private int _position;

    public Lexer(string input)
    {
        _input = input;
        _position = 0;
    }

    private char Current()
    {
        if (_position >= _input.Length)
            return '\0';

        return _input[_position];
    }

    private char Peek()
    {
        if (_position + 1 >= _input.Length)
            return '\0';

        return _input[_position + 1];
    }

    private char Next()
    {
        if (_position >= _input.Length)
            return '\0';

        return _input[_position++];
    }

    public List<Token> Lex()
    {
        var tokens = new List<Token>();

        while (true)
        {
            char c = Current();

            if (c == '\0')
            {
                tokens.Add(new Token(TokenType.EOF, ""));
                break;
            }

            if (char.IsWhiteSpace(c))
            {
                Next();
                continue;
            }
            else if (char.IsNumber(c))
            {
                string number = "";

                while (char.IsNumber(Current()))
                {
                    number += Current();
                    Next();
                }

                tokens.Add(new Token(TokenType.Number, number));
                continue;
            }
            else if (c == '"')
            {
                Next();
                string text = "";

                while (Current() != '"' && Current() != '\0')
                {
                    text += Current();
                    Next();
                }

                if (Current() == '"')
                    Next();

                tokens.Add(new Token(TokenType.Text, text));
                continue;
            }
            else if (char.IsLetter(c))
            {
                string keyword = "";

                while (char.IsLetterOrDigit(Current()) || Current() == '_')
                {
                    keyword += Current();
                    Next();
                }

                tokens.Add(new Token(TokenType.Keyword, keyword));
                continue;
            }
            else if (c == '(')
            {
                tokens.Add(new Token(TokenType.LeftParenthesis, "("));
                Next();
                continue;
            }
            else if (c == ')')
            {
                tokens.Add(new Token(TokenType.RightParenthesis, ")"));
                Next();
                continue;
            }
            else if (c == ':')
            {
                tokens.Add(new Token(TokenType.Colon, ":"));
                Next();
                continue;
            }
            else if (c == ';')
            {
                tokens.Add(new Token(TokenType.SemiColon, ";"));
                Next();
                continue;
            }
            else if (c == ',')
            {
                tokens.Add(new Token(TokenType.Comma, ","));
                Next();
                continue;
            }
            else if (c == '>')
            {
                if (Peek() == '=')
                {
                    tokens.Add(new Token(TokenType.GreaterThanOrEqual, ">="));
                    Next();
                    Next();
                }
                else
                {
                    tokens.Add(new Token(TokenType.GreaterThan, ">"));
                    Next();
                }
                continue;
            }

            else if (c == '<')
            {
                if (Peek() == '=')
                {
                    tokens.Add(new Token(TokenType.LessThanOrEqual, "<="));
                    Next();
                    Next();
                }
                else
                {
                    tokens.Add(new Token(TokenType.LessThan, "<"));
                    Next();
                }
                continue;
            }
            else if (c == '=')
            {
                tokens.Add(new Token(TokenType.Equal, "="));
                Next();
                continue;
            }
            else if (c == '|')
            {
                tokens.Add(new Token(TokenType.Or, "|"));
                Next();
                continue;
            }
            else if (c == '&')
            {
                tokens.Add(new Token(TokenType.And, "&"));
                Next();
                continue;
            }
            else if (c == '!')
            {
                if (Peek() == '=')
                {
                    tokens.Add(new Token(TokenType.NotEqual, "!="));
                    Next();
                    Next();
                }
                else
                {
                    tokens.Add(new Token(TokenType.Not, "!"));
                    Next();
                }
                continue;
            }
            else
            {
                Next();
            }
        }

        return tokens;
    }
}
