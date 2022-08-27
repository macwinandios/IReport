using IReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IReport.Services
{
    public interface IReportInfoModel
    {
        List<ReportInfoModel> YesNoPicker { get; set; }
        List<ReportInfoModel> SubjectVideoConfirmations { get; set; }

        List<ReportInfoModel> Heights { get; set; }
        List<ReportInfoModel> SurveillanceEndReasons { get; set; }

        List<ReportInfoModel> Weights { get; set; }

        List<ReportInfoModel> Builds { get; set; }
        List<ReportInfoModel> HairColors { get; set; }
        List<ReportInfoModel> TypesOfVideoObtained { get; set; }
        List<ReportInfoModel> HairLengths { get; set; }

        List<ReportInfoModel> MalesOrFemales { get; set; }

        List<ReportInfoModel> SurveillancePositions { get; set; }


        List<ReportInfoModel> VehiclesPresentAtStartLocation { get; set; }


        List<ReportInfoModel> AttemptToConfirmSubjectWasHome { get; set; }


        List<ReportInfoModel> MedicalDevicesUsed { get; set; }

        int Key { get; set; }
        string Value { get; set; }
        bool ReadingSqlAssignedCases { get; set; }
        bool CreatingReport { get; set; }
        string ScheduledSurveillance { get; set; }
        string StartLocation { get; set; }
        string EndLocation { get; set; }
        string AddressWhereVideoWasObtained { get; set; }
        string SurveillanceSummary { get; set; }
        string VideoObtainedDetails { get; set; }
        DateTime SurveillanceDate { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        DateTime VideoObtainedTime { get; set; }
        TimeSpan TotalHoursOfSurveillance { get; set; }

    }
}
