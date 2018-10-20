using System.Collections.Generic;

namespace AddableList.Models
{
    public class Country
    {
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
