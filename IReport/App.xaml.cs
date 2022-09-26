using IReport.Views;
using Reports.Locator;
using Xamarin.Forms;

namespace IReport
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var loginView = ViewModelLocator.Resolve<LoginView>();
            MainPage = new NavigationPage(loginView);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
