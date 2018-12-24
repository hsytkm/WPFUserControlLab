using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;

namespace TransitionPanel.ViewModels
{
    class Content01ViewModel : ViewModel
    {
        private readonly static int Index = 1;

        public string Title { get => $"Title{Index}"; }

        public string Caption { get => $"Content{Index}"; }

    }
}
