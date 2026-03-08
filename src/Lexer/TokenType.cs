namespace Lexer;

public enum TokenType
{
  /// <summary>
  /// Идентификатор (имя переменной, функции, структуры)
  /// </summary>
  Identifier,

  /// <summary>
  /// Объявление константы const
  /// </summary>
  Const,

  /// <summary>
  /// Объявление переменной let
  /// </summary>
  Let,

  /// <summary>
  /// Тип данных int
  /// </summary>
  Int,

  /// <summary>
  /// Тип данных float
  /// </summary>
  Float,

  /// <summary>
  /// Числовой литерал (int или float)
  /// </summary>
  NumberLiteral,

  /// <summary>
  /// Тип данных void
  /// </summary>
  Void,

  /// <summary>
  /// Ключевое слово func
  /// </summary>
  Func,

  /// <summary>
  /// Ключевое слово input
  /// </summary>
  Input,

  /// <summary>
  /// Ключевое слово print
  /// </summary>
  Print,

  /// <summary>
  ///  Разделитель элементов ','
  /// </summary>
  Comma,

  /// <summary>
  /// Оператор присваивания равно "=".
  /// </summary>
  Assignment,

  /// <summary>
  /// Арифметический оператор плюс "+"
  /// </summary>
  Plus,

  /// <summary>
  /// Арифметический оператор минус "-"
  /// </summary>
  Minus,

  /// <summary>
  /// Арифметический оператор умножения "*"
  /// </summary>
  Multiplication,

  /// <summary>
  /// Арифметический оператор деления "/"
  /// </summary>
  Division,

  /// <summary>
  /// Оператор указания типа (разделитель типа) ":"
  /// </summary>
  ColonTypeIndication,

  /// <summary>
  /// Основная ф-ия "main"
  /// </summary>
  Main,

  /// <summary>
  ///  Открывающая круглая скобка '('.
  /// </summary>
  OpenParenthesis,

  /// <summary>
  ///  Закрывающая круглая скобка ')'.
  /// </summary>
  CloseParenthesis,

  /// <summary>
  /// Открывающая фигурная скобка '{'.
  /// </summary>
  OpenCurlyBrace,

  /// <summary>
  /// Закрывающая фигурная скобка '}'.
  /// </summary>
  CloseCurlyBrace,

  /// <summary>
  ///  Конец инструкции ';'
  /// </summary>
  Semicolon,

  /// <summary>
  ///  Конец файла.
  /// </summary>
  EndOfFile,

  /// <summary>
  ///  Недопустимая лексема.
  /// </summary>
  Error,
}