#nullable disable

using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace TextEditorApp
{
    // Класс текстового файла
    [Serializable]
    public class TextFile
    {
        public string FileName { get; set; }
        public string Content { get; set; }

        public TextFile()
        {

        }

        public TextFile(string fileName, string content)
        {
            FileName = fileName;
            Content = content;
        }

        // Сохранение в обычный txt
        public void Save()
        {
            File.WriteAllText(FileName, Content);
        }

        // Загрузка
        public void Load()
        {
            if (File.Exists(FileName))
            {
                Content = File.ReadAllText(FileName);
            }
        }

        // Бинарная сериализация
        public void SaveBinary(string path)
        {
#pragma warning disable SYSLIB0011
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fs, this);
            }
#pragma warning restore SYSLIB0011
        }

        // Бинарная десериализация
        public static TextFile LoadBinary(string path)
        {
#pragma warning disable SYSLIB0011
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                return (TextFile)formatter.Deserialize(fs);
            }
#pragma warning restore SYSLIB0011
        }

        // XML сериализация
        public void SaveXML(string path)
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(TextFile));

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fs, this);
            }
        }

        // XML десериализация
        public static TextFile LoadXML(string path)
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(TextFile));

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                return (TextFile)serializer.Deserialize(fs);
            }
        }
    }

    // Memento
    class FileMemento
    {
        public string Content { get; private set; }

        public FileMemento(string content)
        {
            Content = content;
        }
    }

    // Редактор
    class TextEditor
    {
        private TextFile file;

        private Stack<FileMemento> history =
            new Stack<FileMemento>();

        public TextEditor(TextFile file)
        {
            this.file = file;
        }

        // Изменение текста
        public void Edit(string newText)
        {
            history.Push(
                new FileMemento(file.Content));
            file.Content = newText;
        }

        // Откат
        public void Undo()
        {
            if (history.Count > 0)
            {
                FileMemento memento = history.Pop();

                file.Content = memento.Content;

                Console.WriteLine("Изменения отменены.");
            }
            else
            {
                Console.WriteLine("Нет сохраненных состояний.");
            }
        }

        public void Show()
        {
            Console.WriteLine();
            Console.WriteLine("Текст файла:");
            Console.WriteLine(file.Content);
        }

        public TextFile GetFile()
        {
            return file;
        }
    }

    // Поиск файлов
    class FileSearcher
    {
        public static void Search(string directory,
            string keyword)
        {
            string[] files =
                Directory.GetFiles(directory, "*.txt");

            Console.WriteLine();
            Console.WriteLine("Результаты поиска:");

            foreach (string file in files)
            {
                string text = File.ReadAllText(file);

                if (text.Contains(keyword))
                {
                    Console.WriteLine(file);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TextFile file =
                new TextFile("test.txt",
                "Пример текста");

            TextEditor editor =
                new TextEditor(file);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Показать текст");
                Console.WriteLine("2 - Изменить текст");
                Console.WriteLine("3 - Сохранить txt");
                Console.WriteLine("4 - Сохранить binary");
                Console.WriteLine("5 - Сохранить xml");
                Console.WriteLine("6 - Откат изменений");
                Console.WriteLine("7 - Поиск файлов");
                Console.WriteLine("8 - Выход");

                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        editor.Show();

                        break;

                    case "2":

                        Console.Write("Введите новый текст: ");

                        string newText =
                            Console.ReadLine();

                        editor.Edit(newText);

                        break;

                    case "3":

                        editor.GetFile().Save();

                        Console.WriteLine(
                            "Файл сохранен.");

                        break;

                    case "4":

                        editor.GetFile()
                            .SaveBinary("file.dat");

                        Console.WriteLine(
                            "Binary сохранение выполнено.");

                        break;

                    case "5":

                        editor.GetFile()
                            .SaveXML("file.xml");

                        Console.WriteLine(
                            "XML сохранение выполнено.");

                        break;

                    case "6":

                        editor.Undo();

                        break;

                    case "7":

                        Console.Write(
                            "Введите директорию: ");

                        string dir =
                            Console.ReadLine();

                        Console.Write(
                            "Введите ключевое слово: ");

                        string word =
                            Console.ReadLine();

                        FileSearcher.Search(dir, word);

                        break;

                    case "8":

                        exit = true;

                        break;

                    default:

                        Console.WriteLine(
                            "Неверный ввод");

                        break;
                }
            }
        }
    }
}
