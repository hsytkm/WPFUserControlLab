using AddableList.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AddableList.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "WPFUserControlLab AddableList";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<Country> CountriesSource { get; } = new ObservableCollection<Country>();
        public ReadOnlyReactiveCollection<Country> Countries { get; }

        public AsyncReactiveCommand AddCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand DelCommand { get; } = new AsyncReactiveCommand();

        public MainWindowViewModel()
        {
            Countries = CountriesSource.ToReadOnlyReactiveCollection();

            AddCommand.Subscribe(async _ =>
            {
                await Task.Run(() =>
                {
                    CountriesSource.Add(new Country("Japan", "日"));
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
