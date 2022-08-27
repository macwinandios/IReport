using IReport.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IReport.Services
{
    public interface IReportInfoServices
    {
        void ReadSqlAssignedCasesMethod();

        void CheckConnectionMethod();

        List<ReportInfoModel> GetYesNoPicker();

        List<ReportInfoModel> GetMedicalDevicesUsed();

        List<ReportInfoModel> GetVehiclesPresent();

        List<ReportInfoModel> GetAttemptToConfirm();

        List<ReportInfoModel> GetSurveillanceDesc();

        List<ReportInfoModel> GetMaleOrFemale();

        List<ReportInfoModel> GetHeights();

        List<ReportInfoModel> GetWeights();

        List<ReportInfoModel> GetBuilds();

        List<ReportInfoModel> GetHairColors();

        List<ReportInfoModel> GetHairLengths();

        List<ReportInfoModel> GetSurveillanceEndReason();

        List<ReportInfoModel> GetTypeOfVideo();

        List<ReportInfoModel> GetSubjectVideoConfirmation();


    }
}
