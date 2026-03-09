namespace Lexer;

public class Lexer(string text)
{
  private static readonly Dictionary<string, TokenType> Keywords = new()
  {
    { "const", TokenType.Const },
    { "let", TokenType.Let },
    { "int", TokenType.Int },
    { "float", TokenType.Float },
    { "void", TokenType.Void },
    { "func", TokenType.Func },
    { "main", TokenType.Main },
  };

  private readonly TextScanner scanner = new TextScanner(text);

  public Token ParseToken()
  {
    SkipWhiteSpacesAndComments();

    if (scanner.IsEnd())
    {
      return new Token(TokenType.EndOfFile);
    }

    char ch = scanner.Peek();

    if (char.IsLetter(ch) || ch == '_')
    {
      return ParseIdentifierOrKeyword();
    }
    else if (char.IsAsciiDigit(ch))
    {
      return ParseNumberLiteral();
    }
    else
    {
      return ParseRemainTokens();
    }
  }

  private Token ParseNumberLiteral()
  {
    string value = "";

    while (char.IsAsciiDigit(scanner.Peek()))
    {
      value += scanner.Peek();
      scanner.Advance();
    }

    if (scanner.Peek() == '.')
    {
      value += scanner.Peek();
      scanner.Advance();

      while (char.IsAsciiDigit(scanner.Peek()))
      {
        value += scanner.Peek();
        scanner.Advance();
      }
    }

    decimal number;
    decimal.TryParse(value, out number);

    return new Token(TokenType.NumberLiteral, new TokenValue(number));
  }

  private Token ParseRemainTokens()
  {
    char ch = scanner.Peek();
    scanner.Advance();

    switch (ch)
    {
      case '=':
        return new Token(TokenType.Assignment);
      case '+':
        return new Token(TokenType.Plus);
      case '-':
        return new Token(TokenType.Minus);
      case '/':
        return new Token(TokenType.Division);
      case ':':
        return new Token(TokenType.ColonTypeIndication);
      case '(':
        return new Token(TokenType.OpenParenthesis);
      case ')':
        return new Token(TokenType.CloseParenthesis);
      case '{':
        return new Token(TokenType.OpenCurlyBrace);
      case '}':
        return new Token(TokenType.CloseCurlyBrace);
      case ';':
        return new Token(TokenType.Semicolon);
      case '*':
        return new Token(TokenType.Multiplication);
      case ',':
        return new Token(TokenType.Comma);
      default:
        return new Token(TokenType.Error, new TokenValue(ch.ToString()));
    }
  }

  private Token ParseIdentifierOrKeyword()
  {
    string identifier = "";
    for (char ch = scanner.Peek(); ch == '_' || char.IsLetter(ch) || char.IsAsciiDigit(ch); ch = scanner.Peek())
    {
      identifier += ch;
      scanner.Advance();
    }

    if (Keywords.TryGetValue(identifier, out TokenType keyword))
    {
      return new Token(keyword);
    }
    else
    {
      return new Token(TokenType.Identifier, new TokenValue(identifier));
    }
  }

  private void SkipWhiteSpacesAndComments()
  {
    do
    {
      SkipWhiteSpaces();
    }
    while (SkipComments());
  }

  private bool SkipComments()
  {
    if (scanner.Peek() == '/' && scanner.Peek(1) == '/' && scanner.Peek(2) == '/')
    {
      while (scanner.Peek() != '\n' && !scanner.IsEnd())
      {
        scanner.Advance();
      }

      return true;
    }

    if (scanner.Peek() == '/' && scanner.Peek(1) == '*')
    {
      scanner.Advance();
      scanner.Advance();

      while (!scanner.IsEnd())
      {
        if (scanner.Peek() == '*' && scanner.Peek(1) == '/')
        {
          break;
        }

        if (!SkipComments())
        {
          scanner.Advance();
        }
      }

      scanner.Advance();
      scanner.Advance();
      return true;
    }

    return false;
  }

  private void SkipWhiteSpaces()
  {
    while (char.IsWhiteSpace(scanner.Peek()))
    {
      scanner.Advance();
    }
  }
}