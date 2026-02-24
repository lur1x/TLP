```
# Суммирование чисел до нуля с использованием break и continue

func main:void() 
{
    let sum:int = 0;
    while (true) 
    {
        let num:int;
        input(num);
        
        if (num == 0) 
        {
            break; # выход из цикла при вводе 0
        }

        if (num < 0) 
        {
            continue; # пропуск отрицательных чисел
        }

        sum = sum + num;
    }

    print("Sum of positives:", sum);
}
```