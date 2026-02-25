```
# Булев тип, логические операции, ветвления 

func main:void() 
{
    let a:int;
    let b:float;
    let s1:string;
    let s2:string;
    
    print("Enter an integer:");
    input(a);
    print("Enter a float:");
    input(b);
    print("Enter first string:");
    input(s1);
    print("Enter second string:");
    input(s2);
    
    # Сравнения чисел
    let intCompare:bool = a > 10;
    let floatCompare:bool = b <= 5.5;
    
    # Сравнения строк
    let stringEqual:bool = s1 == s2;
    let stringLess:bool = s1 < s2;   # лексикографическое сравнение с учётом длины
    
    # Логические комбинации
    let complexCondition:bool = (a > 0 && b > 0.0) || (s1 == "yes");
    
    # Ветвление с использованием булевых переменных
    if (intCompare) 
    {
        print("a is greater than 10");
    } 
    else 
    {
        print("a is not greater than 10");
    }
    
    if (floatCompare) 
    {
        print("b is less than or equal to 5.5");
    }
    
    if (stringEqual) 
    {
        print("Strings are equal");
    } 
    else 
    {
        print("Strings are different");
        if (stringLess) 
        {
            print(s1, "is less than", s2);
        } 
        else 
        {
            print(s1, "is greater than", s2);
        }
    }
    
    # Использование логического НЕ
    if (!stringEqual) 
    {
        print("Strings are not equal (confirmed by !)");
    }
    
    # Демонстрация short‑circuit вычисления
    let safeDivision:bool = (a != 0) && (b / a > 1.0); # если a=0 деление не выполняется

    if (safeDivision) 
    {
        print("b/a > 1.0");
    }
    
    # Вложенные условия и использование булевых переменных в выражениях
    if (complexCondition) 
    {
        print("Complex condition is true");
    } 
    else 
    {
        print("Complex condition is false");
    }
    
    # Булевы литералы
    let alwaysTrue:bool = true;
    if (alwaysTrue) 
    {
        print("This always prints");
    }
}
```