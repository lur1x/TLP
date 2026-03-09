using System;
using System.Globalization;

using Ast;
using Ast.Declarations;
using Ast.Expressions;

using Lexer;

using Runtime;

using ValueType = Runtime.ValueType;

namespace Parser;

public class Parser
{
  private readonly TokenStream tokens;

  public Parser(string source)
  {
    tokens = new TokenStream(source);
  }

  /// <summary>
  /// program = { top_level_declaration }, main_function ;
  /// </summary>
  public List<AstNode> ParseProgram()
  {
    List<AstNode> expressions = new();
    while (IsTopLevelDeclaration() && tokens.Peek().Type != TokenType.EndOfFile)
    {
      expressions.Add(ParseTopLevelDeclaration());
    }

    if (!IsTopLevelDeclaration())
    {
      expressions.Add(ParseMainFunction());
    }
    else
    {
      throw new UnexpectedLexemeException(TokenType.Main, tokens.Peek());
    }

    return expressions;
  }

  private bool IsTopLevelDeclaration()
  {
    return !(tokens.Peek().Type == TokenType.Func && tokens.Peek(1).Type == TokenType.Main);
  }

  /// <summary>
  /// main_function = "func", "main", ":", "void", "(", ")", block ;
  /// </summary>
  private Expression ParseMainFunction()
  {
    Match(TokenType.Func);
    Match(TokenType.Main);
    Match(TokenType.ColonTypeIndication);
    Match(TokenType.Void);
    Match(TokenType.OpenParenthesis);
    Match(TokenType.CloseParenthesis);
    return ParseBlock();
  }

  /// <summary>
  /// block = "{", { statement }, "}" ;
  /// </summary>
  private Expression ParseBlock()
  {
    Match(TokenType.OpenCurlyBrace);

    List<Expression> exprs = [];
    while (tokens.Peek().Type != TokenType.CloseCurlyBrace)
    {
      Expression state = ParseStatement();
      exprs.Add(state);
    }

    Match(TokenType.CloseCurlyBrace);
    return new SequenceExpression(exprs);
  }

  /// <summary>
  /// statement = assignment_statement | empty_statement  | value_declaration | input_statement
  /// | print_statement | if_statement | block | while_statement | break_statement | continue_statement
  /// | return_statement | function_call_statement ;
  /// </summary>
  private Expression ParseStatement()
  {
    Token t = tokens.Peek();

    switch (t.Type)
    {
      case TokenType.OpenCurlyBrace:
        return ParseBlock();
      case TokenType.Semicolon:
        tokens.Advance();
        return new EmptyExpression();
      case TokenType.Identifier:
        string name = tokens.Peek().Value!.ToString();
        tokens.Advance();

        Expression expr;
        if (tokens.Peek().Type == TokenType.Assignment)
        {
          expr = ParseAssignableExpr(name);
        }
        else if (tokens.Peek().Type == TokenType.OpenParenthesis)
        {
          expr = ParseFunctionCall(name);
          Match(TokenType.Semicolon);
        }
        else
        {
          throw new UnexpectedLexemeException(t.Type, t);
        }

        return expr;
      default:
        throw new UnexpectedLexemeException(t.Type, t);
    }
  }

  /// <summary>
  /// assignment_statement = identifier, "=", expression, ";" ;
  /// </summary>
  private Expression ParseAssignableExpr(string name)
  {
    Match(TokenType.Assignment);
    Expression left = new VariableExpression(name);
    Expression right = ParseExpr();
    Match(TokenType.Semicolon);
    return new AssignmentExpression(left, right);
  }

  /// <summary>
  /// top_level_declaration = value_declaration | function_declaration ;
  /// </summary>
  private Declaration ParseTopLevelDeclaration()
  {
    switch (tokens.Peek().Type)
    {
      case TokenType.Const:
      case TokenType.Let:
        return ParseValueDeclaration();
      default:
        throw new UnexpectedLexemeException(tokens.Peek().Type, tokens.Peek());
    }
  }

  /// <summary>
  /// value_declaration = variable_declaration | constant_declaration;
  /// </summary>
  private Declaration ParseValueDeclaration()
  {
    switch (tokens.Peek().Type)
    {
      case TokenType.Const:
        return ParseConstantDeclaration();
      case TokenType.Let:
        return ParseVariableDeclaration();
      default:
        throw new UnexpectedLexemeException(tokens.Peek().Type, tokens.Peek());
    }
  }

  /// <summary>
  /// constant_declaration = "const", identifier, ":", type, "=", expression, ";" ;
  /// </summary>
  private Declaration ParseConstantDeclaration()
  {
    tokens.Advance();
    Token t = tokens.Peek();

    if (t.Type != TokenType.Identifier || t.Value == null)
    {
      throw new UnexpectedLexemeException(TokenType.Identifier, t);
    }

    string name = tokens.Peek().Value!.ToString();
    tokens.Advance();

    Match(TokenType.ColonTypeIndication);
    string type = ParseType();

    Match(TokenType.Assignment);
    Expression value = ParseExpr();

    Match(TokenType.Semicolon);

    return new ConstantDeclaration(name, type, value);
  }

  /// <summary>
  /// variable_declaration = "let", identifier, ":", type ["=", expression ], ";" ;
  /// </summary>
  private Declaration ParseVariableDeclaration()
  {
    tokens.Advance();
    string name = tokens.Peek().Value!.ToString();

    tokens.Advance();
    Match(TokenType.ColonTypeIndication);
    string type = ParseType();

    Expression? value = null;

    if (tokens.Peek().Type == TokenType.Assignment)
    {
      tokens.Advance();
      value = ParseExpr();
    }

    Match(TokenType.Semicolon);

    return new VariableDeclaration(name, type, value);
  }

