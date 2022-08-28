using IReport.Models.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IReport.Models
{
    public class ClientInfoModel : ViewModelBase
    {
        public List<ClientInfoModel> YesNoIDontKnowPicker { get; set; }
        public int Key { get; set; }
        public string Value { get; set; }

        ObservableCollection<ClientInfoModel> _clientInfoModelList;

        public ObservableCollection<ClientInfoModel> ClientInfoModelList
        {
            get { return _clientInfoModelList; }
            set
            {
                _clientInfoModelList = value;
                OnPropertyChanged(nameof(ClientInfoModelList));
            }
        }

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




        bool _hasActiveCase;
        public bool HasActiveCase
        {
            get { return _hasActiveCase; }
            set
            {
                _hasActiveCase = value;
                OnPropertyChanged(HasActiveCase.ToString());
            }
        }

        private bool _creatingNewClient;
        public bool CreatingNewClient
        {
            get => _creatingNewClient;
            set
            {
                _creatingNewClient = value;
                OnPropertyChanged(nameof(CreatingNewClient));
            }
        }

        private bool _readingClient;
        public bool ReadingClient
        {
            get => _readingClient;
            set
            {
                _readingClient = value;
                OnPropertyChanged(nameof(ReadingClient));
            }
        }

        private bool _updatingClient;
        public bool UpdatingClient
        {
            get => _updatingClient;
            set
            {
                _updatingClient = value;
                OnPropertyChanged(nameof(UpdatingClient));
            }
        }

        private bool _deletingClient;
        public bool DeletingClient
        {
            get => _deletingClient;
            set
            {
                _deletingClient = value;
                OnPropertyChanged(nameof(DeletingClient));
            }
        }
        string _clientName;
        public string ClientName
        {
            get => _clientName;
            set
            {
                _clientName = value;
                OnPropertyChanged(nameof(ClientName));
            }
        }

        string _clientEmail;
        public string ClientEmail
        {
            get { return _clientEmail; }
            set
            {
                _clientEmail = value;
                OnPropertyChanged(nameof(ClientEmail));
            }
        }

        string _clientPhoneNumber;
        public string ClientPhoneNumber
        {
            get { return _clientPhoneNumber; }
            set
            {
                _clientPhoneNumber = value;
                OnPropertyChanged(nameof(ClientPhoneNumber));
            }
        }

        string _clientAddress;
        public string ClientAddress
        {
            get { return _clientAddress; }
            set
            {
                _clientAddress = value;
                OnPropertyChanged(nameof(ClientAddress));
            }
        }

        string _clientMainContactName;
        public string ClientMainContactName
        {
            get { return _clientMainContactName; }
            set
            {
                _clientMainContactName = value;
                OnPropertyChanged(nameof(ClientMainContactName));
            }
        }

        string _clientMainContactPhoneNumber;
        public string ClientMainContactPhoneNumber
        {
            get { return _clientMainContactPhoneNumber; }
            set
            {
                _clientMainContactPhoneNumber = value;
                OnPropertyChanged(nameof(ClientMainContactPhoneNumber));
            }
        }

        DateTime _clientSince;
        public DateTime ClientSince
        {
            get { return _clientSince; }
            set
            {
                _clientSince = value;
                OnPropertyChanged(ClientSince.ToString());
            }
        }
        // adding ? checks if its null
        //now it doesnt print 0 when you run the app
        decimal? _pricePerHour;
        public decimal? PricePerHour
        {
            get { return _pricePerHour; }
            set
            {
                _pricePerHour = value;
                OnPropertyChanged(PricePerHour.ToString());


            }
        }
        decimal? _numberOfHoursBilledToDate;
        public decimal? NumberOfHoursBilledToDate
        {
            get { return _numberOfHoursBilledToDate; }
            set
            {
                _numberOfHoursBilledToDate = value;
                OnPropertyChanged(NumberOfHoursBilledToDate.ToString());
                OnPropertyChanged(TotalAmountBilledToDate.ToString());
            }
        }
        public decimal? TotalAmountBilledToDate
        {
            get { return PricePerHour * NumberOfHoursBilledToDate; }
        } 
         
        decimal? _totalPaidToDate;
        public decimal? TotalPaidToDate
        {
            get { return _totalPaidToDate; }
            set
            {
                _totalPaidToDate = value;
                OnPropertyChanged(TotalPaidToDate.ToString());
                OnPropertyChanged(BalanceDue.ToString());
            }
        } 
        public decimal? BalanceDue
        {
            get { return TotalAmountBilledToDate - TotalPaidToDate; }
        }

        decimal _totalHoursBilledToDate;
        public decimal TotalHoursBilledToDate
        {
            get { return _totalHoursBilledToDate;}
            set
            {
                _totalHoursBilledToDate = value;
                OnPropertyChanged(TotalHoursBilledToDate.ToString());
            }
        }

        
    }
}
