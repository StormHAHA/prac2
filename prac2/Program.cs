#nullable disable
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
            set => name = string.IsNullOrWhiteSpace(value) ? "Unknown" : value.Trim();
        }

        public int Age
        {
            get => age;
            set => age = (value >= 0 && value <= MaxAge) ? value : 0;
        }

        public double Height
        {
            get => height;
            set => height = (value > 30 && value < 300) ? value : 170.0;
        }

        public double Weight
        {
            get => weight;
            set => weight = (value > 2 && value < 500) ? value : 70.0;
        }

        public Person()
        {
            Name = "Неизвестно";
            Age = 0;
            Height = 170;
            Weight = 70;
            CreatedAt = DateTime.Now;
        }

        public Person(string name, int age, double height, double weight)
        {
            Name = name;
            Age = age;
            Height = height;
            Weight = weight;
            CreatedAt = DateTime.Now;
        }

        public virtual void SayHello() => Console.WriteLine($"Привет! Меня зовут {Name}, мне {Age} лет.");

        public virtual void GrowUp(int years = 1)
        {
            if (years < 0)
            {
                Console.WriteLine("Ошибка: возраст не может уменьшаться таким способом.");
                return;
            }

            if (Age + years <= MaxAge)
            {
                Age += years;
                Console.WriteLine($"{Name} стал(а) старше на {years} лет. Теперь {Age} лет.");
            }
            else
            {
                Console.WriteLine($"{Name} уже достиг(ла) максимального возраста!");
            }
        }

        public double BMI()
        {
            double heightMeters = Height / 100.0;
            return Math.Round(Weight / (heightMeters * heightMeters), 2);
        }

        public virtual void ShowInfo() => Console.WriteLine(ToString());

        public override string ToString() =>
            $"Имя: {Name}, Возраст: {Age}, Рост: {Height} см, Вес: {Weight} кг, ИМТ: {BMI()}";

        public static bool operator >(Person a, Person b) => a.Age > b.Age;
        public static bool operator <(Person a, Person b) => a.Age < b.Age;
        public static bool operator >=(Person a, Person b) => a.Age >= b.Age;
        public static bool operator <=(Person a, Person b) => a.Age <= b.Age;

        // --- Деконструктор ---
        public void Deconstruct(out string name, out int age, out double height, out double weight)
        {
            name = Name;
            age = Age;
            height = Height;
            weight = Weight;
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
            set => university = string.IsNullOrWhiteSpace(value) ? "Неизвестный университет" : value.Trim();
        }

        public int YearOfStudy
        {
            get => yearOfStudy;
            set => yearOfStudy = (value >= 1 && value <= 6) ? value : 1;
        }

        public Student() : base()
        {
            University = "Неизвестно";
            YearOfStudy = 1;
        }

        public Student(string name, int age, double height, double weight, string university, int year)
            : base(name, age, height, weight)
        {
            University = university;
            YearOfStudy = year;
        }

        public override void SayHello() =>
            Console.WriteLine($"Привет! Я студент {University}, меня зовут {Name}, я на {YearOfStudy} курсе.");

        public override string ToString() =>
            base.ToString() + $", Университет: {University}, Курс: {YearOfStudy}";

       
        public void Deconstruct(out string name, out int age, out double height, out double weight, out string university, out int year)
        {
            base.Deconstruct(out name, out age, out height, out weight);
            university = University;
            year = YearOfStudy;
        }
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
            DiplomaTopic = string.IsNullOrWhiteSpace(diplomaTopic) ? "Не указана" : diplomaTopic.Trim();
        }

        public void DefendDiploma() =>
            Console.WriteLine($"{Name} защитил(а) диплом на тему \"{DiplomaTopic}\" в {GraduationYear} году!");

        public override void SayHello() =>
            Console.WriteLine($"Здравствуйте, я выпускник {University}, выпуск {GraduationYear} года.");

        public override string ToString() =>
            base.ToString() + $", Год выпуска: {GraduationYear}, Тема диплома: \"{DiplomaTopic}\"";

        // --- Деконструктор ---
        public void Deconstruct(
            out string name,
            out int age,
            out double height,
            out double weight,
            out string university,
            out int year,
            out int graduationYear,
            out string diplomaTopic)
        {
            base.Deconstruct(out name, out age, out height, out weight, out university, out year);
            graduationYear = GraduationYear;
            diplomaTopic = DiplomaTopic;
        }
        
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
            Console.WriteLine("11. Сравнить человека и студента по возрасту");
            Console.WriteLine("12. Сравнить студента и выпускника по возрасту");
            Console.WriteLine("13. Сравнить человека и выпускника по возрасту");
            Console.WriteLine("14. Деконструировать объекты");
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
                    ComparePeople(person, student);
                    break;
                case "12":
                    ComparePeople(student, grad);
                    break;
                case "13":
                    ComparePeople(person, grad);
                    break;
                case "14":
                    ShowDeconstruction(person, student, grad);
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

    // --- Функция сравнения ---
    static void ComparePeople(Person a, Person b)
    {
        Console.WriteLine($"Сравнение {a.Name} ({a.Age} лет) и {b.Name} ({b.Age} лет):");
        Console.WriteLine($"{a.Name} > {b.Name}: {a > b}");
        Console.WriteLine($"{a.Name} < {b.Name}: {a < b}");
        Console.WriteLine($"{a.Name} >= {b.Name}: {a >= b}");
        Console.WriteLine($"{a.Name} <= {b.Name}: {a <= b}");
    }

    // --- Безопасный ввод ---
    static string SafeReadString(string prompt)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? "Неизвестно" : input.Trim();
    }

    static int SafeReadInt(string prompt, int min = 0, int max = 200)
    {
        int value;
        do
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"Ошибка: введите число от {min} до {max}.");
        } while (true);
    }

    static double SafeReadDouble(string prompt, double min = 1, double max = 500)
    {
        double value;
        do
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (double.TryParse(input, out value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"Ошибка: введите число от {min} до {max}.");
        } while (true);
    }

    static Person CreatePerson()
    {
        string name = SafeReadString("Имя: ");
        int age = SafeReadInt("Возраст: ", 0, Person.MaxAge);
        double height = SafeReadDouble("Рост (см): ", 50, 250);
        double weight = SafeReadDouble("Вес (кг): ", 2, 300);
        return new Person(name, age, height, weight);
    }

    static Student CreateStudent()
    {
        string name = SafeReadString("Имя: ");
        int age = SafeReadInt("Возраст: ", 0, Person.MaxAge);
        double height = SafeReadDouble("Рост (см): ", 50, 250);
        double weight = SafeReadDouble("Вес (кг): ", 2, 300);
        string university = SafeReadString("Университет: ");
        int year = SafeReadInt("Курс (1–6): ", 1, 6);
        return new Student(name, age, height, weight, university, year);
    }

    static Graduate CreateGraduate()
    {
        string name = SafeReadString("Имя: ");
        int age = SafeReadInt("Возраст: ", 0, Person.MaxAge);
        double height = SafeReadDouble("Рост (см): ", 50, 250);
        double weight = SafeReadDouble("Вес (кг): ", 2, 300);
        string university = SafeReadString("Университет: ");
        int year = SafeReadInt("Курс (1–6): ", 1, 6);
        int graduationYear = SafeReadInt("Год выпуска: ", 1900, DateTime.Now.Year + 1);
        string topic = SafeReadString("Тема диплома: ");
        return new Graduate(name, age, height, weight, university, year, graduationYear, topic);
    }

    static void ShowDeconstruction(Person p, Student s, Graduate g)
    {
        Console.WriteLine("\n--- Деконструкция объектов ---");

        var (pn, pa, ph, pw) = p;
        Console.WriteLine($"Person: {pn}, {pa} лет, {ph} см, {pw} кг");

        var (sn, sa, sh, sw, su, sy) = s;
        Console.WriteLine($"Student: {sn}, {sa} лет, {sh} см, {sw} кг, {su}, курс {sy}");

        var (gn, ga, gh, gw, gu, gy, gyear, gtopic) = g;
        Console.WriteLine($"Graduate: {gn}, {ga} лет, {gh} см, {gw} кг, {gu}, курс {gy}, выпуск {gyear}, тема: {gtopic}");
    }
}
}
