using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AddableList.Models
{
    public class Country
    {
        public enum Type
        {
            Japan, America, England, China, India
        };

        // Type/Alias
        public static IReadOnlyDictionary<Type, string> AliasMap = new ReadOnlyDictionary<Type, string>(
            new Dictionary<Type, string>()
            {
                [Type.Japan] = "日",
                [Type.America] = "米",
                [Type.England] = "英",
                [Type.China] = "中",
                [Type.India] = "印",
            });
        
        public Type Typ { get; }
        public string Name { get => Typ.ToString(); }
        public string Alias { get => GetAlias(Typ); }

        public Country(Type type)
        {
            Typ = type;
        }

        private static string GetAlias(Type key)
        {
            if (AliasMap.TryGetValue(key, out string value)) return value;
            else return "？";
        }
    }
}
