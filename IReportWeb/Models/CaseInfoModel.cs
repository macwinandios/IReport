using IReport.Models.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IReport.Models
{
    //github push test 7:37pm
    public class CaseInfoModel : ViewModelBase
    {
        ObservableCollection<CaseInfoModel> _assignedCasesInfolList;

        public ObservableCollection<CaseInfoModel> AssignedCasesInfolList
        {
            get { return _assignedCasesInfolList; }
            set
            {
                _assignedCasesInfolList = value;
                OnPropertyChanged(nameof(AssignedCasesInfolList));

            }
        }

        ObservableCollection<CaseInfoModel> _caseInfoModelList;

        public ObservableCollection<CaseInfoModel> CaseInfoModelList
        {
            get { return _caseInfoModelList; }
            set
            {
                _caseInfoModelList = value;
                OnPropertyChanged(nameof(CaseInfoModelList));

            }
        }
        //the following List, Key, Value are here for the picker to work correctly
        public List<CaseInfoModel> SkinComplexionList { get; set; }
        public List<CaseInfoModel> LanguageList { get; set; }
        public List<CaseInfoModel> EthnicityList { get; set; }
        public List<CaseInfoModel> LevelOfAwarenessList { get; set; }

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

        public DateTime MinimumDateOfBirth { get; set; } = new DateTime(2004, 01, 01);
        public DateTime? MinimumDate { get; set; } = DateTime.Today;
        public DateTime MaximumDate { get; set; } = new DateTime(2023, 12, 31);


        DateTime _dateCaseInitialized;
        public DateTime DateCaseInitialized
        {
            get { return _dateCaseInitialized; }
            set
            {
                _dateCaseInitialized = value;
                OnPropertyChanged(nameof(DateCaseInitialized));
            }
        }

        DateTime _dateOfSurveillance;
        public DateTime DateOfSurveillance
        {
            get { return _dateOfSurveillance.Date; }
            set
            {
                _dateOfSurveillance = value;
                OnPropertyChanged(nameof(DateOfSurveillance));
            }
        }

        DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }
        string _caseDetails;
        public string CaseDetails
        {
            get { return _caseDetails; }
            set
            {
                _caseDetails = value;
                OnPropertyChanged(nameof(CaseDetails));
            }
        }
        string _licensePlateColor;
        public string LicensePlateColor
        {
            get { return _licensePlateColor; }
            set
            {
                _licensePlateColor = value;
                OnPropertyChanged(nameof(LicensePlateColor));
            }
        }
        string _licensePlateState;
        public string LicensePlateState
        {
            get { return _licensePlateState; }
            set
            {
                _licensePlateState = value;
                OnPropertyChanged(nameof(LicensePlateState));
            }
        }
        string _licensePlateNumber;
        public string LicensePlateNumber
        {
            get { return _licensePlateNumber; }
            set
            {
                _licensePlateNumber = value;
                OnPropertyChanged(nameof(LicensePlateNumber));
            }
        }
        string _vehicleColor;
        public string VehicleColor
        {
            get { return _vehicleColor; }
            set
            {
                _vehicleColor = value;
                OnPropertyChanged(nameof(VehicleColor));
            }
        }
        string _vehicleModel;
        public string VehicleModel
        {
            get { return _vehicleModel; }
            set
            {
                _vehicleModel = value;
                OnPropertyChanged(nameof(VehicleModel));
            }
        }
        string _vehicleMake;
        public string VehicleMake
        {
            get { return _vehicleMake; }
            set
            {
                _vehicleMake = value;
                OnPropertyChanged(nameof(VehicleMake));
            }
        }
        string _vehicleYear;
        public string VehicleYear
        {
            get { return _vehicleYear; }
            set
            {
                    _vehicleYear = value;
                    OnPropertyChanged(nameof(VehicleYear));
                
            }
        }
        string _frequentedPlaces;
        public string FrequentedPlaces
        {
            get { return _frequentedPlaces; }
            set
            {
                _frequentedPlaces = value;
                OnPropertyChanged(nameof(FrequentedPlaces));
            }
        }

        string _tattooDescription;
        public string TattooDescription
        {
            get { return _tattooDescription; }
            set
            {
                _tattooDescription = value;
                OnPropertyChanged(nameof(TattooDescription));
            } 
        }
        string _hairColor;
        public string HairColor
        {
            get { return _hairColor; }
            set
            {
                _hairColor = value;
                OnPropertyChanged(nameof(HairColor));
            }
        }
        string _eyeColor;
        public string EyeColor
        {
            get { return _eyeColor; }
            set
            {
                _eyeColor = value;
                OnPropertyChanged(nameof(EyeColor));
            }
        }


        string _weight;
        public string Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        string _height;
        public string Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        string _workDescription;
        public string WorkDescription
        {
            get { return _workDescription; }
            set
            {
                _workDescription = value;
                OnPropertyChanged(nameof(WorkDescription));
            }
        }
        string _workAddress;
        public string WorkAddress
        {
            get { return _workAddress; }
            set
            {
                _workAddress = value;
                OnPropertyChanged(nameof(WorkAddress));
            }
        }
        string _homeDescription;
        public string HomeDescription
        {
            get { return _homeDescription; }
            set
            {
                _homeDescription = value;
                OnPropertyChanged(nameof(HomeDescription));
            }
        }
        string _homeAddress;
        public string HomeAddress
        {
            get { return _homeAddress; }
            set
            {
                _homeAddress = value;
                OnPropertyChanged(nameof(HomeAddress));
            }
        }
        string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        string _middleName;
        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                _middleName = value;
                OnPropertyChanged(nameof(MiddleName));
            }
        }
        string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        string _caseId;
        public string CaseId
        {
            get { return _caseId; }
            set
            {
                _caseId = value;
                OnPropertyChanged(nameof(CaseId));
            }
        }

        string _assignedEmployeeUsername;
        public string AssignedEmployeeUsername
        {
            get { return _assignedEmployeeUsername; }
            set
            {
                _assignedEmployeeUsername = value;
                OnPropertyChanged(nameof(AssignedEmployeeUsername));
            }
        }
        string _assignedCaseId;
        public string AssignedCaseId
        {
            get { return _assignedCaseId; }
            set
            {
                _assignedCaseId = value;
                OnPropertyChanged(nameof(AssignedCaseId));
            }
        }
        DateTime _assignedDate;
        public DateTime AssignedDate
        {
            get { return _assignedDate; }
            set
            {
                _assignedDate = value;
                OnPropertyChanged(nameof(AssignedDate));
            }
        }
        DateTime _assignedTime;
        public DateTime AssignedTime
        {
            get { return _assignedTime; }
            set
            {
                _assignedTime = value;
                OnPropertyChanged(nameof(AssignedTime));
            }
        }

        bool _assigningACase;
        public bool AssigningACase
        {
            get { return _assigningACase; }
            set
            {
                _assigningACase = value;
                OnPropertyChanged(nameof(AssigningACase));
            }
        }

        bool _creatingCase;
        public bool CreatingCase
        {
            get { return _creatingCase; }
            set
            {
                _creatingCase = value;
                OnPropertyChanged(nameof(CreatingCase));
            }
        }

        bool _readingCase;
        public bool ReadingCase
        {
            get { return _readingCase; }
            set
            {
                _readingCase = value;
                OnPropertyChanged(nameof(ReadingCase));
            }
        }

        bool _updatingCase;
        public bool UpdatingCase
        {
            get { return _updatingCase; }
            set
            {
                _updatingCase = value;
                OnPropertyChanged(nameof(UpdatingCase));
            }
        }

        bool _deletingCase;
        public bool DeletingCase
        {
            get { return _deletingCase; }
            set
            {
                _deletingCase = value;
                OnPropertyChanged(nameof(DeletingCase));
            }
        }

        public string SelectedComplexion { get; internal set; }
        public string ClientName { get; internal set; }
        public string SelectedEthnicity { get; internal set; }
        public string SelectedLanguage { get; internal set; }
        public string SelectedAwareness { get; internal set; }
    }
}
