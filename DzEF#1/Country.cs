using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzEF_1
{
    public class Country
    {
        public int Id { get; set; }
        public string? CountryName { get; set; }
        public string? CapitalName { get; set; }
        public int? InhabitantsNumber {  get; set; }

        public double? Square {  get; set; }

        public virtual Continent? Continent { get; set; }

    }
}
