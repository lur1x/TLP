# Синтаксис языка

## Объявление переменных

Язык на данном этапе разработки поддерживает 1 вида переменных:
- изменяемые: объявляются с помощью ключевого слова `let`

## Вывод значений
Вывод значений осуществляется с помощью инструкции `print()` 


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
| print_statement 
| block ;

(* Присваивание *)
assignment_statement = identifier, "=", expression, ";" ;

(* Пустая инструкция *)
empty_statement = ";" ;

(* Блок *)
block = "{", { statement }, "}" ;

(* Вывод *)
print_statement = "print", "(", [ expression_list ], ")" ";" ;

(* Список выражений *)
expression_list = expression, { ",", expression } ;

(* Объявление переменной*)
value_declaration = variable_declaration
| constant_declaration;

(* Объявление изменяемой переменной *)
variable_declaration = "let", identifier, ":", type ["=", expression ], ";" ;

(* Типы *)
type = "int" | "void" ;

(* Выражения *)
expression = additive_expression ;

additive_expression = term_expression, { ("+" | "-"), term_expression } ;

term_expression = unary_expression, { ("*" | "/"), unary_expression } ;

unary_expression = {"-"}, primary_expression ;

primary_expression = identifier
| literal
| "(", expression, ")" ;

```