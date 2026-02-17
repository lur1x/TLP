/// Программа для вычисления площади и периметр треугольника по координатам вершин
let x1 : float; let y1 : float;
let x2 : float; let y2 : float;
let x3 : float; let y3 : float;

input(x1); input(y1);
input(x2); input(y2);
input(x3); input(y3);

/// Вычисление длин сторон
let a : float = ((x2 - x1) ** 2.0 + (y2 - y1) ** 2.0) ** 0.5;
let b : float = ((x3 - x2) ** 2.0 + (y3 - y2) ** 2.0) ** 0.5;
let c : float = ((x1 - x3) ** 2.0 + (y1 - y3) ** 2.0) ** 0.5;

/// Полупериметр
let p : float = (a + b + c) / 2.0;

/// Площадь по формуле Герона
let area : float = (p * (p - a) * (p - b) * (p - c)) ** 0.5;

print(p * 2, area);