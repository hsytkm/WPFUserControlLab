using AddableList.Models;
using Reactive.Bindings;
using System;
using System.Collections.Generic;

namespace AddableList.ViewModels
{
    public class CountryViewModel
    {
        public IReadOnlyCollection<Country> CountryCandidates { get; } = Country.Candidates;

        public CountryViewModel()
        {

        }
    }
}
