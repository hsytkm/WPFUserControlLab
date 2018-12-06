using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AddableList.Models
{
    public class Country
    {
        // name/alias
        public static IReadOnlyDictionary<string, string> AliasMap = new ReadOnlyDictionary<string, string>(
            new Dictionary<string, string>()
            {
                ["Japan"] = "日",
                ["America"] = "米",
                ["England"] = "英",
                ["China"] = "中",
                ["India"] = "印",
            });

        public string Name { get; }
        public string Alias { get; }

        public Country(string name)
        {
            Name = name;
            Alias = GetAlias(name);
        }

        private static string GetAlias(string name)
        {
            if (AliasMap.TryGetValue(name, out string alias)) return alias;
            else return "？";
        }
    }
}
