using AddableList.Models;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace AddableList.ViewModels
{
    public class CountryViewModel : BindableBase
    {
        public ObservableCollection<Country> CountryCandidates { get; }

        private Country _SelectedCountry;
        public Country SelectedCountry
        {
            get => _SelectedCountry;
            set => SetProperty(ref _SelectedCountry, value);
        }

        public CountryViewModel()
        {
            CountryCandidates = new ObservableCollection<Country>(
                Enum.GetValues(typeof(Country.Type)).Cast<Country.Type>().Select(x => new Country(x)));
        }
    }
}
