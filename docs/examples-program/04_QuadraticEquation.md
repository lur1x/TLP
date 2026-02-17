/// Решение квадратного уравнения

let a: float;
let b: float;
let c: float;

print("Enter coefficients a, b, c:");
input(a);
input(b);
input(c);

let d: float = b**2 - 4.0 * a * c;

if (d > 0.0) 
{
    let sqrt_d: float = d ** 0.5;     
    let x1: float = (-b + sqrt_d) / (2.0 * a);
    let x2: float = (-b - sqrt_d) / (2.0 * a);
    print("Two roots:", x1, x2);
} 
else 
{
    if (d == 0.0) 
    {
        let x: float = - b / (2.0 * a);
        print("One root:", x);
    } 
    else 
    {
        print("No real roots");
    }
}