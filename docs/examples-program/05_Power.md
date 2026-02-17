/// Возведение в степень через умножение в цикле

func power : int (base: int, exp: int)
{
    let result: int = 1;
    let i: int = 0;
    while (i < exp)
    {
        result = result * base;
        i = i + 1;
    }
    return result;
}

func main : void ()
{
    let a: int;
    let b: int;
    print("Enter base and exponent:");
    input(a);
    input(b);
    let p: int = power(a, b);
    print(a, "raised to power", b, "is", p);
}