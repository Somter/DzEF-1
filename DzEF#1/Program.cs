using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.IdentityModel.Tokens;

namespace DzEF_1 
{
    class MainClass
    {
        static void Main()
        {
            try
            {
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                var config = builder.Build();
                string connectionString = config.GetConnectionString("DefaultConnection");
                 
                var optionsBuilder = new DbContextOptionsBuilder<CountryContext>();
                var options = optionsBuilder.UseSqlServer(connectionString).Options;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1. Отобразить всю информацию о странах");
                    Console.WriteLine("2. Отобразить название стран");
                    Console.WriteLine("3. Отобразить название столиц");
                    Console.WriteLine("4. Отобразить название всех европейских стран");
                    Console.WriteLine("5. Отобразить название стран с площадью больше заданного числа");
                    Console.WriteLine("6. Отобразить все страны, у которых в названии есть буквы a, е;");
                    Console.WriteLine("7. Отобразить все страны, у которых название начинается с буквы a;");
                    Console.WriteLine("8. Отобразить название стран, у которых площадь находится в указанном диапазоне;");
                    Console.WriteLine("9. Отобразить название стран, у которых количество жителей больше указанного числа.");
                    Console.WriteLine("0. Выход");
                    int result = int.Parse(Console.ReadLine()!);
                    switch (result)
                    {
                        case 1:
                            ShowAllCountries(options);  
                            break;
                        case 2:
                            ShowCountriesName(options);
                            break;
                        case 3:
                            ShowCountriesCapitalName(options);
                            break;
                        case 4:
                            ShowEuropeanCountries(options);
                            break;
                        case 5:
                            ShowCountriesByArea(options);
                            break;
                        case 6:
                            ShowCountriesWithAEInName(options);
                            break;
                        case 7:
                            ShowCountriesStartingWithA(options);    
                            break;
                        case 8:
                            ShowCountriesByAreaRange(options);
                            break;
                        case 9:
                            ShowCountriesByPopulation(options); 
                            break;
                        case 0:
                            return;
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       
        static void ShowAllCountries(DbContextOptions<CountryContext> options)
        {
            Console.Clear();
            using (var db = new CountryContext(options)) 
            {
                var query = db.Countries.Include(c => c.Continent).ToList();
                int inter = 0;
                foreach (var countr in query)
                    Console.WriteLine($"Страна {++inter}:\n Название - {countr.CountryName}; \n " +
                        $"Столица: {countr.CapitalName}; \n Количество жителей - {countr.InhabitantsNumber}; \n " +
                        $"Площадь - {countr.Square}; \n Часть света - {countr.Continent?.Name} \n"); 
   
            }
            Console.ReadKey();  
        }

        static void ShowCountriesName(DbContextOptions<CountryContext> options) 
        {
            Console.Clear();
            using (var db = new CountryContext(options)) 
            {
                var query = from gr in db.Countries
                            select gr;
                int inter = 0;
                foreach(var countr in query)
                {
                    Console.WriteLine($"Старана {inter++} \n Название: {countr.CountryName}");
                }
            }
            Console.ReadKey();
        }

        static void ShowCountriesCapitalName(DbContextOptions<CountryContext> options)
        {
            Console.Clear();
            using (var db = new CountryContext(options))
            {
                var query = from gr in db.Countries
                            select gr;
                int inter = 0;
                foreach (var countr in query)
                {
                    Console.WriteLine($"Старана {inter++} \n Столица: {countr.CapitalName}");
                }
            }
            Console.ReadKey();
        }

        static void ShowEuropeanCountries(DbContextOptions<CountryContext> options)
        {
            Console.Clear();
            using (var db = new CountryContext(options))
            {
                var europeanCountries = db.Countries.Where(c => c.Continent.Name == "Europe").ToList();
                int inter = 0;
                foreach (var country in europeanCountries)
                {
                    Console.WriteLine($"Страна {++inter}: {country.CountryName}");
                }
            }
            Console.ReadKey();
        }

        static void ShowCountriesByArea(DbContextOptions<CountryContext> options)
        {
            Console.Clear();
            Console.Write("Введите минимальную площадь: ");
            double areaThreshold = double.Parse(Console.ReadLine()!);

            using (var db = new CountryContext(options))
            {
                var largeCountries = db.Countries.Where(c => c.Square > areaThreshold).ToList();
                int inter = 0;
                foreach (var country in largeCountries)
                {
                    Console.WriteLine($"Страна {++inter}: {country.CountryName} - Площадь: {country.Square}");
                }
            }
            Console.ReadKey();
        }

        static void ShowCountriesWithAEInName(DbContextOptions<CountryContext> options)
        {
            Console.Clear();
            using (var db = new CountryContext(options))
            {
                var countriesWithAE = db.Countries
                    .Where(c => c.CountryName.ToLower().Contains("a") || c.CountryName.ToLower().Contains("e"))
                    .ToList();

                int inter = 0;
                foreach (var country in countriesWithAE)
                {
                    Console.WriteLine($"Страна {++inter}: {country.CountryName}");
                }
            }
            Console.ReadKey();
        }


        static void ShowCountriesStartingWithA(DbContextOptions<CountryContext> options)
        {
            Console.Clear();
            using (var db = new CountryContext(options))
            {
                var countriesStartingWithA = db.Countries
                    .Where(c => c.CountryName.ToLower().StartsWith("a"))
                    .ToList();

                int inter = 0;
                foreach (var country in countriesStartingWithA)
                {
                    Console.WriteLine($"Страна {++inter}: {country.CountryName}");
                }
            }
            Console.ReadKey();
        }


        static void ShowCountriesByAreaRange(DbContextOptions<CountryContext> options)
        {
            Console.Clear();
            Console.Write("Введите минимальную площадь: ");
            double minArea = double.Parse(Console.ReadLine()!);

            Console.Write("Введите максимальную площадь: ");
            double maxArea = double.Parse(Console.ReadLine()!);

            using (var db = new CountryContext(options))
            {
                var countriesInAreaRange = db.Countries.Where(c => c.Square >= minArea && c.Square <= maxArea).ToList();
                int inter = 0;
                foreach (var country in countriesInAreaRange)
                {
                    Console.WriteLine($"Страна {++inter}: {country.CountryName} - Площадь: {country.Square}");
                }
            }
            Console.ReadKey();
        }

        static void ShowCountriesByPopulation(DbContextOptions<CountryContext> options)
        {
            Console.Clear();
            Console.Write("Введите минимальное количество жителей: ");
            int populationThreshold = int.Parse(Console.ReadLine()!);

            using (var db = new CountryContext(options))
            {
                var countriesWithMorePopulation = db.Countries.Where(c => c.InhabitantsNumber > populationThreshold).ToList();
                int inter = 0;
                foreach (var country in countriesWithMorePopulation)
                {
                    Console.WriteLine($"Страна {++inter}: {country.CountryName} - Население: {country.InhabitantsNumber}");
                }
            }
            Console.ReadKey();
        }
    }
}
