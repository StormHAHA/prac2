using System;

namespace PeopleApp
{
    // --- Интерфейс ---
    public interface IPersonActions
    {
        void SayHello();
        void GrowUp(int years);
        double BMI();
        void ShowInfo();
    }

    // --- Базовый класс ---
    public class Person : IPersonActions
    {
        private string name;
        private int age;
        private double height;
        private double weight;

        public readonly DateTime CreatedAt;
        public const int MaxAge = 120;

        public string Name
        {
            get => name;
            set => name = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        public int Age
        {
            get => age;
            set => age = (value >= 0 && value <= MaxAge) ? value : 0;
        }

        public double Height
        {
            get => height;
            set => height = (value > 0) ? value : 1.0;
        }

        public double Weight
        {
            get => weight;
            set => weight = (value > 0) ? value : 1.0;
        }

        public Person()
        {
            name = "Неизвестно";
            age = 0;
            height = 170;
            weight = 70;
            CreatedAt = DateTime.Now;
        }

        public Person(string name, int age, double height, double weight)
        {
            this.name = name;
            this.age = age;
            this.height = height;
            this.weight = weight;
            CreatedAt = DateTime.Now;
        }

        public virtual void SayHello() => Console.WriteLine($"Привет! Меня зовут {name}, мне {age} лет.");

        public virtual void GrowUp(int years = 1)
        {
            if (age + years <= MaxAge)
            {
                age += years;
                Console.WriteLine($"{name} стал(а) старше на {years} лет. Теперь {age} лет.");
            }
            else
            {
                Console.WriteLine($"{name} уже достиг(ла) максимального возраста!");
            }
        }

        public double BMI()
        {
            double heightMeters = height / 100.0;
            return Math.Round(weight / (heightMeters * heightMeters), 2);
        }

        public virtual void ShowInfo() => Console.WriteLine(ToString());

        public override string ToString() => $"Имя: {name}, Возраст: {age}, Рост: {height} см, Вес: {weight} кг, ИМТ: {BMI()}";

        public static Person operator +(Person a, Person b)
        {
            string newName = $"{a.name}-{b.name}";
            int newAge = Math.Min(a.age + b.age, MaxAge);
            double newHeight = (a.height + b.height) / 2;
            double newWeight = (a.weight + b.weight) / 2;
            return new Person(newName, newAge, newHeight, newWeight);
        }

        public static Person operator -(Person a, Person b)
        {
            int newAge = Math.Max(a.age - b.age, 0);
            return new Person(a.name, newAge, a.height, a.weight);
        }
    }

    // --- Student ---
    public class Student : Person
    {
        private string university;
        private int yearOfStudy;

        public string University
        {
            get => university;
            set => university = string.IsNullOrWhiteSpace(value) ? "Неизвестный университет" : value;
        }

        public int YearOfStudy
        {
            get => yearOfStudy;
            set => yearOfStudy = (value >= 1 && value <= 6) ? value : 1;
        }

        public Student() : base()
        {
            university = "Неизвестно";
            yearOfStudy = 1;
        }

        public Student(string name, int age, double height, double weight, string university, int year)
            : base(name, age, height, weight)
        {
            this.university = university;
            this.yearOfStudy = year;
        }

        public override void SayHello() => Console.WriteLine($"Привет! Я студент {University}, меня зовут {Name}, я на {YearOfStudy} курсе.");

        public override void ShowInfo() => Console.WriteLine(ToString());

        public override string ToString() => base.ToString() + $", Университет: {university}, Курс: {yearOfStudy}";
    }

    // --- Graduate ---
    public class Graduate : Student
    {
        public int GraduationYear { get; set; }
        public string DiplomaTopic { get; set; }

        public Graduate() : base()
        {
            GraduationYear = DateTime.Now.Year;
            DiplomaTopic = "Не указана";
        }

