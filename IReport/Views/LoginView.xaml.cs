using IReport.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IReport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView(LoginViewModel loginVM)
        {
            InitializeComponent();

            BindingContext = loginVM;
        }
    }
}