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
  private SymbolsTable _symbols;

  public ResolveNamesPass(SymbolsTable globalSymbols)
  {
    _symbols = globalSymbols;
  }
}