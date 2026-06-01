using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Задание 1");

            Console.Write("Введите число a: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите степень n: ");
            int n = Convert.ToInt32(Console.ReadLine());

            int result = 1;

            for (int i = 0; i < n; i++)
            {
                result = result * a;
            }

            Console.WriteLine("a^n = " + result);

            Console.WriteLine();



            Console.WriteLine("Задание 2");

            Console.Write("Введите число x (x >= 100): ");
            int x = Convert.ToInt32(Console.ReadLine());

            string s = x.ToString();

            char secondDigit = s[1];

            // Формируем новое число без второй цифры
            string newNumber =
                s[0].ToString() +
                s.Substring(2);

            // Приписываем вторую цифру в конец
            newNumber = newNumber + secondDigit;

            int resultN = Convert.ToInt32(newNumber);

            Console.WriteLine("n = " + resultN);

            Console.ReadKey();
        }
    }
}