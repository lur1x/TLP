# Синтаксис языка

## Объявление переменных

Язык поддерживает 2 вида переменных:
- изменяемые: объявляются с помощью ключевого слова `let`
- неизменяемые или константы: объявляются с помщью ключего слова `const`

## Ввод/вывод значений
Ввод значений осуществляется с помощью инструкции `input()`, а вывод `print()` 


## Выражения

Выражения строятся с учётом приоритетов операций 

| Уровень | Операции                          | Ассоциативность |
|---------|-----------------------------------|-----------------|
| 1       | `( )`                             | –               |
| 2       | унарные: `-`, `!`                 | справа          |
| 3       | `**`                              | справа          |
| 4       | `*`, `/`                          | слева           |
| 5       | `+`, `-`                          | слева           |
| 6       | `<`, `>`, `<=`, `>=`, `==`, `!=`  | слева           |
| 7       | `&&`                              | слева           |
| 8       | `||`                              | слева           |

## Грамматика языка в нотации EBNF

```
(* Программа *)
program = { top_level_declaration }, main_function ;

top_level_declaration = value_declaration | function_declaration ;

(* Объявление функции *)
function_declaration = "func", identifier, ":", type, "(", [ parameter_list ], ")" block ;
parameter_list = parameter, { ",", parameter } ;
parameter= identifier, ":", type ;

(* Главная функция *)
main_function = "func", "main", ":", "void", "(", ")", block ;

(* Инструкции *)
statement = assignment_statement
| empty_statement 
| value_declaration
| input_statement
| print_statement 
| if_statement
| block 
| while_statement
| break_statement
| continue_statement
| return_statement
| function_call_statement ;

(* Присваивание *)
assignment_statement = identifier, "=", expression, ";" ;

(* Пустая инструкция *)
empty_statement = ";" ;

(* Блок *)
block = "{", { statement }, "}" ;

(* Управлюящие конструкции *)

if_statement =
"if", "(", expression, ")", block, [ "else", block ; ] ;

while_statement = "while", "(", expression, ")", block ;

break_statement = "break", ";" ;

continue_statement = "continue", ";" ;

return_statement = "return", [ expression ], ";" ;

function_call_statement = function_call, ";" ;

function_call   = identifier, "(", [ expression_list ], ")" ;

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
type = "int" | "float" | "string" | "bool" | "void" ;

(* Выражения *)
expression = logical_or ;
logical_or = logical_and, { "||", logical_and } ;
logical_and = comparison_expression, { "&&", comparison_expression } ;
comparison_expression = additive_expression, [ ("<" | ">" | "<=" | ">=" | "==" | "!="), additive_expression ] ;
additive_expression = term_expression, { ("+" | "-"), term_expression } ;
term_expression = power_expression, { ("*" | "/"), power_expression } ;
power_expression = unary_expression, [ "**", power_expression ] ;
unary_expression = [ "-" | "!" ], primary_expression ;
primary_expression = identifier
| literal
| "(" expression ")" 
| function_call ;

(* Литералы *)
literal = integer_literal | float_literal | string_literal | boolean literal ;
integer_literal = digit, { digit } ;
float_literal = digit, { digit }, ".", digit, { digit } ;
string_literal = '"', { любой символ UTF-16 }, '"' ж
boolean_literal = "true" | "false" ;

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