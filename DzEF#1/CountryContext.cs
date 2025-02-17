using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzEF_1
{
    public class CountryContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<Continent> Continents { get; set; }
        public CountryContext(DbContextOptions<CountryContext> options) : base (options)
        {
            if (Database.EnsureCreated())
            {
                Continent Europe = new Continent { Name = "Europe" };
                Continent Asia = new Continent { Name = "Asia" };
                Continent Africa = new Continent { Name = "Africa" };
                Continent NorthAmerica = new Continent { Name = "North America" };
                Continent SouthAmerica = new Continent { Name = "South America" };
                Continent Australia = new Continent { Name = "Australia" };
                Continent Antarctica = new Continent { Name = "Antarctica" };

                Continents?.Add(Europe);
                Continents?.Add(Asia);
                Continents?.Add(Africa);
                Continents?.Add(NorthAmerica);
                Continents?.Add(SouthAmerica);
                Continents?.Add(Australia);
                Continents?.Add(Antarctica);

                SaveChanges();

                Countries?.Add(new Country
                {
                    CountryName = "France",
                    CapitalName = "Paris",
                    InhabitantsNumber = 67000000,
                    Square = 643801,
                    Continent = Europe
                });

                Countries?.Add(new Country
                {
                    CountryName = "Japan",
                    CapitalName = "Tokyo",
                    InhabitantsNumber = 126800000,
                    Square = 377975,
                    Continent = Asia
                });

                Countries?.Add(new Country
                {
                    CountryName = "Nigeria",
                    CapitalName = "Abuja",
                    InhabitantsNumber = 206000000,
                    Square = 923768,
                    Continent = Africa
                });

                Countries?.Add(new Country
                {
                    CountryName = "Germany",
                    CapitalName = "Berlin",
                    InhabitantsNumber = 83000000,
                    Square = 357022,
                    Continent = Europe
                });

                Countries?.Add(new Country
                {
                    CountryName = "India",
                    CapitalName = "New Delhi",
                    InhabitantsNumber = 1393409038,
                    Square = 3287263,
                    Continent = Asia
                });

                Countries?.Add(new Country
                {
                    CountryName = "USA",
                    CapitalName = "Washington, D.C.",
                    InhabitantsNumber = 331893745,
                    Square = 9833517,
                    Continent = NorthAmerica
                });

                Countries?.Add(new Country
                {
                    CountryName = "Brazil",
                    CapitalName = "Brasília",
                    InhabitantsNumber = 213993437,
                    Square = 8515767,
                    Continent = SouthAmerica
                });

                Countries?.Add(new Country
                {
                    CountryName = "Australia",
                    CapitalName = "Canberra",
                    InhabitantsNumber = 25690000,
                    Square = 7692024,
                    Continent = Australia
                });

                Countries?.Add(new Country
                {
                    CountryName = "Argentina",
                    CapitalName = "Buenos Aires",
                    InhabitantsNumber = 45195777,
                    Square = 2780400,
                    Continent = SouthAmerica
                });

                Countries?.Add(new Country
                {
                    CountryName = "South Africa",
                    CapitalName = "Pretoria",
                    InhabitantsNumber = 59308690,
                    Square = 1219090,
                    Continent = Africa
                });

                SaveChanges(); 
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
    }
}
