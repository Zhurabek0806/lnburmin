#nullable disable

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TextCorrection
{
    class Program
    {
        static void Main(string[] args)
        {
            // Словарь ошибочных слов
            Dictionary<string, string> words =
                new Dictionary<string, string>();

            words.Add("првиет", "привет");
            words.Add("пирвет", "привет");
            words.Add("здраствуйте", "здравствуйте");
            words.Add("спосибо", "спасибо");
            words.Add("покаа", "пока");

            Console.Write("Введите путь к папке: ");

            string path = Console.ReadLine();

            // Получаем txt файлы
            string[] files =
                Directory.GetFiles(path, "*.txt");

            foreach (string file in files)
            {
                string text = File.ReadAllText(file);

                // Исправление ошибок
                foreach (var item in words)
                {
                    text = text.Replace(
                        item.Key,
                        item.Value);
                }

                // Замена телефонов
                text = Regex.Replace(
                    text,
                    @"\((\d{3})\)\s(\d{3})-(\d{2})-(\d{2})",
                    "+380 $1 $2 $3 $4"
                );

                // Сохраняем изменения
                File.WriteAllText(file, text);
                Console.WriteLine(
                    "Файл обработан: " + file);
            }

            Console.WriteLine();
            Console.WriteLine("Обработка завершена.");
        }
    }
}
