using System;

/// <summary>
/// Клас для виведення привітання
/// </summary>
public class Hello
{
    /// <summary>
    /// Виводить привітання на консоль
    /// </summary>
    public void DisplayGreeting()
    {
        Console.WriteLine("Hello, World!");
    }
}

class Program
{
    static void Main()
    {
        // Створюємо екземпляр класу Hello
        Hello myGreetings = new Hello();

        // Викликаємо метод DisplayGreeting для виведення привітання
        myGreetings.DisplayGreeting();
    }
}
