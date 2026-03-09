using Ast.Attributes;
using Ast.Expressions;

namespace Ast.Declarations;

public sealed class ConstantDeclaration : AbstractVariableDeclaration
{
  private AstAttribute<AbstractTypeDeclaration?> declaredType;

  public ConstantDeclaration(string name, string declaredTypeName, Expression initialValue)
  : base(name)
  {
    DeclaredTypeName = declaredTypeName;
    InitialValue = initialValue;
  }

  public string DeclaredTypeName { get; }

  public Expression InitialValue { get; }

  public AbstractTypeDeclaration? DeclaredType
  {
    get => declaredType.Get();
    set => declaredType.Set(value);
  }

  public override void Accept(IAstVisitor visitor)
  {
    visitor.Visit(this);
  }
}