﻿using Prism.Mvvm;
using Reactive.Bindings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AddableList.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title { get; } = "AddableList";

        private ObservableCollection<CountryViewModel> CountriesSource { get; } = new ObservableCollection<CountryViewModel>();
        public ReadOnlyReactiveCollection<CountryViewModel> Countries { get; }

        public AsyncReactiveCommand AddCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand DelCommand { get; } = new AsyncReactiveCommand();

        public MainWindowViewModel()
        {
            Countries = CountriesSource.ToReadOnlyReactiveCollection();

            AddCommand.Subscribe(async _ =>
            {
                await Task.Run(() =>
                {
                    CountriesSource.Add(new CountryViewModel());
                });
            });

            DelCommand.Subscribe(async _ =>
            {
                await Task.Run(() =>
                {
                    if (CountriesSource.Count > 0)
                    {
                        CountriesSource.RemoveAt(CountriesSource.Count - 1);
                    }
                });
            });

        }
    }
}
