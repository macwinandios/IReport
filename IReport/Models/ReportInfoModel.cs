using IReport.Models.Base;
using IReport.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IReport.Models
{
    public class ReportInfoModel : ViewModelBase
    {
        public List<ReportInfoModel> YesNoPicker { get; set; }
        public List<ReportInfoModel> SubjectVideoConfirmations { get; set; }

        public List<ReportInfoModel> Heights { get; set; }
        public List<ReportInfoModel> SurveillanceEndReasons { get; set; }

        public List<ReportInfoModel> Weights { get; set; }

        public List<ReportInfoModel> Builds { get; set; }
        public List<ReportInfoModel> HairColors { get; set; }
        public List<ReportInfoModel> TypesOfVideoObtained { get; set; }
        public List<ReportInfoModel> HairLengths { get; set; }

        public List<ReportInfoModel> MalesOrFemales { get; set; }
        
        public List<ReportInfoModel> SurveillancePositions { get; set; }

        
        public List<ReportInfoModel> VehiclesPresentAtStartLocation { get; set; }

        
        public List<ReportInfoModel> AttemptToConfirmSubjectWasHome { get; set; }

        
        public List<ReportInfoModel> MedicalDevicesUsed { get; set; }
        public List<ReportInfoModel> MedicalDevicesUsedToBeUpdated { get; set; }

        int? _identifier;
        public int? Identifier
        {
            get => _identifier;
            set
            {
                _identifier = value;
                OnPropertyChanged(nameof(Identifier));
            }
        }

        public int Key { get; set; }
        public string Value { get; set; }

        

        bool _readingSqlAssignedCases;
        public bool ReadingSqlAssignedCases
        {
            get { return _readingSqlAssignedCases; }
            set
            {
                _readingSqlAssignedCases = value;
                OnPropertyChanged(nameof(ReadingSqlAssignedCases));
            }
        }

        bool _creatingReport;
        public bool CreatingReport
        {
            get { return _creatingReport; }
            set
            {
                _creatingReport = value;
                OnPropertyChanged(nameof(CreatingReport));
            }
        }

        bool _readingReport;
        public bool ReadingReport
        {
            get { return _readingReport; }
            set
            {
                _readingReport = value;
                OnPropertyChanged(nameof(ReadingReport));
            }
        }

        bool _updatingReport;
        public bool UpdatingReport
        {
            get { return _updatingReport; }
            set
            {
                _updatingReport = value;
                OnPropertyChanged(nameof(UpdatingReport));
            }
        }

        bool _deletingReport;
        public bool DeletingReport
        {
            get { return _deletingReport; }
            set
            {
                _deletingReport = value;
                OnPropertyChanged(nameof(DeletingReport));
            }
        }

        //string _scheduledSurveillance;
        //public string ScheduledSurveillance
        //{
        //    get { return _scheduledSurveillance; }
        //    set
        //    {
        //        _scheduledSurveillance = value;
        //        OnPropertyChanged(nameof(ScheduledSurveillance));
        //    }
        //}

        string _startLocation;
        public string StartLocation
        {
            get { return _startLocation; }
            set
            {
                _startLocation = value;
                OnPropertyChanged(nameof(StartLocation));
            }
        }

        string _startLocationToBeUpdated;
        public string StartLocationToBeUpdated
        {
            get { return _startLocationToBeUpdated; }
            set
            {
                _startLocationToBeUpdated = value;
                OnPropertyChanged(nameof(StartLocationToBeUpdated));
            }
        }

        string _endLocation;
        public string EndLocation
        {
            get { return _endLocation; }
            set
            {
                _endLocation = value;
                OnPropertyChanged(nameof(EndLocation));
            }
        }

        string _endLocationToBeUpdated;
        public string EndLocationToBeUpdated
        {
            get { return _endLocationToBeUpdated; }
            set
            {
                _endLocationToBeUpdated = value;
                OnPropertyChanged(nameof(EndLocationToBeUpdated));
            }
        }

        string _addressWhereVideoWasObtained;
        public string AddressWhereVideoWasObtained
        {
            get { return _addressWhereVideoWasObtained; }
            set
            {
                _addressWhereVideoWasObtained = value;
                OnPropertyChanged(nameof(AddressWhereVideoWasObtained));
            }
        }

        string _addressWhereVideoWasObtainedToBeUpdated;
        public string AddressWhereVideoWasObtainedToBeUpdated
        {
            get { return _addressWhereVideoWasObtainedToBeUpdated; }
            set
            {
                _addressWhereVideoWasObtainedToBeUpdated = value;
                OnPropertyChanged(nameof(AddressWhereVideoWasObtainedToBeUpdated));
            }
        }

        string _surveillanceSummary;
        public string SurveillanceSummary
        {
            get { return _surveillanceSummary; }
            set
            {
                _surveillanceSummary = value;
                OnPropertyChanged(nameof(SurveillanceSummary));
            }
        }

        string _surveillanceSummaryToBeUpdated;
        public string SurveillanceSummaryToBeUpdated
        {
            get { return _surveillanceSummaryToBeUpdated; }
            set
            {
                _surveillanceSummaryToBeUpdated = value;
                OnPropertyChanged(nameof(SurveillanceSummaryToBeUpdated));
            }
        }

        string _videoObtainedDetails;
        public string VideoObtainedDetails
        {
            get { return _videoObtainedDetails; }
            set
            {
                _videoObtainedDetails = value;
                OnPropertyChanged(nameof(VideoObtainedDetails));
            }
        }

        string _videoObtainedDetailsToBeUpdated;
        public string VideoObtainedDetailsToBeUpdated
        {
            get { return _videoObtainedDetailsToBeUpdated; }
            set
            {
                _videoObtainedDetailsToBeUpdated = value;
                OnPropertyChanged(nameof(VideoObtainedDetailsToBeUpdated));
            }
        }
        DateTime _surveillanceDate;
        public DateTime SurveillanceDate
        {
            get { return _surveillanceDate; }
            set
            {
                _surveillanceDate = value;
                OnPropertyChanged(nameof(SurveillanceDate));
            }
        }

        DateTime _surveillanceDateToBeUpdated;
        public DateTime SurveillanceDateToBeUpdated
        {
            get { return _surveillanceDateToBeUpdated; }
            set
            {
                _surveillanceDateToBeUpdated = value;
                OnPropertyChanged(nameof(SurveillanceDateToBeUpdated));
            }
        }
        //DateTime ringer = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;

        TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));

            }
        }
        TimeSpan _startTimeToBeUpdated;
        public TimeSpan StartTimeToBeUpdated
        {
            get { return _startTimeToBeUpdated; }
            set
            {
                _startTimeToBeUpdated = value;
                OnPropertyChanged(nameof(StartTimeToBeUpdated));

            }
        }

        TimeSpan _endTime;
        public TimeSpan EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                OnPropertyChanged(nameof(EndTime));
                OnPropertyChanged(nameof(TotalHoursOfSurveillance));

            }
        }

        TimeSpan _endTimeToBeUpdated;
        public TimeSpan EndTimeToBeUpdated
        {
            get { return _endTimeToBeUpdated; }
            set
            {
                _endTimeToBeUpdated = value;
                OnPropertyChanged(nameof(EndTimeToBeUpdated));

            }
        }

        TimeSpan _videoObtainedTime;
        public TimeSpan VideoObtainedTime
        {
            get { return _videoObtainedTime; }
            set
            {
                _videoObtainedTime = value;
                OnPropertyChanged(nameof(VideoObtainedTime));
            }
        }

        TimeSpan _videoObtainedTimeToBeUpdated;
        public TimeSpan VideoObtainedTimeToBeUpdated
        {
            get { return _videoObtainedTimeToBeUpdated; }
            set
            {
                _videoObtainedTimeToBeUpdated = value;
                OnPropertyChanged(nameof(VideoObtainedTimeToBeUpdated));
            }
        }
        public TimeSpan TotalHoursOfSurveillance
        {
            get { return EndTime - StartTime; }

        }

        public TimeSpan TotalHoursOfSurveillanceToBeUpdated
        {
            get { return EndTimeToBeUpdated - StartTimeToBeUpdated; }

        }

        ObservableCollection<ReportInfoModel> _reportInfoModelList;

        public ObservableCollection<ReportInfoModel> ReportInfoModelList
        {
            get { return _reportInfoModelList; }
            set
            {
                _reportInfoModelList = value;
                OnPropertyChanged(nameof(ReportInfoModelList));
            }
        }

        public string CaseId { get; internal set; }
        public string CaseIdToBeUpdated { get; internal set; }

        public string ClientName { get; internal set; }
        public string ClientNameToBeUpdated { get; internal set; }
        public string SubjectVideoConfirmation { get; internal set; }
        public string MedicalDeviceUsed { get; internal set; }
        public string VehiclePresentAtStartLocationDesc { get; internal set; }
        public string SelectedYesNoPicker { get; internal set; }
        public string AttemptToConfirmSubjectHomeType { get; internal set; }
        public string SurveillancePositionDesc { get; internal set; }
        public string MaleOrFemale { get; internal set; }
        public string Height { get; internal set; }
        public string Weight { get; internal set; }
        public string Build { get; internal set; }
        public string HairColor { get; internal set; }
        public string HairLength { get; internal set; }
        public string SurveillanceEndReason { get; internal set; }
        public string TypeOfVideoObtained { get; internal set; }

    }
}
