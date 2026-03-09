using Ast.Declarations;
using Ast.Expressions;

using Semantics.Symbols;

namespace Semantics.Passes;

/// <summary>
/// Проход по AST, устанавливающий соответствие имён и символов (объявлений).
/// </summary>
public sealed class ResolveNamesPass : AbstractPass
{
  /// <summary>
  /// В таблицу символов складываются объявления.
  /// </summary>
  private readonly SymbolsTable _symbols;

  public ResolveNamesPass(SymbolsTable globalSymbols)
  {
    _symbols = globalSymbols;
  }

  public override void Visit(FunctionCallExpression e)
  {
    base.Visit(e);

    _symbols.GetFunctionDeclaration(e.Name);
  }

  public override void Visit(VariableDeclaration d)
  {
    base.Visit(d);

    d.DeclaredType = d.DeclaredTypeName != null ? _symbols.GetTypeDeclaration(d.DeclaredTypeName) : null;
    _symbols.DeclareVariable(d);
  }

  public override void Visit(ConstantDeclaration d)
  {
    base.Visit(d);

    d.DeclaredType = d.DeclaredTypeName != null ? _symbols.GetTypeDeclaration(d.DeclaredTypeName) : null;
    _symbols.DeclareVariable(d);
  }
}