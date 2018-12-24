using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;

namespace TransitionPanel.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public ViewModel Content01ViewModel { get; } = new Content01ViewModel();
        public ViewModel Content02ViewModel { get; } = new Content02ViewModel();
        public ViewModel Content03ViewModel { get; } = new Content03ViewModel();

        public IList<string> Titles { get; } = new List<string>()
        {
            new Content01ViewModel().Title,
            new Content02ViewModel().Title,
            new Content03ViewModel().Title,
        };

    }

}
