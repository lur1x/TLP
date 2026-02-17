/// Поиск максимума из двух чисел
let x: int;
let y: int;

print("Enter two integers:");
input(x);
input(y);

if (x > y) 
{
    print("Max is:", x);
}
else 
{
    if (x < y) 
    {
        print("Max is:", y);
    } 
    else 
    {
        print("Both are equal:", x);
    }
}

