```
# Локальная переменная

func main:void() 
{
    let a:int;
    let b:int;

    print(a);
    print(b);

    {
        a = 5;
        b = 2;
        print(a, b);
    }
    
    print(a, b);
}
```