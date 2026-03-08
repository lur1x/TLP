using System.Globalization;

namespace Runtime;

public class Value : IEquatable<Value>
{
  public const double Tolerance = 0.001d;

  public static readonly Value Void = new(VoidValue.Value);
  public static readonly Value Nil = new(NilValue.Value);

  private readonly object value;

  public Value(float value)
  {
    this.value = value;
  }

  public Value(int value)
  {
    this.value = value;
  }

  private Value(object value)
  {
    this.value = value;
  }

  /// <summary>
  /// Определяет, является ли значение целым числом.
  /// </summary>
  public bool IsInt()
  {
    return value switch
    {
      int => true,
      _ => false,
    };
  }

  /// <summary>
  /// Возвращает значение как целое число либо бросает исключение.
  /// </summary>
  public int AsInt()
  {
    return value switch
    {
      int i => i,
      _ => throw new InvalidOperationException($"Value {value} is not an integer"),
    };
  }

  public bool IsFloat()
  {
    return value switch
    {
      float => true,
      _ => false,
    };
  }

  /// <summary>
  /// Возвращает значение как вещественное число либо бросает исключение.
  /// </summary>
  public float AsFloat()
  {
    return value switch
    {
      float i => i,
      _ => throw new InvalidOperationException($"Value {value} is not an integer"),
    };
  }

  public bool Equals(Value? other)
  {
    if (other is null)
    {
      return false;
    }

    return value switch
    {
      // Числа сравниваются по значению.
      int i => other.AsInt() == i,

      // Вещественные числа сравниваются с погрешностью.
      float d => Math.Abs(other.AsFloat() - d) < Tolerance,

      // Пустые значения всегда равны.
      VoidValue => true,

      // Несуществующая структура равна сама себе и не равна никаким другим.
      NilValue => other.value is NilValue,

      _ => throw new NotImplementedException(),
    };
  }

  public override bool Equals(object? obj)
  {
    return Equals(obj as Value);
  }

  public override int GetHashCode()
  {
    return value.GetHashCode();
  }
}