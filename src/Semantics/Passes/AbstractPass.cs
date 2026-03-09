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
    foreach (Expression nested in e.Sequence)
    {
      nested.Accept(this);
    }
  }

  public virtual void Visit(EmptyExpression e)
  {
  }

  public virtual void Visit(BinaryOperationExpression e)
  {
    e.Left.Accept(this);
    e.Right.Accept(this);
  }

  public virtual void Visit(UnaryOperationExpression e)
  {
    e.Operand.Accept(this);
  }

  public virtual void Visit(FunctionCallExpression e)
  {
    foreach (Expression argument in e.Arguments)
    {
      argument.Accept(this);
    }
  }

  public virtual void Visit(AssignmentExpression e)
  {
    e.Left.Accept(this);
    e.Right.Accept(this);
  }

  public virtual void Visit(VariableDeclaration d)
  {
    d.InitialValue!.Accept(this);
  }

  public virtual void Visit(ConstantDeclaration d)
  {
    d.InitialValue.Accept(this);
  }
}