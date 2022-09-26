using Reports.Locator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IReport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabAccessor : TabbedPage
    {
        public TabAccessor()
        {
            InitializeComponent();

            var reportInfoView = ViewModelLocator.Resolve<ReportInfoView>();
            reportInfoView.Title = "WRITE REPORT";

            var caseInfoView = ViewModelLocator.Resolve<CaseInfoView>();
            caseInfoView.Title = "CASE INFO";

            var clientInfoView = ViewModelLocator.Resolve<ClientInfoView>();
            clientInfoView.Title = "CLIENT INFO";

            // Add pages to tabbed page
            this.Children.Add(reportInfoView);
            this.Children.Add(caseInfoView);
            this.Children.Add(clientInfoView);
        }
    }
}