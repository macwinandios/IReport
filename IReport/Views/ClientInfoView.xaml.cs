using IReport.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IReport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientInfoView : ContentPage
    {
        public ClientInfoView(ClientInfoViewModel clientInfoVM)
        {
            InitializeComponent();

            BindingContext = clientInfoVM;
        }
    }
}