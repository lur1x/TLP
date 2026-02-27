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
| 2       | унарный `-`                       | справа          |
| 3       | `*`, `/`                          | слева           |
| 4       | `+`, `-`                          | слева           |

## Грамматика языка в нотации EBNF

```
(* Программа *)
program = { top_level_declaration }, main_function ;

top_level_declaration = value_declaration ;

(* Главная функция *)
main_function = "func", "main", ":", "void", "(", ")", block ;

(* Инструкции *)
statement = assignment_statement
| empty_statement 
| value_declaration
| input_statement
| print_statement 
| block ;

(* Присваивание *)
assignment_statement = identifier, "=", expression, ";" ;

(* Пустая инструкция *)
empty_statement = ";" ;

(* Блок *)
block = "{", { statement }, "}" ;

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
variable_declaration = "let", identifier, ":", type ["=", expression ], ";" ;

(* Объявление неизменяемой переменной *)
constant_declaration = "const", identifier, ":", type, "=", expression, ";" ;

(* Типы *)
type = "int" | "float" | "void" ;

(* Выражения *)
expression = additive_expression ;

additive_expression = term_expression, { ("+" | "-"), term_expression } ;

term_expression = unary_expression, { ("*" | "/"), unary_expression } ;

unary_expression = {"-"}, primary_expression ;

primary_expression = identifier
| literal
| "(", expression, ")" ;

```