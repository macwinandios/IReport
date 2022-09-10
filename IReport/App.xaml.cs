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


        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
