#nullable disable

using System;
using System.Collections.Generic;

namespace AnimalsProject
{
    // Базовый класс
    abstract class Animal
    {
        public string Name;
        public int Age;
        public string Habitat;
        public string FoodType;

        public Animal(string name, int age, string habitat, string foodType)
        {
            Name = name;
            Age = age;
            Habitat = habitat;
            FoodType = foodType;
        }

        public virtual string GetInfo()
        {
            return "Кличка: " + Name +
                   ", Возраст: " + Age +
                   ", Среда: " + Habitat +
                   ", Питание: " + FoodType;
        }
    }

    // Млекопитающее
    class Mammal : Animal
    {
        public bool HasFur;

        public Mammal(string name, int age, string habitat,
            string foodType, bool hasFur)
            : base(name, age, habitat, foodType)
        {
            HasFur = hasFur;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   ", Тип: Млекопитающее" +
                   ", Шерсть: " + (HasFur ? "есть" : "нет");
        }
    }

    // Птица
    class Bird : Animal
    {
        public double WingSpan;

        public Bird(string name, int age, string habitat,
            string foodType, double wingSpan)
            : base(name, age, habitat, foodType)
        {
            WingSpan = wingSpan;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   ", Тип: Птица" +
                   ", Размах крыльев: " + WingSpan;
        }
    }

    // Рыба
    class Fish : Animal
    {
        public string WaterType;

        public Fish(string name, int age, string habitat,
            string foodType, string waterType)
            : base(name, age, habitat, foodType)
        {
            WaterType = waterType;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   ", Тип: Рыба" +
                   ", Вода: " + WaterType;
        }
    }

    // Пресмыкающееся
    class Reptile : Animal
    {
        public bool IsVenomous;

        public Reptile(string name, int age, string habitat,
            string foodType, bool isVenomous)
            : base(name, age, habitat, foodType)
        {
            IsVenomous = isVenomous;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   ", Тип: Пресмыкающееся" +
                   ", Ядовитое: " + (IsVenomous ? "да" : "нет");
        }
    }

    // Земноводное
    class Amphibian : Animal
    {
        public string SkinMoisture;

        public Amphibian(string name, int age, string habitat,
            string foodType, string skinMoisture)
            : base(name, age, habitat, foodType)
        {
            SkinMoisture = skinMoisture;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   ", Тип: Земноводное" +
                   ", Влажность кожи: " + SkinMoisture;
        }
    }

    // Singleton
    class AnimalManager
    {
        private static AnimalManager instance;

        private List<Animal> animals;

        private AnimalManager()
        {
            animals = new List<Animal>();
        }

        public static AnimalManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AnimalManager();
                }

                return instance;
            }
        }

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        public void ShowAnimals()
        {
            if (animals.Count == 0)
            {
                Console.WriteLine("Животных нет.");
                return;
            }

            for (int i = 0; i < animals.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " +
                    animals[i].GetInfo());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AnimalManager manager = AnimalManager.Instance;

            // Начальные животные
            manager.AddAnimal(
                new Mammal("Барсик", 5,
                "лес", "хищник", true));

            manager.AddAnimal(
                new Bird("Кеша", 2,
                "джунгли", "всеядное", 1.3));

            manager.AddAnimal(
                new Fish("Немо", 1,
                "океан", "всеядное", "морская"));

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Показать животных");
                Console.WriteLine("2 - Добавить животное");
                Console.WriteLine("3 - Выход");

                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.ShowAnimals();
                        break;

                    case "2":
                        AddAnimal(manager);
                        break;

                    case "3":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }

        static void AddAnimal(AnimalManager manager)
        {
            Console.WriteLine();
            Console.WriteLine("Тип животного:");
            Console.WriteLine("1 - Млекопитающее");
            Console.WriteLine("2 - Птица");
            Console.WriteLine("3 - Рыба");
            Console.WriteLine("4 - Пресмыкающееся");
            Console.WriteLine("5 - Земноводное");

            Console.Write("Ваш выбор: ");
            string type = Console.ReadLine();

            Console.Write("Кличка: ");
            string name = Console.ReadLine();

            Console.Write("Возраст: ");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Среда обитания: ");
            string habitat = Console.ReadLine();

            Console.Write("Тип питания: ");
            string food = Console.ReadLine();

            switch (type)
            {
                case "1":

                    Console.Write("Есть шерсть (true/false): ");
                    bool fur =
                        Convert.ToBoolean(Console.ReadLine());

                    manager.AddAnimal(
                        new Mammal(name, age,
                        habitat, food, fur));

                    break;

                case "2":

                    Console.Write("Размах крыльев: ");
                    double wings =
                        Convert.ToDouble(Console.ReadLine());

                    manager.AddAnimal(
                        new Bird(name, age,
                        habitat, food, wings));

                    break;

                case "3":

                    Console.Write("Тип воды: ");
                    string water = Console.ReadLine();

                    manager.AddAnimal(
                        new Fish(name, age,
                        habitat, food, water));

                    break;

                case "4":

                    Console.Write("Ядовитое (true/false): ");
                    bool venom =
                        Convert.ToBoolean(Console.ReadLine());

                    manager.AddAnimal(
                        new Reptile(name, age,
                        habitat, food, venom));

                    break;

                case "5":

                    Console.Write("Влажность кожи: ");
                    string moisture = Console.ReadLine();

                    manager.AddAnimal(
                        new Amphibian(name, age,
                        habitat, food, moisture));

                    break;

                default:
                    Console.WriteLine("Неверный тип");
                    break;
            }

            Console.WriteLine("Животное добавлено.");
        }
    }
}