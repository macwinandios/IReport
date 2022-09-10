using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IReport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CaseInfoView : ContentPage
    {
        public CaseInfoView()
        {
            InitializeComponent();
            ///BindingContext = App.Locator.CaseInfoViewModel;

        }
    }
}