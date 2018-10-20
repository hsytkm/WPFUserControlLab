using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace AddableList.Models
{
    public class Country
    {
        public static IReadOnlyDictionary<string, string> AliasMap =
            new ReadOnlyDictionary<string, string>(
                new Dictionary<string, string>()
                {
                    ["Japan"] = "日",
                    ["America"] = "米",
                    ["England"] = "英",
                    ["China"] = "中",
                    ["India"] = "印",
                });

        public static IReadOnlyCollection<Country> Candidates =
            AliasMap.Select(x => new Country(x.Key)).ToList();

        public string Name { get; }

        public string Alias { get; }

        //public IReadOnlyCollection<string> Cities;

        public Country(string name)
        {
            Name = name;

            if (AliasMap.TryGetValue(name, out string alias))
            {
                Alias = alias;
            }
            else
            {
                Alias = "？";
            }
        }

    }
}
