//using IReport.Models;
//using IReport.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Unity;

//namespace IReport.Services
//{
//    public class ViewModelLocator
//    {
//        private readonly IUnityContainer _container;

//        public ViewModelLocator()
//        {
//            _container = new UnityContainer();
//            ReportInfoViewModel viewModel = new ReportInfoViewModel();
//            CaseInfoViewModel caseviewModel = new CaseInfoViewModel();
//            ClientInfoViewModel clientviewModel = new ClientInfoViewModel();


//            _container.RegisterType<ISql, ReportInfoViewModel>();
//            _container.RegisterType<ISql, CaseInfoViewModel>();
//            _container.RegisterType<ISql, ClientInfoViewModel>();

//            //checks for error - did not work properly
//            _container = new UnityContainer().AddExtension(new Diagnostic());

//        }

//        public ReportInfoViewModel ReportInfoViewModel
//        {
//            get { return _container.Resolve<ReportInfoViewModel>(); }
//        }

//        public CaseInfoViewModel CaseInfoViewModel
//        {
//            get { return _container.Resolve<CaseInfoViewModel>(); }
//        }

//        public ClientInfoViewModel ClientInfoViewModel
//        {
//            get { return _container.Resolve<ClientInfoViewModel>(); }
//        }
//    }
//}
