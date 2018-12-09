using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AddableList.Models
{
    public class City
    {
        public enum Type
        {
            Osaka, Tokyo, Nagoya,
            NewYork, LosAngeles, Chicago, Hawaii,
            London,
            Peking, Shanghai,
            Delhi, Mumbai,
        };

        // City.Type/Country.Type
        private static IReadOnlyDictionary<City.Type, Country.Type> BelongCountryMap = new ReadOnlyDictionary<City.Type, Country.Type>(
            new Dictionary<City.Type, Country.Type>()
            {
                [Type.Osaka] = Country.Type.Japan,
                [Type.Tokyo] = Country.Type.Japan,
                [Type.Nagoya] = Country.Type.Japan,

                [Type.NewYork] = Country.Type.America,
                [Type.LosAngeles] = Country.Type.America,
                [Type.Chicago] = Country.Type.America,
                [Type.Hawaii] = Country.Type.America,

                [Type.London] = Country.Type.England,

                [Type.Peking] = Country.Type.China,
                [Type.Shanghai] = Country.Type.China,

                [Type.Delhi] = Country.Type.India,
                [Type.Mumbai] = Country.Type.India,
            });

        // Country.Type, IList<City.Type>
        public static IReadOnlyDictionary<Country.Type, IReadOnlyList<City.Type>> CitiesMap =
             new ReadOnlyDictionary<Country.Type, IReadOnlyList<City.Type>>(
                 new Dictionary<Country.Type, IReadOnlyList<City.Type>>()
                 {
                     [Country.Type.Japan] = GetBelongCityList(Country.Type.Japan),
                     [Country.Type.America] = GetBelongCityList(Country.Type.America),
                     [Country.Type.England] = GetBelongCityList(Country.Type.England),
                     [Country.Type.China] = GetBelongCityList(Country.Type.China),
                     [Country.Type.India] = GetBelongCityList(Country.Type.India),
                 });

        public City() { }

        private static IReadOnlyList<City.Type> GetBelongCityList(Country.Type country)
        {
            return Enum.GetValues(typeof(City.Type)).Cast<City.Type>()
                .Where(x => BelongCountryMap[x] == country)
                .Select(x => x).ToList();
        }


    }

}
