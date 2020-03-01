using FFImageCleanCache.ViewModels;
using FFImageCleanCache.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FFImageCleanCache
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
        }
    }
}