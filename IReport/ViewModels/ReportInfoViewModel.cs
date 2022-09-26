using IReport.Models;
using IReport.Models.Base;
using IReport.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace IReport.ViewModels
{
    /// <summary>
    /// VIEWMODEL FOR REPORTINFOVIEW

    /// ADD A LINE DIRECTORY
    //CREATE METHOD SQL SERVER IS ON LINE:
    //READ METHOD FOR SQL IS ON LINE:
    // UPDATE METHOD FOR SQL IS ON LINE:
    // DELETE METHOD FOR SQL IS ON LINE:
    /// </summary>
    /// 
    public class ReportInfoViewModel : ViewModelBase, ISql
    {
        public ReportInfoViewModel(ISql isql)
        {
            _isql = isql;
        }

        public ReportInfoViewModel(ReportInfoModel reportInfoModel, CaseInfoModel caseInfoModel, ClientInfoModel clientInfoModel, LoginModel loginModel, SqlModel sqlModel)
        {
            ReportInfoModel = reportInfoModel;
            CaseInfoModel = caseInfoModel;
            ClientInfoModel = clientInfoModel;
            LoginModel = loginModel;
            SqlModel = sqlModel;

            //NECESSARY TO READ FROM SQL AND POPULATE THE PICKERS WITH DATA
            ObservableCollection<CaseInfoModel> cases = new ObservableCollection<CaseInfoModel>();
            CaseInfoModel.CaseInfoModelList = cases;

            ObservableCollection<CaseInfoModel> assignedCases = new ObservableCollection<CaseInfoModel>();
            CaseInfoModel.AssignedCasesInfolList = assignedCases;

            ObservableCollection<ClientInfoModel> clients = new ObservableCollection<ClientInfoModel>();
            ClientInfoModel.ClientInfoModelList = clients;

            ObservableCollection<ReportInfoModel> reports = new ObservableCollection<ReportInfoModel>();
            ReportInfoModel.ReportInfoModelList = reports;



            //LINQ TO POPULATE PICKERS WITH KEY-VALUE PAIRS
            ReportInfoModel.YesNoPicker = GetYesNoPicker().OrderBy(t => t.Value).ToList();
            ReportInfoModel.MedicalDevicesUsed = GetMedicalDevicesUsed().OrderBy(t => t.Value).ToList();
            ReportInfoModel.VehiclesPresentAtStartLocation = GetVehiclesPresent().OrderBy(t => t.Value).ToList();
            ReportInfoModel.AttemptToConfirmSubjectWasHome = GetAttemptToConfirm().OrderBy(t => t.Value).ToList();
            ReportInfoModel.SurveillancePositions = GetSurveillanceDesc().OrderBy(t => t.Value).ToList();
            ReportInfoModel.MalesOrFemales = GetMaleOrFemale().OrderBy(t => t.Value).ToList();
            ReportInfoModel.Heights = GetHeights().OrderBy(t => t.Value).ToList();
            ReportInfoModel.Weights = GetWeights().OrderBy(t => t.Value).ToList();
            ReportInfoModel.Builds = GetBuilds().OrderBy(t => t.Value).ToList();
            ReportInfoModel.HairColors = GetHairColors().OrderBy(t => t.Value).ToList();
            ReportInfoModel.HairLengths = GetHairLengths().OrderBy(t => t.Value).ToList();
            ReportInfoModel.SurveillanceEndReasons = GetSurveillanceEndReason().OrderBy(t => t.Value).ToList();
            ReportInfoModel.TypesOfVideoObtained = GetTypeOfVideo().OrderBy(t => t.Value).ToList();
            ReportInfoModel.SubjectVideoConfirmations = GetSubjectVideoConfirmation().OrderBy(t => t.Value).ToList();
        }

        ISql _isql;

        //AFTER WEEKS OF TRYING
        //THIS ALLOWED ME TO BIND MY CASE ID TO THE PICKER'S ITEMDISPLAYBINDING
        //THOUGHT OF IT MYSELF
        BindingBase _caseId;
        BindingBase _clientName;

        //PRIVATE MEMBERS FOR SELECTED CASE AND CLIENT PICKERS
        CaseInfoModel _selectedCase;
        ClientInfoModel _selectedClient;


        //PRIVATE MEMBERS OF EACH MODEL
        ReportInfoModel _reportInfoModel;
        CaseInfoModel _caseInfoModel;
        ClientInfoModel _clientInfoModel;
        LoginModel _loginModel;
        SqlModel _sqlModel;



        //PRIVATE MEMBERS FOR PICKERS FROM SQL. NECESSARY TO READ FROM SQL USING A PICKER
        //the public properties for these private members are at the end of this class to avoid clutter
        ReportInfoModel _selectedYesNoPicker;
        ReportInfoModel _medicalDeviceUsed;
        ReportInfoModel _vehiclePresentAtStartLocationDesc;
        ReportInfoModel _attemptToConfirmSubjectHomeType;
        ReportInfoModel _surveillancePositionDesc;
        ReportInfoModel _maleOrFemale;
        ReportInfoModel _height;
        ReportInfoModel _weight;
        ReportInfoModel _build;
        ReportInfoModel _hairColor;
        ReportInfoModel _hairLength;
        ReportInfoModel _surveillanceEndReason;
        ReportInfoModel _typeOfVideoObtained;
        ReportInfoModel _subjectVideoConfirmation;


        //PRIVATE MEMBERS FOR EACH COMMAND
        //the public properties for these private members are at the end of this class to avoid clutter
        private ICommand _readSqlAssignedCasesCommand;
        private ICommand _readSqlCommand;
        private ICommand _createSqlCommand;
        private ICommand _getClientAndCasePickersCommand;
        private ICommand _updateSqlCommand;
        private ICommand _deleteSqlCommand;
        private ICommand _updateCommand;
        private ICommand _deleteCommand;
        private ICommand _readAssignedCasesIsVisibleCommand;


        //THE BINDINGBASE PROPERTY TO POPULATE THE CASEID PICKER
        //The BindingBase class is abstract so it cannot be instanciated and derives from a XAMARIN.FORMS
        public BindingBase CaseId
        {
            get => _caseId;
            set
            {
                _caseId = value;
                OnPropertyChanged(nameof(CaseId));
            }
        }

        //SetProperty method being used (this method has become very popular)
        //BindingBase property to populate the ClientName picker
        public BindingBase ClientName { get => _clientName; set => SetProperty(ref _clientName, value); }



        //property to populate picker. Once chosen, we are able to insert to SQL the SelectedCase via this picker
        public CaseInfoModel SelectedCase
        {
            get => _selectedCase;
            set
            {
                if (_selectedCase != value)
                {
                    _selectedCase = value;
                }
                OnPropertyChanged(nameof(SelectedCase));
            }
        }

        //property to populate picker. Once chosen, we are able to insert to SQL the SelectedClient via this picker
        public ClientInfoModel SelectedClient
        {
            get => _selectedClient;
            set
            {
                if (_selectedClient != value)
                {
                    _selectedClient = value;
                }
                OnPropertyChanged(nameof(SelectedClient));
            }
        }



        //PUBLIC PROPERTIES BEGIN
        public LoginModel LoginModel
        {
            get => _loginModel;
            set
            {
                _loginModel = value;
                OnPropertyChanged(nameof(LoginModel));
            }
        }


        public ReportInfoModel ReportInfoModel
        {
            get => _reportInfoModel;
            set
            {
                _reportInfoModel = value;
                OnPropertyChanged(nameof(ReportInfoModel));

            }
        }


        public CaseInfoModel CaseInfoModel
        {
            get => _caseInfoModel;
            set
            {
                _caseInfoModel = value;
                OnPropertyChanged(nameof(CaseInfoModel));

            }
        }

        public ClientInfoModel ClientInfoModel
        {
            get => _clientInfoModel;
            set
            {
                _clientInfoModel = value;
                OnPropertyChanged(nameof(ClientInfoModel));
            }
        }


        public SqlModel SqlModel
        {
            get => _sqlModel;
            set
            {
                _sqlModel = value;
            }
        }

        //METHOD MAKES ASSIGNED CASES VISIBLE
        public void ReadAssignedCasesIsVisibleMethod()
        {
            ReportInfoModel.ReadingSqlAssignedCases = true;
        }

        //READ FROM SQL ASSIGNEDCASESTABLE
        public async void ReadSqlAssignedCasesMethod()
        {

            try
            {
                SqlModel.SqlConnection.Open();

                SqlCommand assignedCasesCommand = new SqlCommand(SqlModel.AssignedCasesQuery, SqlModel.SqlConnection);
                SqlDataReader assignedCasesReader = assignedCasesCommand.ExecuteReader();
                if (LoginModel.EmployeeUsername == CaseInfoModel.AssignedEmployeeUsername)
                {
                    ReportInfoModel.ReadingSqlAssignedCases = true;

                    while (assignedCasesReader.Read())
                    {

                        CaseInfoModel.AssignedCasesInfolList.Add(new CaseInfoModel
                        {
                            AssignedEmployeeUsername = assignedCasesReader["EmployeeName"].ToString(),
                            AssignedCaseId = assignedCasesReader["CaseId"].ToString(),
                            AssignedDate = Convert.ToDateTime(assignedCasesReader["Date"]),
                            AssignedTime = Convert.ToDateTime(assignedCasesReader["Time"])
                        });

                    }

                }


                else
                {
                    await Application.Current.MainPage.DisplayAlert("YOU'RE OFF", "YOU HAVE NO ASSIGNED CASES", "OK");

                }



                assignedCasesReader.Close();
                SqlModel.SqlConnection.Close();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");
            }
            finally
            {
                SqlModel.SqlConnection.Close();


            }
        }

        //METHOD READS FROM CASEINFOTABLE AND CLIENTINFOTABLE TO GET CLIENTS AND CASES FROM EACH TABLE
        public async void GetClientAndCasePickersMethod()
        {

            try
            {
                ReportInfoModel.CreatingReport = true;
                ReportInfoModel.ReadingReport = false;

                SqlModel.SqlConnection.Open();

                SqlCommand caseCommand = new SqlCommand(SqlModel.CaseQuery, SqlModel.SqlConnection);

                SqlDataReader caseReader = caseCommand.ExecuteReader();

                while (caseReader.Read())
                {

                    CaseInfoModel.CaseInfoModelList.Insert(0, new CaseInfoModel
                    {
                        CaseId = caseReader["CaseId"].ToString()
                    });


                }
                caseReader.Close();


                SqlCommand clientCommand = new SqlCommand(SqlModel.ClientQuery, SqlModel.SqlConnection);

                SqlDataReader clientReader = clientCommand.ExecuteReader();
                while (clientReader.Read())
                {
                    ClientInfoModel.ClientInfoModelList.Insert(0, new ClientInfoModel
                    {
                        ClientName = clientReader["ClientName"].ToString()
                    });
                }
                clientReader.Close();
                SqlModel.SqlConnection.Close();


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");

            }
            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }
        //CREATE TO SQL REPORTINFOTABLE
        public async void CreateSqlMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();

                if (CaseInfoModel.CaseId != string.Empty)
                {
                    using (SqlCommand sqlInsertCaseCommand = new SqlCommand(SqlModel.ReportInsertQuery, SqlModel.SqlConnection))
                    {
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("CaseId", SelectedCase.CaseId));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("ClientName", SelectedClient.ClientName));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("SurveillanceDate", ReportInfoModel.SurveillanceDate.ToShortDateString()));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("StartTime", ReportInfoModel.StartTime.ToString()));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("EndTime", ReportInfoModel.EndTime.ToString()));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("TotalHoursOfSurveillance", ReportInfoModel.TotalHoursOfSurveillance));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("StartLocation", ReportInfoModel.StartLocation));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("EndLocation", ReportInfoModel.EndLocation));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("SubjectVideoObtained", SubjectVideoConfirmation.Value));

                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("MedicalDevicesUsed", MedicalDeviceUsed.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("AddressWhereVideoWasObtained", ReportInfoModel.AddressWhereVideoWasObtained));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("VehiclesPresentAtStartLocation", SelectedYesNoPicker.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("NumberOfVehiclesAtStartLocation", VehiclePresentAtStartLocationDesc.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("StartLocationIsSubjectsResidence", SelectedYesNoPicker.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("AttemptedToConfirmSubjectWasHome", AttemptToConfirmSubjectHomeType.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("SurveillancePosition", SurveillancePositionDesc.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("SubjectSex", MaleOrFemale.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("ApproximateHeight", Height.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("ApproximateWeight", Weight.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("ApproximateBuild", Build.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("HairColor", HairColor.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("HairLength", HairLength.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("WhyWasSurveillanceEnded", SurveillanceEndReason.Value));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("SurveillanceSummary", ReportInfoModel.SurveillanceSummary));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("VideoObtainedDetails", ReportInfoModel.VideoObtainedDetails));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("VideoObtainedTime", ReportInfoModel.VideoObtainedTime.ToString()));
                        sqlInsertCaseCommand.Parameters.Add(new SqlParameter("TypeOfVideoObtained", TypeOfVideoObtained.Value));

                        sqlInsertCaseCommand.ExecuteNonQuery();
                        await Application.Current.MainPage.DisplayAlert("SUCCESSFULLY ADDED", "CLICK OK TO PROCEED", "OK");
                    }

                }
                SqlModel.SqlConnection.Close();
            }

            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");

            }
            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }
        //READ FROM SQL REPORTINFOTABLE
        public async void ReadSqlMethod()
        {
            ReportInfoModel.CreatingReport = false;
            ReportInfoModel.DeletingReport = false;
            ReportInfoModel.UpdatingReport = false;

            ReportInfoModel.ReadingReport = true;
            try
            {
                SqlModel.SqlConnection.Open();

                SqlCommand reportCommand = new SqlCommand(SqlModel.ReportQuery, SqlModel.SqlConnection);

                SqlDataReader reportReader = reportCommand.ExecuteReader();

                while (reportReader.Read())
                {

                    ReportInfoModel.ReportInfoModelList.Insert(0, new ReportInfoModel
                    {
                        CaseId = reportReader["CaseId"].ToString(),
                        ClientName = reportReader["ClientName"].ToString(),
                        SurveillanceDate = Convert.ToDateTime(reportReader["SurveillanceDate"]),
                        StartLocation = reportReader["StartLocation"].ToString(),
                        EndLocation = reportReader["EndLocation"].ToString(),
                        AddressWhereVideoWasObtained = reportReader["AddressWhereVideoWasObtained"].ToString(),
                        SubjectVideoConfirmation = reportReader["SubjectVideoObtained"].ToString(),
                        MedicalDeviceUsed = reportReader["MedicalDevicesUsed"].ToString(),
                        VehiclePresentAtStartLocationDesc = reportReader["VehiclesPresentAtStartLocation"].ToString(),
                        SelectedYesNoPicker = reportReader["StartLocationIsSubjectsResidence"].ToString(),
                        AttemptToConfirmSubjectHomeType = reportReader["AttemptedToConfirmSubjectWasHome"].ToString(),
                        SurveillancePositionDesc = reportReader["SurveillancePosition"].ToString(),
                        MaleOrFemale = reportReader["SubjectSex"].ToString(),
                        Height = reportReader["ApproximateHeight"].ToString(),
                        Weight = reportReader["ApproximateWeight"].ToString(),
                        Build = reportReader["ApproximateBuild"].ToString(),
                        HairColor = reportReader["HairColor"].ToString(),
                        HairLength = reportReader["HairLength"].ToString(),
                        SurveillanceEndReason = reportReader["WhyWasSurveillanceEnded"].ToString(),
                        TypeOfVideoObtained = reportReader["TypeOfVideoObtained"].ToString(),
                        VideoObtainedDetails = reportReader["VideoObtainedDetails"].ToString(),
                        SurveillanceSummary = reportReader["SurveillanceSummary"].ToString(),
                        Identifier = Convert.ToInt32(reportReader["Identifier"])

                    });

                }
                reportReader.Close();
                SqlModel.SqlConnection.Close();


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");

            }
            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }
        //makes ONLY the UpdateAClient entries visible
        public void UpdateMethod()
        {
            ReportInfoModel.UpdatingReport = true;
            ReportInfoModel.CreatingReport = false;
            ReportInfoModel.ReadingReport = false;
            ReportInfoModel.DeletingReport = false;

        }

        //UPDATE SQL REPORTINFOTABLE
        public async void UpdateSqlMethod()
        {
            try
            {
                ReportInfoModel.CreatingReport = false;
                ReportInfoModel.DeletingReport = false;
                ReportInfoModel.ReadingReport = false;
                SqlModel.SqlConnection.Open();


                if (ReportInfoModel.EndLocationToBeUpdated != string.Empty)
                {
                    string queryString = $"UPDATE dbo.ReportInfoTable SET EndLocation = '{ReportInfoModel.EndLocationToBeUpdated}'  WHERE Identifier ='{ReportInfoModel.Identifier}'";

                    using (SqlCommand command = new SqlCommand(queryString, SqlModel.SqlConnection))
                    {
                        command.ExecuteNonQuery();

                    }

                }
                if (ReportInfoModel.StartLocationToBeUpdated != string.Empty)
                {
                    string queryString = $"UPDATE dbo.ReportInfoTable SET StartLocation = '{ReportInfoModel.StartLocationToBeUpdated}'  WHERE Identifier ='{ReportInfoModel.Identifier}'";

                    using (SqlCommand command = new SqlCommand(queryString, SqlModel.SqlConnection))
                    {
                        command.ExecuteNonQuery();

                    }

                }


                if (ReportInfoModel.AddressWhereVideoWasObtainedToBeUpdated != string.Empty)
                {
                    string queryString = $"UPDATE dbo.ReportInfoTable SET AddressWhereVideoWasObtained = '{ReportInfoModel.AddressWhereVideoWasObtainedToBeUpdated}'  WHERE Identifier ='{ReportInfoModel.Identifier}'";

                    using (SqlCommand command = new SqlCommand(queryString, SqlModel.SqlConnection))
                    {
                        command.ExecuteNonQuery();

                    }

                }
                await Application.Current.MainPage.DisplayAlert("SUCCESSFULLY UPDATED", "CLICK OK TO PROCEED", "OK");
            }

            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");

            }
            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }

        //makes ONLY the DeleteAClient entries visible

        public void DeleteMethod()
        {
            ReportInfoModel.DeletingReport = true;
            ReportInfoModel.UpdatingReport = false;
            ReportInfoModel.CreatingReport = false;
            ReportInfoModel.ReadingReport = false;
        }

        //DELETE FROM SQL REPORTINFOTABLE
        public async void DeleteSqlMethod()
        {
            try
            {
                ReportInfoModel.UpdatingReport = false;
                ReportInfoModel.CreatingReport = false;
                ReportInfoModel.ReadingReport = false;
                SqlModel.SqlConnection.Open();


                using (SqlCommand deleteCommand = new SqlCommand($"Delete FROM dbo.ReportInfoTable WHERE Identifier = {ReportInfoModel.Identifier}", SqlModel.SqlConnection))
                {
                    deleteCommand.ExecuteNonQuery();
                }
                SqlModel.SqlConnection.Close();
                await Application.Current.MainPage.DisplayAlert("SUCCESSFULLY DELETED", "CLICK OK TO PROCEED", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");
            }
            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }

        ////public properties and lists
        ////each pair of property/list is for the pickers to get these values using LINQ

        public ReportInfoModel SelectedYesNoPicker
        {
            get => _selectedYesNoPicker;
            set
            {
                _selectedYesNoPicker = value;
                OnPropertyChanged(nameof(SelectedYesNoPicker));
            }
        }

        public List<ReportInfoModel> GetYesNoPicker()
        {
            var yesOrno = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "YES"},
                new ReportInfoModel(){Key = 2, Value = "NO"},
                new ReportInfoModel(){Key = 3, Value = "Not requested by client."}

            };
            return yesOrno;
        }

        public ReportInfoModel MedicalDeviceUsed
        {
            get => _medicalDeviceUsed;
            set
            {
                _medicalDeviceUsed = value;
                OnPropertyChanged(nameof(MedicalDeviceUsed));
            }
        }

        public List<ReportInfoModel> GetMedicalDevicesUsed()
        {
            var devicesUsed = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "The subject used NO visible medical devices."},
                new ReportInfoModel(){Key = 2, Value = "The subject walked with the assistance of a cane."},
                new ReportInfoModel(){Key = 3, Value = "The subject wore a sling."},
                new ReportInfoModel(){Key = 4, Value = "The subject wore medicated shoes."},
                new ReportInfoModel(){Key = 5, Value = "The subject was in a wheelchair."},
                new ReportInfoModel(){Key = 6, Value = "The subject wore a neck brace."},
                new ReportInfoModel(){Key = 7, Value = "The subject walked with the assistance of two crutches."},
                new ReportInfoModel(){Key = 8, Value = "Not requested by client."}

            };
            return devicesUsed;
        }

        public ReportInfoModel VehiclePresentAtStartLocationDesc
        {
            get => _vehiclePresentAtStartLocationDesc;
            set
            {
                _vehiclePresentAtStartLocationDesc = value;
                OnPropertyChanged(nameof(VehiclePresentAtStartLocationDesc));
            }
        }

        public List<ReportInfoModel> GetVehiclesPresent()
        {
            var vehiclesPresent = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "There was one vehicle."},
                new ReportInfoModel(){Key = 2, Value = "There were two vehicles."},
                new ReportInfoModel(){Key = 3, Value = "There were no vehicles."},
                new ReportInfoModel(){Key = 4, Value = "There were three vehicles."},
                new ReportInfoModel(){Key = 5, Value = "There were more than three vehicles."},
                new ReportInfoModel(){Key = 6, Value = "Not requested by client."}

            };
            return vehiclesPresent;
        }

        public ReportInfoModel AttemptToConfirmSubjectHomeType
        {
            get => _attemptToConfirmSubjectHomeType;
            set
            {
                _attemptToConfirmSubjectHomeType = value;
                OnPropertyChanged(nameof(AttemptToConfirmSubjectHomeType));
            }
        }

        public List<ReportInfoModel> GetAttemptToConfirm()
        {
            var attemptToConfirm = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "Knocked on the door, but no one answered."},
                new ReportInfoModel(){Key = 2, Value = "Yes I Tried, but I couldn't confirm the subject was home."},
                new ReportInfoModel(){Key = 3, Value = "Yes, I confirmed the subject was home."},
                new ReportInfoModel(){Key = 4, Value = "Not needed, subject was active."},
                new ReportInfoModel(){Key = 5, Value = "Client requested that we do not confirm."},
                new ReportInfoModel(){Key = 6, Value = "Not requested by client."}
            };
            return attemptToConfirm;
        }

        public ReportInfoModel SurveillancePositionDesc
        {
            get => _surveillancePositionDesc;
            set
            {
                _surveillancePositionDesc = value;
                OnPropertyChanged(nameof(SurveillancePositionDesc));
            }
        }

        public List<ReportInfoModel> GetSurveillanceDesc()
        {
            var attemptToConfirm = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "A stationary surveillance position was established with a direct view of the residence."},
                new ReportInfoModel(){Key = 2, Value = "A stationary surveillance position was established witha partial view of the residence."},
                new ReportInfoModel(){Key = 3, Value = "A stationary surveillance position was established at the main egress route."},
                new ReportInfoModel(){Key = 4, Value = "There was no surveillance position available.  Multiple drive-bys of the residence were conducted throughout the day."},
                new ReportInfoModel(){Key = 5, Value = "Client requested that we do not confirm."},
                new ReportInfoModel(){Key = 6, Value = "Not requested by client."}
            };
            return attemptToConfirm;
        }

        public ReportInfoModel MaleOrFemale
        {
            get => _maleOrFemale;
            set
            {
                _maleOrFemale = value;
                OnPropertyChanged(nameof(MaleOrFemale));
            }
        }

        public List<ReportInfoModel> GetMaleOrFemale()
        {
            var yesOrno = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "MALE"},
                new ReportInfoModel(){Key = 2, Value = "FEMALE"},
                new ReportInfoModel(){Key = 3, Value = "Not requested by client."}

            };
            return yesOrno;
        }

        public ReportInfoModel Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        public List<ReportInfoModel> GetHeights()
        {
            var heights = new List<ReportInfoModel>()
            {

                new ReportInfoModel(){Key = 1, Value = "5'4"},
                new ReportInfoModel(){Key = 2, Value = "5'5"},
                new ReportInfoModel(){Key = 3, Value = "5'7"},
                new ReportInfoModel(){Key = 4, Value = "5'8"},
                new ReportInfoModel(){Key = 5, Value = "5'9"},
                new ReportInfoModel(){Key = 6, Value = "5'10"},
                new ReportInfoModel(){Key = 7, Value = "5'11"},
                new ReportInfoModel(){Key = 8, Value = "6'0"},
                new ReportInfoModel(){Key = 9, Value = "6'1"},
                new ReportInfoModel(){Key = 10, Value = "6'2"},
                new ReportInfoModel(){Key = 11, Value = "Not requested by client."}

            };
            return heights;
        }

        public ReportInfoModel Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        public List<ReportInfoModel> GetWeights()
        {
            var weights = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "100LBS"},
                new ReportInfoModel(){Key = 2, Value = "150LBS"},
                new ReportInfoModel(){Key = 3, Value = "180LBS"},
                new ReportInfoModel(){Key = 4, Value = "200LBS"},
                new ReportInfoModel(){Key = 5, Value = "220LBS"},
                new ReportInfoModel(){Key = 6, Value = "250LBS"},
                new ReportInfoModel(){Key = 7, Value = "300LBS"},
                new ReportInfoModel(){Key = 8, Value = "300LBS++"},
                new ReportInfoModel(){Key = 9, Value = "Not requested by client."}

            };
            return weights;
        }

        public ReportInfoModel Build
        {
            get => _build;
            set
            {
                _build = value;
                OnPropertyChanged(nameof(Build));
            }
        }

        public List<ReportInfoModel> GetBuilds()
        {
            var weights = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "AN UNKNOWN BUILD"},
                new ReportInfoModel(){Key = 2, Value = "A THIN BUILD"},
                new ReportInfoModel(){Key = 3, Value = "AN AVERAGE BUILD"},
                new ReportInfoModel(){Key = 4, Value = "A FIT BUILD"},
                new ReportInfoModel(){Key = 5, Value = "A MUSCULAR BUILD"},
                new ReportInfoModel(){Key = 6, Value = "AN OVERWEIGHT BUILD"},
                new ReportInfoModel(){Key = 7, Value = "AN OBESE BUILD"},
                new ReportInfoModel(){Key = 8, Value = "Not requested by client."}

            };
            return weights;
        }

        public ReportInfoModel HairColor
        {
            get => _hairColor;
            set
            {
                _hairColor = value;
                OnPropertyChanged(nameof(HairColor));
            }
        }

        public List<ReportInfoModel> GetHairColors()
        {
            var hairColors = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "GRAY"},
                new ReportInfoModel(){Key = 2, Value = "WHITE"},
                new ReportInfoModel(){Key = 3, Value = "BLACK"},
                new ReportInfoModel(){Key = 4, Value = "BROWN"},
                new ReportInfoModel(){Key = 5, Value = "BLONDE"},
                new ReportInfoModel(){Key = 6, Value = "BLACK AND GRAY"},
                new ReportInfoModel(){Key = 7, Value = "DIRTY BLONDE"},
                new ReportInfoModel(){Key = 8, Value = "Not requested by client."}

            };
            return hairColors;
        }

        public ReportInfoModel HairLength
        {
            get => _hairLength;
            set
            {
                _hairLength = value;
                OnPropertyChanged(nameof(HairLength));
            }
        }

        public List<ReportInfoModel> GetHairLengths()
        {
            var hairLengths = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "BUZZ CUT"},
                new ReportInfoModel(){Key = 2, Value = "EAR LENGTH"},
                new ReportInfoModel(){Key = 3, Value = "CHIN LENGTH"},
                new ReportInfoModel(){Key = 4, Value = "SHOULDER LENGTH"},
                new ReportInfoModel(){Key = 5, Value = "ARMPIT LENGTH"},
                new ReportInfoModel(){Key = 6, Value = "MID-BACK LENGTH"},
                new ReportInfoModel(){Key = 7, Value = "TAIL BONE LENGTH"},
                new ReportInfoModel(){Key = 8, Value = "Not requested by client."}

            };
            return hairLengths;
        }

        public ReportInfoModel SurveillanceEndReason
        {
            get => _surveillanceEndReason;
            set
            {
                _surveillanceEndReason = value;
                OnPropertyChanged(nameof(SurveillanceEndReason));
            }
        }

        public List<ReportInfoModel> GetSurveillanceEndReason()
        {
            var endReasons = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "With the subject remaining away from the residence, efforts were discontinued."},
                new ReportInfoModel(){Key = 2, Value = "The subject was not observed on the outside of the residence. With no activity, on behalf of the subject, surveillance was discontinued."},
                new ReportInfoModel(){Key = 3, Value = "Surveillance efforts were discontinued as the subject was neither observed nor confirmed."},
                new ReportInfoModel(){Key = 4, Value = "With the subject containing their activities to the interior of the residence, and out of view, surveillance was discontinued."},
                new ReportInfoModel(){Key = 5, Value = "Not requested by client."}

            };
            return endReasons;
        }

        public ReportInfoModel TypeOfVideoObtained
        {
            get => _typeOfVideoObtained;
            set
            {
                _typeOfVideoObtained = value;
                OnPropertyChanged(nameof(TypeOfVideoObtained));
            }
        }

        public List<ReportInfoModel> GetTypeOfVideo()
        {
            var typeofVideo = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "Covert video was obtained."},
                new ReportInfoModel(){Key = 2, Value = "There was no video obtained."},
                new ReportInfoModel(){Key = 3, Value = "Not requested by client."}

            };
            return typeofVideo;
        }

        public ReportInfoModel SubjectVideoConfirmation
        {
            get => _subjectVideoConfirmation;
            set
            {
                _subjectVideoConfirmation = value;
                OnPropertyChanged(nameof(SubjectVideoConfirmation));
            }
        }
        public List<ReportInfoModel> GetSubjectVideoConfirmation()
        {
            var typeofVideo = new List<ReportInfoModel>()
            {
                new ReportInfoModel(){Key = 1, Value = "Yes, vidoe was obtained."},
                new ReportInfoModel(){Key = 2, Value = "There was no video obtained."},
                new ReportInfoModel(){Key = 3, Value = "Not requested by client."}

            };
            return typeofVideo;
        }


        //public properties of type ICommand that create an instance of the Command class and calls it's shared-named method
        public ICommand ReadSqlAssignedCasesCommand => _readSqlAssignedCasesCommand ?? (_readSqlAssignedCasesCommand = new Command(ReadSqlAssignedCasesMethod));
        public ICommand ReadSqlCommand => _readSqlCommand ?? (_readSqlCommand = new Command(ReadSqlMethod));
        public ICommand CreateSqlCommand => _createSqlCommand ?? (_createSqlCommand = new Command(CreateSqlMethod));
        public ICommand GetClientAndCasePickersCommand => _getClientAndCasePickersCommand ?? (_getClientAndCasePickersCommand = new Command(GetClientAndCasePickersMethod));
        public ICommand UpdateSqlCommand => _updateSqlCommand ?? (_updateSqlCommand = new Command(UpdateSqlMethod));
        public ICommand DeleteSqlCommand => _deleteSqlCommand ?? (_deleteSqlCommand = new Command(DeleteSqlMethod));
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(DeleteMethod));
        public ICommand UpdateCommand => _updateCommand ?? (_updateCommand = new Command(UpdateMethod));
        public ICommand ReadAssignedCasesIsVisibleCommand => _readAssignedCasesIsVisibleCommand ?? (_readAssignedCasesIsVisibleCommand = new Command(ReadAssignedCasesIsVisibleMethod));

    }
}
