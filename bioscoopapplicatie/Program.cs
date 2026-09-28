using System;
using System.Collections.Generic;
using System.Threading;

class Film
{
    public string Naam;
    public int LeeftijdsGrens;
    public double KaartPrijs;

    public Film(string naam, int leeftijdsGrens, double kaartPrijs)
    {
        Naam = naam;
        LeeftijdsGrens = leeftijdsGrens;
        KaartPrijs = kaartPrijs;
    }
}

class Program
{
    
    const double StudentenKorting = 2.50;

    static void Main(string[] args)
    {
        Console.Clear();
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        string name = GetName();
        int age = GetAge();

        List<Film> films = new List<Film>
        {
            new Film("Pressure", 16, 25),
            new Film("Avengers Endgame", 12, 25),
            new Film("Paw Patrol", 4, 25),
            new Film("Toy Story", 8, 25),
            new Film("Cars 2", 6, 25)
        };

        Film selectedFilm = ChooseMovie(films, age);
        bool student = GetStudentStatus();
        int tickets = GetTicketAmount();
        double totalPrice = CalculateTotalPrice(
            selectedFilm,
            tickets,
            student
        );

        Console.WriteLine("\nLoading...");
        Thread.Sleep(1000);
        Console.WriteLine("Done!");
        Thread.Sleep(100);
        Console.Clear();

        string kortingRegel = student
            ? $"Studentenkorting: € {StudentenKorting:F2} per kaartje{Environment.NewLine}"
            : "";

        Console.WriteLine($"""
        ============================
               Bioscoop Luis
        ============================
        Naam: {name}
        Leeftijd: {age}
        Film: {selectedFilm.Naam}
        Leeftijdsgrens: {selectedFilm.LeeftijdsGrens}+
        Aantal kaartjes: {tickets}
        Student: {(student ? "Ja" : "Nee")}
        Prijs per kaartje: € {selectedFilm.KaartPrijs:F2}
        {kortingRegel}Totaalprijs: € {totalPrice:F2}
        ============================
        """);
    }
    
    static string GetName()
    {
        Console.Write("What is your name: ");
        return Console.ReadLine();
    }
    
    static int GetAge()
    {
        int age;
        Console.Write("What is your age: ");

        while (!int.TryParse(Console.ReadLine(), out age))
        {
            Console.Write("Enter a valid age: ");
        }

        return age;
    }
    
    static Film ChooseMovie(List<Film> films, int age)
    {
        Console.WriteLine("\nAvailable movies:");

        foreach (Film film in films)
        {
            if (age >= film.LeeftijdsGrens)
            {
                Console.WriteLine(
                    $"{films.IndexOf(film) + 1}. {film.Naam} - " +
                    $"{film.LeeftijdsGrens}+ - €{film.KaartPrijs:F2}"
                );
            }
        }

        int choice;

        while (true)
        {
            Console.Write("What movie would you like to watch: ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                if (choice >= 1 && choice <= films.Count)
                {
                    Film selectedFilm = films[choice - 1];

                    if (age >= selectedFilm.LeeftijdsGrens)
                    {
                        return selectedFilm;
                    }

                    Console.WriteLine($"You are too young for {selectedFilm.Naam}.");
                }
            }

            Console.WriteLine("Enter a valid movie number.");
        }
    }
    
    static bool GetStudentStatus()
    {
        while (true)
        {
            Console.Write("Are you a student (yes/no): ");
            string answer = Console.ReadLine().ToLower();

            switch (answer)
            {
                case "yes":
                    Console.WriteLine("You are a student.");
                    return true;
                case "no":
                    Console.WriteLine("You are not a student.");
                    return false;
                default:
                    Console.WriteLine("Please enter yes or no.");
                    break;
            }
        }
    }
    
    static int GetTicketAmount()
    {
        int tickets;

        while (true)
        {
            Console.Write("How many tickets do you want (1-8): ");

            if (int.TryParse(Console.ReadLine(), out tickets))
            {
                if (tickets >= 1 && tickets <= 8)
                {
                    return tickets;
                }
            }

            Console.WriteLine("Enter a valid number between 1 and 8.");
        }
    }
    
    static double CalculateTotalPrice(
        Film film,
        int tickets,
        bool student)
    {
        if (student)
        {
            return CalculatePrice(
                film.KaartPrijs,
                tickets,
                StudentenKorting
            );
        }
        return CalculatePrice(
            film.KaartPrijs,
            tickets
        );
    }

    static double CalculatePrice(double price, int tickets)
    {
        return price * tickets;
    }
    
    static double CalculatePrice(
        double price,
        int tickets,
        double discount)
    {
        return (price - discount) * tickets;
    }
}