  private string ParseType()
  {
    Token t = tokens.Peek();
    switch (t.Type)
    {
      case TokenType.Int:
        return "int";
      case TokenType.Float:
        return "float";
      default:
        throw new UnexpectedLexemeException(t.Type, t);
    }
  }

  /// <summary>
  /// expression = logical_or ;
  /// </summary>
  private Expression ParseExpr()
  {
    return ParseOrExpr();
  }

  /// <summary>
  /// logical_or = logical_and, { "||", logical_and } ;
  /// </summary>
  private Expression ParseOrExpr()
  {
    return ParseAndExpr();
  }

  /// <summary>
  /// logical_and = comparison_expression, { "&&", comparison_expression } ;
  /// </summary>
  private Expression ParseAndExpr()
  {
    return ParseComparisonExpr();
  }

  /// <summary>
  /// comparison_expression = additive_expression, [ ("<" | ">" | "<=" | ">=" | "==" | "!="), additive_expression ] ;
  /// </summary>
  private Expression ParseComparisonExpr()
  {
    return ParseAdditiveExpr();
  }

  /// <summary>
  /// additive_expression = term_expression, { ("+" | "-"), term_expression } ;
  /// </summary>
  private Expression ParseAdditiveExpr()
  {
    Expression value = ParseTermExpr();
    while (true)
    {
      switch (tokens.Peek().Type)
      {
        case TokenType.Plus:
          tokens.Advance();
          value = new BinaryOperationExpression(value, BinaryOperation.Plus, ParseTermExpr());
          break;
        case TokenType.Minus:
          tokens.Advance();
          value = new BinaryOperationExpression(value, BinaryOperation.Minus, ParseTermExpr());
          break;
        default:
          return value;
      }
    }
  }

  /// <summary>
  /// term_expression = unary_expression, { ("*" | "/"), unary_expression } ;
  /// </summary>
  private Expression ParseTermExpr()
  {
    Expression value = ParseUnariExpr();
    while (true)
    {
      switch (tokens.Peek().Type)
      {
        case TokenType.Multiplication:
          tokens.Advance();
          value = new BinaryOperationExpression(value, BinaryOperation.Multiplication, ParseUnariExpr());
          break;
        case TokenType.Division:
          tokens.Advance();
          value = new BinaryOperationExpression(value, BinaryOperation.Division, ParseUnariExpr());
          break;
        default:
          return value;
      }
    }
  }

  /// <summary>
  /// unary_expression = {"-" | "!" }, primary_expression ;
  /// </summary>
  private Expression ParseUnariExpr()
  {
    switch (tokens.Peek().Type)
    {
      case TokenType.Minus:
        return new UnaryOperationExpression(UnaryOperation.Minus, ParsePrimaryExpr());
      default:
        return ParsePrimaryExpr();
    }
  }

  /// <summary>
  /// primary_expression = identifier | literal | "(", expression, ")" | function_call ;
  /// </summary>
  private Expression ParsePrimaryExpr()
  {
    Token t = tokens.Peek();
    switch (t.Type)
    {
      case TokenType.Identifier:
        string name = t.Value!.ToString();
        tokens.Advance();

        if (tokens.Peek().Type == TokenType.OpenParenthesis)
        {
          return ParseFunctionCall(name);
        }
        else
        {
          return new VariableExpression(name);
        }

      case TokenType.NumberLiteral:
        return ParseLiteral();
      case TokenType.OpenParenthesis:
        tokens.Advance();
        Expression value = ParseExpr();
        Match(TokenType.CloseParenthesis);
        return value;
      default:
        throw new UnexpectedLexemeException(t.Type, t);
    }
  }

  /// <summary>
  /// function_call = identifier, "(", [ expression_list ], ")" ;
  /// </summary>
  private Expression ParseFunctionCall(string name)
  {
    Match(TokenType.OpenParenthesis);
    List<Expression> args = [];
    if (tokens.Peek().Type != TokenType.CloseParenthesis)
    {
      args = ParseExprList();
    }

    Match(TokenType.CloseParenthesis);

    return new FunctionCallExpression(name, args);
  }

  /// <summary>
  /// expression_list = expression, { ",", expression } ;
  /// </summary>
  private List<Expression> ParseExprList()
  {
    List<Expression> values = new List<Expression> { ParseExpr() };
    while (tokens.Peek().Type == TokenType.Comma)
    {
      tokens.Advance();
      values.Add(ParseExpr());
    }

    return values;
  }

  /// <summary>
  /// literal = integer_literal | float_literal | string_literal | boolean_literal ;
  /// </summary>
  private Expression ParseLiteral()
  {
    Token t = tokens.Peek();
    switch (t.Type)
    {
      case TokenType.NumberLiteral:
        return ParseNumberLiteral();
      default:
        throw new UnexpectedLexemeException(t.Type, t);
    }
  }

  /// <summary>
  /// integer_literal = digit, { digit } ;
  /// float_literal = digit, { digit }, ".", digit, { digit } ;
  /// </summary>
  private Expression ParseNumberLiteral()
  {
    Token t = tokens.Peek();
    tokens.Advance();
    string text = t.Value!.ToString();

    if (int.TryParse(text, out int i))
    {
      return new LiteralExpression(ValueType.Int, new Value(i));
    }
    else
    {
      if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out float f))
      {
        return new LiteralExpression(ValueType.Float, new Value(f));
      }
      else
      {
        throw new UnexpectedLexemeException(t.Type, t);
      }
    }
  }

  private Token Match(TokenType expected)
  {
    Token t = tokens.Peek() ?? new Token(TokenType.EndOfFile);

    if (t.Type != expected || t.Type == TokenType.EndOfFile)
    {
      throw new UnexpectedLexemeException(expected, t);
    }

    tokens.Advance();
    return t;
  }
}