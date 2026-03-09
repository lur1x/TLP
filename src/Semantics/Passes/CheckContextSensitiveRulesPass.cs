using Ast.Declarations;
using Ast.Expressions;

using Semantics.Exceptions;

namespace Semantics.Passes;

/// <summary>
/// Проверяет соблюдение контекстно-зависимых правил языка.
/// </summary>
/// <remarks>
/// Контекстно-зависимые правила не могли быть проверены при синтаксическом анализе, поскольку синтаксический анализатор
///  разбирает контекстно-свободную грамматику.
/// </remarks>
public sealed class CheckContextSensitiveRulesPass : AbstractPass
{
  // Стек контекстов выражений используется для проверки контекстно-зависимых правил.
  // private readonly Stack<ExpressionContext> _expressionContextStack;
  private enum ExpressionContext
  {
    Default,
    InsideLoop,
  }

  /// <summary>
  /// Проверяет корректность программы с точки зрения использования функций.
  /// </summary>
  /// <exception cref="InvalidFunctionCallException">Бросается при неправильном вызове функций.</exception>
  public override void Visit(FunctionCallExpression e)
  {
    base.Visit(e);

    if (e.Arguments.Count != e.Function.Parameters.Count)
    {
      throw new InvalidFunctionCallException(
          $"Function {e.Name} requires {e.Function.Parameters.Count} arguments, got {e.Arguments.Count}"
      );
    }
  }

  public override void Visit(AssignmentExpression e)
  {
    base.Visit(e);

    // Проверяем контекстно-зависимые правила присваивания:
    // 1) Левая часть присваивания должна быть lvalue. Левая часть только переменная(let)
    if (!IsLvalue(e.Left))
    {
      throw new InvalidAssignmentException("Left side of assignment must be a lvalue");
    }
  }

  private static bool IsLvalue(Expression e)
  {
    if (e is VariableExpression)
    {
      return true;
    }

    return false;
  }
}