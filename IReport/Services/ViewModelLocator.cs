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
//            ReportInfoModel model = new ReportInfoModel();
//            _container.RegisterType<IReportInfoModel, ReportInfoModel>();
//            _container.RegisterType<IReportInfoServices, ReportInfoViewModel>();
//            _container.RegisterType<ISql, ReportInfoViewModel>();

//            _container.RegisterType<ILoginModel, LoginModel>();
//            _container.RegisterType<ILoginServices, LoginViewModel>();
//            _container.RegisterType<ISql, LoginViewModel>();
//            _container = new UnityContainer().AddExtension(new Diagnostic());



//        }

//        public ReportInfoViewModel ReportInfoViewModel
//        {
//           get { return _container.Resolve<ReportInfoViewModel>(); }
//        }

//        public LoginViewModel LoginViewModel
//        {
//            get { return _container.Resolve<LoginViewModel>(); }
//        }

//        public ReportInfoModel ReportInfoModel
//        {
//            get { return _container.Resolve<ReportInfoModel>(); }
//        }
//    }
//}
