
using IReport.Services;
using IReport.ViewModels;
using FluentAssertions;
using Moq;
using Xunit;

namespace IReport.Test
{
    public class IReportTest
    {

        [Fact]

        public void AssertAllSqlMethods()
        {

            //Arrange
            Mock<CaseInfoViewModel> mockCaseInfoViewModel = new();
            Mock<ClientInfoViewModel> mockClientInfoViewModel = new Mock<ClientInfoViewModel>();
            Mock<ReportInfoViewModel> mockReportInfoViewModel = new Mock<ReportInfoViewModel>();

            var _caseInfoViewModelInstance = new CaseInfoViewModel(mockCaseInfoViewModel.Object);
            var _clientInfoViewModelInstance = new ClientInfoViewModel(mockClientInfoViewModel.Object);
            var _reportInfoViewModelInstance = new ReportInfoViewModel(mockReportInfoViewModel.Object);

            //Act



            //Assert
            _caseInfoViewModelInstance.Should().NotBeNull();
            _clientInfoViewModelInstance.Should().NotBeNull();
            _reportInfoViewModelInstance.Should().NotBeNull();


        }
    }
}