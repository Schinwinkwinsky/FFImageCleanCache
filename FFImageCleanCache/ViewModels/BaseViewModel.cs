using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace FFImageCleanCache.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        protected IPageDialogService _pageDialogService;
        protected INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }
    }
}
