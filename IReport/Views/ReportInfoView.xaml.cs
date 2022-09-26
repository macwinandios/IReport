using IReport.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IReport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportInfoView : ContentPage
    {
        public ReportInfoView(ReportInfoViewModel reportInfoVM)
        {
            InitializeComponent();

            BindingContext = reportInfoVM;
        }
    }
}