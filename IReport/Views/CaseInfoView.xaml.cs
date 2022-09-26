using IReport.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IReport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CaseInfoView : ContentPage
    {
        public CaseInfoView(CaseInfoViewModel caseInfoVM)
        {
            InitializeComponent();

            BindingContext = caseInfoVM;
        }
    }
}