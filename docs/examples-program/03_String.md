```
# Работа со строками

func main:void() 
{
    # Ввод строки
    let s:string;
    print("Enter a string:");
    input(s);
    
    # Длина строки
    let len:int = length(s);
    print("Length:", len);
    
    # Первый символ и остаток
    let first:string = substr(s, 0, 1);
    let rest:string = substr(s, 1, len - 1);
    print("First char:", first);
    print("Rest:", rest);
    
    # Замена подстроки
    let replaced:string = replace(s, "a", "o");
    print("Replaced 'a' with 'o':", replaced);
    
    # Конкатенация
    let combined:string = s + " - concatenated";
    print("Combined:", combined);
    
    # Преобразование строки в число
    let numStr:string = "123";
    let num:int = toInt(numStr);
    print("Parsed int:", num);
    
    let floatStr:string = "45.67";
    let f:float = toFloat(floatStr);
    print("Parsed float:", f);
    
    /* Попытка преобразования введенной строки в число
    (если ввод не число, возникнет ошибка выполнения) */
    let val:int = parseInt(s);
    print("Parsed input as int:", val);
}
```

```
```