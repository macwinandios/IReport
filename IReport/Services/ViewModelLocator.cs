using IReport.Models;
using IReport.Models.Base;
using IReport.ViewModels;
using IReport.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Unity;
using Xamarin.Forms;

namespace Reports.Locator
{
    public static class ViewModelLocator
    {
        //
        static UnityContainer _container;
        static Dictionary<Type, Type> _dictionary;
        //dictionary to map our pages to our pagemodels

        //we dont want anyone creating an instance so ctor is not public

        static ViewModelLocator()
        {
            _container = new UnityContainer();
            _dictionary = new Dictionary<Type, Type>();

            //REGISTER VIEWS AND VIEWMODELS
            Register<CaseInfoViewModel, CaseInfoView>();
            Register<ClientInfoViewModel, ClientInfoView>();
            Register<LoginViewModel, LoginView>();
            Register<ReportInfoViewModel, ReportInfoView>();


            //REGISTER SERVICES (SERVICES ARE REGISTERED AS SINGLETONS BY DEFAULT)
            //_container.RegisterType<INavigationService, NavigationService>();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public static Page CreatePageFor(Type viewModelType)
        {
            var viewType = _dictionary[viewModelType];
            var view = (Page)Activator.CreateInstance(viewType);
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
            return view;
        }


        static void Register<TViewModel, TView>() where TViewModel : ViewModelBase where TView : Page
        {
            _dictionary.Add(typeof(TViewModel), typeof(TView));
            _container.RegisterType<TViewModel>();
        }

    }
}
