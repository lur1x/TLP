```
# Вычисление факториала

func factorial:int(n:int)
{
    let result:int = 1;
    let i:int = 1;

    while (i <= n)
    {
        result = result * i;
        i = i + 1;
    }

    return result;
}

func main:void()
{
    let number:int;
    print("Enter a number:");
    input(number);
    
    let fact:int = factorial(number);
    print("Factorial of", number, "is", fact);
}
```