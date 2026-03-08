using Ast.Attributes;

using ValueType = Runtime.ValueType;

namespace Ast.Declarations;

public abstract class Declaration : AstNode
{
  private AstAttribute<ValueType> _resultType;

  /// <summary>
  /// Тип результата объявления.
  /// </summary>
  public ValueType ResultType
  {
    get => _resultType.Get();

    set => _resultType.Set(value);
  }
}