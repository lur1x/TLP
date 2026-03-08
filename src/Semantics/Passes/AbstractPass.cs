using Ast;
using Ast.Declarations;
using Ast.Expressions;

namespace Semantics.Passes;

/// <summary>
/// Базовый класс для проходов по AST с целью вычисления атрибутов и семантических проверок.
/// </summary>
public abstract class AbstractPass : IAstVisitor
{
  public virtual void Visit(LiteralExpression e)
  {
  }

  public virtual void Visit(VariableExpression e)
  {
  }

  public virtual void Visit(SequenceExpression e)
  {
  }

  public virtual void Visit(EmptyExpression e)
  {
  }

  public virtual void Visit(BinaryOperationExpression e)
  {
  }

  public virtual void Visit(UnaryOperationExpression e)
  {
  }

  public virtual void Visit(FunctionCallExpression e)
  {
  }

  public virtual void Visit(AssignmentExpression e)
  {
  }

  public virtual void Visit(VariableDeclaration d)
  {
  }

  public virtual void Visit(ConstantDeclaration d)
  {
  }
}