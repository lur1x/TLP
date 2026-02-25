```
# Калькулятор суммы, разности, произведения и частного

func main:void() 
{
    let a:float;
    let b:float;

    /*
    Вводим значения
    */
    
    input(a);
    input(b);
    
    let sum:float = a + b;
    let diff:float = a - b;
    let prod:float = a * b;
    let quot:float = a / b;
    
    print(sum, diff, prod, quot);
}
```