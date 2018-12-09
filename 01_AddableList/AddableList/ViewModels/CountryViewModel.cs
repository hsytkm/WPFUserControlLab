using AddableList.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace AddableList.ViewModels
{
    public class CountryViewModel : BindableBase
    {
        public ObservableCollection<Country> CountryCandidates { get; }

        private ReactiveProperty<Country> _SelectedCountry;
        public ReactiveProperty<Country> SelectedCountry
        {
            get => _SelectedCountry;
            set => SetProperty(ref _SelectedCountry, value);
        }

        public ObservableCollection<City.Type> CityCandidates { get; }

        private ReactiveProperty<City.Type> _SelectedCityType;
        public ReactiveProperty<City.Type> SelectedCityType
        {
            get => _SelectedCityType;
            set => SetProperty(ref _SelectedCityType, value);
        }

        public CountryViewModel()
        {
            CountryCandidates = new ObservableCollection<Country>(
                Enum.GetValues(typeof(Country.Type)).Cast<Country.Type>().Select(x => new Country(x)));

            SelectedCountry = new ReactiveProperty<Country>(initialValue: CountryCandidates[0], mode: ReactivePropertyMode.None);
            SelectedCountry.Subscribe(x =>
            {
                CityCandidates.Clear();
                CityCandidates.AddRange(City.CitiesMap[x.Typ]);
                SelectedCityType.Value = CityCandidates[0];
            });

            CityCandidates = new ObservableCollection<City.Type>(City.CitiesMap[SelectedCountry.Value.Typ]);

            SelectedCityType = new ReactiveProperty<City.Type>(initialValue: CityCandidates[0], mode:ReactivePropertyMode.None);
            SelectedCityType.Subscribe(x => SelectedCountry.Value.CityType = x);

        }
    }
}
