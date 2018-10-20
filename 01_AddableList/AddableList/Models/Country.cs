using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AddableList.Models
{
    public class Country
    {
        public static IReadOnlyCollection<Country> Candidates = new ReadOnlyCollection<Country>(
            new List<Country>()
            {
                new Country("Japan", "日"),
                new Country("America", "米"),
                new Country("England", "英"),
                new Country("China", "中"),
                new Country("India", "印"),
            });


        public string Name { get; }

        public string Alias { get; }

        //public IReadOnlyCollection<string> Cities;

        public Country(string name, string alias)
        {
            Name = name;
            Alias = alias;
        }

    }
}
