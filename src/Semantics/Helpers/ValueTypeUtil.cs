using ValueType = Runtime.ValueType;

namespace Semantics.Helpers;

public static class ValueTypeUtil
{
  public static bool AreCompatibleTypes(ValueType a, ValueType b)
  {
    return a == b;
  }
}