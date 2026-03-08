namespace Lexer.UnitTests;

public class LexerTest
{
  [Theory]
  [MemberData(nameof(GetTokenizeData))]
  public void LexerTestTheory(string text, List<Token> expected)
  {
    List<Token> actual = Tokenize(text);
    Assert.Equal(expected, actual);
  }

  public static TheoryData<string, List<Token>> GetTokenizeData()
  {
    return new TheoryData<string, List<Token>>
    {
      {
        "let age: int;", [
          new Token(TokenType.Let),
          new Token(TokenType.Identifier, new TokenValue("age")),
          new Token(TokenType.ColonTypeIndication),
          new Token(TokenType.Int),
          new Token(TokenType.Semicolon)
        ]
      },
      {
        "const age: int = 10;", [
          new Token(TokenType.Const),
          new Token(TokenType.Identifier, new TokenValue("age")),
          new Token(TokenType.ColonTypeIndication),
          new Token(TokenType.Int),
          new Token(TokenType.Assignment),
          new Token(TokenType.NumberLiteral, new TokenValue("10")),
          new Token(TokenType.Semicolon),
        ]
      },
      {
        "input();", [
          new Token(TokenType.Input),
          new Token(TokenType.OpenParenthesis),
          new Token(TokenType.CloseParenthesis),
          new Token(TokenType.Semicolon)
        ]
      },
      {
        "print(10);", [
          new Token(TokenType.Print),
          new Token(TokenType.OpenParenthesis),
          new Token(TokenType.NumberLiteral, new TokenValue("10")),
          new Token(TokenType.CloseParenthesis),
          new Token(TokenType.Semicolon)
        ]
      },
      {
        "func main:void() {}", [
          new Token(TokenType.Func),
          new Token(TokenType.Main),
          new Token(TokenType.ColonTypeIndication),
          new Token(TokenType.Void),
          new Token(TokenType.OpenParenthesis),
          new Token(TokenType.CloseParenthesis),
          new Token(TokenType.OpenCurlyBrace),
          new Token(TokenType.CloseCurlyBrace),
        ]
      },
      {
        "const number:int = 11;", [
          new Token(TokenType.Const),
          new Token(TokenType.Identifier, new TokenValue("number")),
          new Token(TokenType.ColonTypeIndication),
          new Token(TokenType.Int),
          new Token(TokenType.Assignment),
          new Token(TokenType.NumberLiteral, new TokenValue("11")),
          new Token(TokenType.Semicolon),
        ]
      },
      {
        "const number:float = 1.1;", [
          new Token(TokenType.Const),
          new Token(TokenType.Identifier, new TokenValue("number")),
          new Token(TokenType.ColonTypeIndication),
          new Token(TokenType.Float),
          new Token(TokenType.Assignment),
          new Token(TokenType.NumberLiteral, new TokenValue("1.1")),
          new Token(TokenType.Semicolon),
        ]
      },
      {
        "const value:int = x + y - z;", [
          new Token(TokenType.Const),
          new Token(TokenType.Identifier, new TokenValue("value")),
          new Token(TokenType.ColonTypeIndication),
          new Token(TokenType.Int),
          new Token(TokenType.Assignment),
          new Token(TokenType.Identifier, new TokenValue("x")),
          new Token(TokenType.Plus),
          new Token(TokenType.Identifier, new TokenValue("y")),
          new Token(TokenType.Minus),
          new Token(TokenType.Identifier, new TokenValue("z")),
          new Token(TokenType.Semicolon),
        ]
      },
      {
        "const area:float = 10 * 5 / 20;", [
          new Token(TokenType.Const),
          new Token(TokenType.Identifier, new TokenValue("area")),
          new Token(TokenType.ColonTypeIndication),
          new Token(TokenType.Float),
          new Token(TokenType.Assignment),
          new Token(TokenType.NumberLiteral, new TokenValue("10")),
          new Token(TokenType.Multiplication),
          new Token(TokenType.NumberLiteral, new TokenValue("5")),
          new Token(TokenType.Division),
          new Token(TokenType.NumberLiteral, new TokenValue("20")),
          new Token(TokenType.Semicolon),
        ]
      },
      {
        "/// Это комментарий",
        [
        ]
      },
      {
        "let x:int = 5; /// Комментарий после кода",
        [
          new Token(TokenType.Let),
          new Token(TokenType.Identifier, new TokenValue("x")),
          new Token(TokenType.ColonTypeIndication),
          new Token(TokenType.Int),
          new Token(TokenType.Assignment),
          new Token(TokenType.NumberLiteral, new TokenValue("5")),
          new Token(TokenType.Semicolon),
        ]
      },
      {
        "/* Это многострочный комментарий */",
        [
        ]
      },
    };
  }

  private static List<Token> Tokenize(string text)
  {
    List<Token> results = [];
    Lexer lexer = new(text);

    for (Token t = lexer.ParseToken(); t.Type != TokenType.EndOfFile; t = lexer.ParseToken())
    {
      results.Add(t);
    }

    return results;
  }
}