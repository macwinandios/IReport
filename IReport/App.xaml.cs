using IReport.Models;
using IReport.Services;
using IReport.ViewModels;
using IReport.Views;
using System;
using Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IReport
{
    public partial class App : Application
    {
        //private static ViewModelLocator _locator;

        //public static ViewModelLocator Locator
        //{
        //    get { return _locator = _locator ?? new ViewModelLocator(); }
        //}
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginView());
        }

        protected override void OnStart()
        {
            //IUnityContainer unityContainer = new UnityContainer();

            //unityContainer = new UnityContainer().AddExtension(new Diagnostic());

            //unityContainer.RegisterType<ISql, ReportInfoViewModel>();
            //unityContainer.RegisterType<ISql, CaseInfoViewModel>();
            //unityContainer.RegisterType<ISql, ClientInfoViewModel>();
            //unityContainer.RegisterType<ISql, LoginViewModel>();

            //unityContainer.RegisterSingleton<IReportInfoModel, ReportInfoModel>();
            //unityContainer.RegisterSingleton<ReportInfoViewModel>();


            //ReportInfoViewModel report = unityContainer.Resolve<ReportInfoViewModel>();

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
