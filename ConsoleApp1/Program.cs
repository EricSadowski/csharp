using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}, City: {City}";
    }
}

class InvalidNameException : Exception
{
    public InvalidNameException() { }

    public InvalidNameException(string name)
        : base(String.Format("Invalid Person Name: {0}", name))
    {

    }
}

class InvalidCityException : Exception
{
    public InvalidCityException() { }

    public InvalidCityException(string city)
        : base(String.Format("Invalid Person City: {0}", city))
    {

    }
}

class InvalidAgeException : Exception
{
    public InvalidAgeException() { }

    public InvalidAgeException(int age)
        : base(String.Format("Invalid Person Age: {0}", age))
    {

    }
}

class Program
{
    static List<Person> people = new List<Person>();

    private static void Main(string[] args)
    {
        try
        {
            ReadAllPeopleFromFile();
            CallMenu();

        }
        catch (InvalidNameException ex)
        {
            Console.WriteLine(ex.Message);
        }

        while (true)
        {
            Console.WriteLine("Enter a command:");
            string input = Console.ReadLine();


            // Execute the command
            ExecuteCommand(input);
        }

    }


    static void ReadAllPeopleFromFile()
    {

        //Create object of FileInfo for specified path            
        FileInfo fi = new FileInfo(@"C:\Users\ericc\OneDrive\Documents\GitHub\csharp\ConsoleApp1\people.txt");

        //Open file for Read\Write
        FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

        //Create object of StreamReader by passing FileStream object on which it needs to operates on
        StreamReader sr = new StreamReader(fs);


        while (!sr.EndOfStream)
        {
            string thisLine = sr.ReadLine();
            string[] info = thisLine.Split(new string[] { ";" }, StringSplitOptions.None);
            AddPersonInfo(info[0], int.Parse(info[1]), info[2]);
        }

        //Close StreamReader object after operation
        sr.Close();
        fs.Close();


    }

    private static void AddPersonInfo(string name, int age, string city)
    {
        Person newPerson = new Person
        {
            Name = name,
            Age = age,
            City = city
        };

        ValidatePerson(newPerson);

        people.Add(newPerson);
    }

    private static void CallMenu()
    {
        Console.WriteLine("1. Add person info");
        Console.WriteLine("2. List persons info");
        Console.WriteLine("3. Find a person by name");
        Console.WriteLine("4. Find all persons younger than age");
        Console.WriteLine("0. Exit");
    }

    private static void ListAllPersonsInfo()
    {
        foreach (var person in people)
        {
            Console.WriteLine(person.ToString());
        }
    }

    private static void FindPersonByName(string search)
    {
        foreach (var person in people)
        {
            if (person.Name.Contains(search))
            {
                Console.WriteLine(person.Name);
            }
        }
    }

    static void FindPersonYoungerThan(int age)
    {
        foreach (var person in people)
        {
            if (person.Age < age)
            {
                Console.WriteLine(person.Name + ", Age: " + person.Age);
            }
        }
    }

    private static void ValidatePerson(Person person)
    {
        Regex forString = new Regex("^[^;]{2,100}$");
        //[a-zA-z]
        Regex forNumber = new Regex(@"^([1-9][0-9]?|1[0-4][0-9]|150)$");

        if (!forString.IsMatch(person.Name))
            throw new InvalidNameException(person.Name);
        if (!forString.IsMatch(person.City))
            throw new InvalidCityException(person.City);
        if (!forNumber.IsMatch(person.Age.ToString()))
            throw new InvalidAgeException(person.Age);
    }


    static void ExecuteCommand(string command)
    {
        switch (command)
        {
            case "0":
                Save();
                Environment.Exit(0);
                break;
            case "1":
                Console.WriteLine("Enter name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter age");
                int age = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter city");
                string city = Console.ReadLine();
                AddPersonInfo(name, age, city);
                CallMenu();
                break;
            case "2":
                ListAllPersonsInfo();
                CallMenu();
                break;
            case "3":
                Console.WriteLine("Enter name to search");
                string input = Console.ReadLine();
                FindPersonByName(input);
                CallMenu();
                break;
            case "4":
                Console.WriteLine("Enter maximum age");
                int input2 = int.Parse(Console.ReadLine());
                FindPersonYoungerThan(input2);
                CallMenu();
                break;
            default:
                Console.WriteLine("Invalid command");
                CallMenu();
                break;
        }
    }

    static void Save()
    {
        using (StreamWriter writer = new StreamWriter(@"C:\Users\ericc\OneDrive\Documents\GitHub\csharp\ConsoleApp1\people.txt"))
        {
            foreach (var person in people)
            {
                writer.WriteLine($"{person.Name};{person.Age};{person.City}");
            }
        }
    }
}
