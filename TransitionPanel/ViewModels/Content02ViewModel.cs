using Livet;

namespace TransitionPanel.ViewModels
{
    class Content02ViewModel : ViewModel
    {
        private readonly static int Index = 2;

        public string Title { get => $"Title{Index}"; }

        public string Caption { get => $"Content{Index}"; }

    }
}