        public Graduate(string name, int age, double height, double weight,
                        string university, int yearOfStudy,
                        int graduationYear, string diplomaTopic)
            : base(name, age, height, weight, university, yearOfStudy)
        {
            GraduationYear = graduationYear;
            DiplomaTopic = diplomaTopic;
        }

        public void DefendDiploma() => Console.WriteLine($"{Name} защитил(а) диплом на тему \"{DiplomaTopic}\" в {GraduationYear} году!");

        public override void SayHello() => Console.WriteLine($"Здравствуйте, я выпускник {University}, выпуск {GraduationYear} года.");

        public override string ToString() => base.ToString() + $", Год выпуска: {GraduationYear}, Тема диплома: \"{DiplomaTopic}\"";
    }

    // --- Main ---
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Person person = new Person();
            Student student = new Student();
            Graduate grad = new Graduate();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1. Создать/изменить человека");
                Console.WriteLine("2. Создать/изменить студента");
                Console.WriteLine("3. Создать/изменить выпускника");
                Console.WriteLine("4. Показать информацию о человеке");
                Console.WriteLine("5. Показать информацию о студенте");
                Console.WriteLine("6. Показать информацию о выпускнике");
                Console.WriteLine("7. Сказать привет (человек)");
                Console.WriteLine("8. Сказать привет (студент)");
                Console.WriteLine("9. Сказать привет (выпускник)");
                Console.WriteLine("10. Защитить диплом");
                Console.WriteLine("11. Сложить человека и студента (оператор +)");
                Console.WriteLine("12. Вычесть возраст (оператор -)");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        person = CreatePerson();
                        break;
                    case "2":
                        student = CreateStudent();
                        break;
                    case "3":
                        grad = CreateGraduate();
                        break;
                    case "4":
                        person.ShowInfo();
                        break;
                    case "5":
                        student.ShowInfo();
                        break;
                    case "6":
                        grad.ShowInfo();
                        break;
                    case "7":
                        person.SayHello();
                        break;
                    case "8":
                        student.SayHello();
                        break;
                    case "9":
                        grad.SayHello();
                        break;
                    case "10":

                        grad.DefendDiploma();
                        break;
                    case "11":
                        Person combined = person + student;
                        Console.WriteLine("Результат сложения: " + combined);
                        break;
                    case "12":
                        Person subtracted = person - student;
                        Console.WriteLine("Результат вычитания: " + subtracted);
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }

            Console.WriteLine("Программа завершена.");
        }

        static Person CreatePerson()
        {
            Console.Write("Имя: ");
            string name = Console.ReadLine();
            Console.Write("Возраст: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Рост (см): ");
            double height = double.Parse(Console.ReadLine());
            Console.Write("Вес (кг): ");
            double weight = double.Parse(Console.ReadLine());
            return new Person(name, age, height, weight);
        }

        static Student CreateStudent()
        {
            Console.Write("Имя: ");
            string name = Console.ReadLine();
            Console.Write("Возраст: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Рост (см): ");
            double height = double.Parse(Console.ReadLine());
            Console.Write("Вес (кг): ");
            double weight = double.Parse(Console.ReadLine());
            Console.Write("Университет: ");
            string university = Console.ReadLine();
            Console.Write("Курс: ");
            int year = int.Parse(Console.ReadLine());
            return new Student(name, age, height, weight, university, year);
        }

        static Graduate CreateGraduate()
        {
            Console.Write("Имя: ");
            string name = Console.ReadLine();
            Console.Write("Возраст: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Рост (см): ");
            double height = double.Parse(Console.ReadLine());
            Console.Write("Вес (кг): ");
            double weight = double.Parse(Console.ReadLine());
            Console.Write("Университет: ");
            string university = Console.ReadLine();
            Console.Write("Курс: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Год выпуска: ");
            int graduationYear = int.Parse(Console.ReadLine());
            Console.Write("Тема диплома: ");
            string topic = Console.ReadLine();
            return new Graduate(name, age, height, weight, university, year, graduationYear, topic);
        }
    }
}
