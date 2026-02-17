# Синтаксис языка

## Общая структура программы

Программа представляет собой последовательность инструкций, выполняемых сверху вниз.  
Каждая инструкция завершается точкой с запятой `;`. Могут быть пустые инструкции (просто `;`), но они игнорируются.

## Объявление переменных

Язык поддерживает 2 вида переменных:
- изменяемые: объявляются с помощью ключевого слова `let`
- неизменяемые или константы: объявляются с помщью ключего слова `const`

## Ввод/вывод значений
Ввод значений осуществляется с помощью инструкции `input()`, а вывод `print()` 


## Выражения

Выражения строятся с учётом приоритетов операций 

| Приоритет | Операции    | Ассоциативность |
| --------- | ----------- | --------------- |
| 1         | `( )`       | –               |
| 2         | унарный `-` | справа          |
| 3         | `**`        | справа          |
| 4         | `*` `/`     | слева           |
| 5         | `+` `-`     | слева           |
| 6         | `=`         | справа          |

## Грамматика языка в нотации EBNF

```
(* Программа *)
program = { statement } ;

(* Инструкции *)
statement = assignment_statement
| empty_statement 
| value_declaration
| input_statement
| print_statement ;

(* Присваивание *)
assignment_statement = identifier, "=", expression, ";" ;

(* Пустая инструкция *)
empty_statement = ";" ;

(* Ввод *)
input_statement = "input", "(", identifier, ")" ";" ;

(* Вывод *)
print_statement = "print", "(", [ expression_list ], ")" ";" ;

(* Список выражений *)
expression_list = expression, { ",", expression } ;

(* Объявление переменной*)
value_declaration = variable_declaration
| constant_declaration;

(* Объявление изменяемой переменной *)
variable_declaration = "let", identifier, ":", type  "=", expression , ";" ;

(* Объявление неизменяемой переменной *)
constant_declaration = "const", identifier, ":", type "=", expression, ";" ;

(* Типы *)
type = "int" | "float" | "str";

(* Выражения *)
expression = term_expression, { ("+" | "-"), term_expression } ;
term_expression = power_expression, { ("*" | "/"), power_expression } ;
power_expression = unary_expression, [ "**", power_expression ] ;
unary_expression = [ "-" ], primary_expression ;
primary_expression = identifier
| literal
| "(" expression ")" ;

(* Литералы *)
literal = integer_literal | float_literal | string_literal;
integer_literal = digit, { digit } ;
float_literal = digit, { digit }, ".", digit, { digit } ;
string_literal = '"', { любой символ }, '"' ж

(* Идентификаторы *)
identifier = ( letter | "_" ), { letter | digit | "_" } ;

(* Базовые символы *)
letter = "A" | "B" | "C" | "D" | "E" | "F" | "G" | "H" | "I" | "J" | "K"
| "L" | "M" | "N" | "O" | "P" | "Q" | "R" | "S" | "T" | "U" | "V"
| "W" | "X" | "Y" | "Z"
| "a" | "b" | "c" | "d" | "e" | "f" | "g" | "h" | "i" | "j" | "k"
| "l" | "m" | "n" | "o" | "p" | "q" | "r" | "s" | "t" | "u" | "v"
| "w" | "x" | "y" | "z" ;

digit = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" ;

```