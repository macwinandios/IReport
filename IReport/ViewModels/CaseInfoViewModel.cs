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
    /// VIEWMODEL FOR CASEINFOVIEW

    /// ADD A LINE DIRECTORY
    //CREATE METHOD SQL SERVER IS ON LINE:
    //READ METHOD FOR SQL IS ON LINE:
    // UPDATE METHOD FOR SQL IS ON LINE:
    // DELETE METHOD FOR SQL IS ON LINE:
    /// </summary>


    public class CaseInfoViewModel : ViewModelBase, ISql
    {
        public CaseInfoViewModel(ISql isql)
        {
            _isql = isql;
        }

        public CaseInfoViewModel(CaseInfoModel caseInfoModel, ClientInfoModel clientInfoModel, ReportInfoModel reportInfoModel, SqlModel sqlModel)
        {

            CaseInfoModel = caseInfoModel;
            ClientInfoModel = clientInfoModel;
            ReportInfoModel = reportInfoModel;
            SqlModel = sqlModel;

            ObservableCollection<CaseInfoModel> cases = new ObservableCollection<CaseInfoModel>();
            CaseInfoModel.CaseInfoModelList = cases;


            CaseInfoModel.SkinComplexionList = GetComplexions().OrderBy(t => t.Value).ToList();

            CaseInfoModel.EthnicityList = GetEthnicity().OrderBy(t => t.Value).ToList();
            CaseInfoModel.LanguageList = GetLanguage().OrderBy(t => t.Value).ToList();
            CaseInfoModel.LevelOfAwarenessList = GetAwareness().OrderBy(t => t.Value).ToList();
        }

        ISql _isql;

        //PRIVATE MEMBERS FOR EACH COMMAND
        //the public properties for these private members are at the end of this class to avoid clutter
        private ICommand _createSqlAssignACaseCommand;
        private ICommand _assignACaseCommand;
        private ICommand _createSqlCommand;
        private ICommand _readSqlCommand;
        private ICommand _updateSqlCommand;
        private ICommand _updateCommand;
        private ICommand _deleteSqlCommand;
        private ICommand _deleteCommand;
        private ICommand _createNewCaseCommand;
        private ICommand _deleteAssignedCasesSqlCommand;
        private ICommand _updateAssignedCasesSqlCommand;
        private ICommand _getClientAndCasePickersCommand;


        //PRIVATE MEMBERS OF MODELS AND SQL INTERFACE
        ReportInfoModel _reportInfoModel;
        CaseInfoModel _caseInfoModel;
        ClientInfoModel _clientInfoModel;
        SqlModel _sqlModel;

        //PRIVATE MEMBERS FOR PICKERS LIST
        CaseInfoModel _selectedAwareness;
        CaseInfoModel _selectedEthnicity;
        CaseInfoModel _selectedComplexion;
        CaseInfoModel _selectedLanguage;



        //PUBLIC PROPERTIES BEGIN
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



        //Reads from SQL to populate the picker
        //public ICommand GetClientAndCasePickersCommand { get; }
        public async void GetClientAndCasePickersMethod()
        {
            try
            {

                SqlModel.SqlConnection.Open();
                SqlCommand caseCommand = new SqlCommand(SqlModel.CaseQuery, SqlModel.SqlConnection);

                SqlDataReader reader = caseCommand.ExecuteReader();
                while (reader.Read())
                {
                    CaseInfoModel.CaseInfoModelList.Add(new CaseInfoModel
                    {
                        CaseId = reader["CaseId"].ToString()
                    });
                }
                reader.Close();
                SqlModel.SqlConnection.Close();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");
                SqlModel.SqlConnection.Close();

            }
            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }

        //ISVISIBLE BUTTON
        public void CreateNewCaseMethod()
        {
            CaseInfoModel.CreatingCase = true;
            CaseInfoModel.UpdatingCase = false;
            CaseInfoModel.ReadingCase = false;
            CaseInfoModel.DeletingCase = false;
            CaseInfoModel.AssigningACase = false;


        }
        //CREATE OR POST TO CASEINFOTABLE

        public async void CreateSqlMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();


                if (CaseInfoModel.CaseId != string.Empty)
                {
                    SqlCommand sqlSaveAndReadCommand = new SqlCommand("SELECT * FROM dbo.CaseInfoTable WHERE CaseId = '" + CaseInfoModel.CaseId + "'", SqlModel.SqlConnection);

                    SqlModel.SqlDataReader = sqlSaveAndReadCommand.ExecuteReader();

                    if (SqlModel.SqlDataReader.Read())
                    {
                        SqlModel.SqlConnection.Close();

                        await Application.Current.MainPage.DisplayAlert("NOT VALID", "THIS CASE I.D. IS ALREADY IN THE DATABASE", "OK");
                        CaseInfoModel.CaseId = string.Empty;
                    }
                    else
                    {
                        using (SqlCommand sqlInsertCommand = new SqlCommand(SqlModel.CaseInsertQuery, SqlModel.SqlConnection))
                        {
                            sqlInsertCommand.Parameters.Add(new SqlParameter("CaseId", CaseInfoModel.CaseId));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("ClientName", ClientInfoModel.ClientName));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("FirstName", CaseInfoModel.FirstName));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("MiddleName", CaseInfoModel.MiddleName));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("LastName", CaseInfoModel.LastName));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("DateOfBirth", CaseInfoModel.DateOfBirth.ToShortDateString()));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("PhoneNumber", CaseInfoModel.PhoneNumber));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("HomeAddress", CaseInfoModel.HomeAddress));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("HomeDescription", CaseInfoModel.HomeDescription));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("WorkAddress", CaseInfoModel.WorkAddress));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("WorkDescription", CaseInfoModel.WorkDescription));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("Height", CaseInfoModel.Height));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("Weight", CaseInfoModel.Weight));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("EyeColor", CaseInfoModel.EyeColor));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("HairColor", CaseInfoModel.HairColor));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("SelectedComplexion", SelectedComplexion.Value));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("TattooDescription", CaseInfoModel.TattooDescription));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("SelectedEthnicity", SelectedEthnicity.Value));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("SelectedLanguage", SelectedLanguage.Value));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("SelectedAwareness", SelectedAwareness.Value));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("FrequentedPlaces", CaseInfoModel.FrequentedPlaces));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("VehicleYear", CaseInfoModel.VehicleYear));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("VehicleMake", CaseInfoModel.VehicleMake));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("VehicleModel", CaseInfoModel.VehicleModel));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("VehicleColor", CaseInfoModel.VehicleColor));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("LicensePlateNumber", CaseInfoModel.LicensePlateNumber));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("LicensePlateState", CaseInfoModel.LicensePlateState));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("LicensePlateColor", CaseInfoModel.LicensePlateColor));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("CaseDetails", CaseInfoModel.CaseDetails));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("DateCaseInitialized", CaseInfoModel.DateCaseInitialized.ToShortDateString()));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("DateOfSurveillance", CaseInfoModel.DateOfSurveillance.ToShortDateString()));

                            SqlModel.SqlDataReader.Close();
                            sqlInsertCommand.ExecuteNonQuery();

                            await Application.Current.MainPage.DisplayAlert("SUCCESSFULLY ADDED", "CLICK OK TO PROCEED", "OK");
                            CaseInfoModel.CaseId = string.Empty;
                            ClientInfoModel.ClientName = string.Empty;
                            CaseInfoModel.FirstName = string.Empty;
                            CaseInfoModel.MiddleName = string.Empty;
                            CaseInfoModel.LastName = string.Empty;
                            CaseInfoModel.PhoneNumber = string.Empty;
                            CaseInfoModel.HomeAddress = string.Empty;
                            CaseInfoModel.HomeDescription = string.Empty;
                            CaseInfoModel.WorkAddress = string.Empty;
                            CaseInfoModel.WorkDescription = string.Empty;
                            CaseInfoModel.Height = string.Empty;
                            CaseInfoModel.Weight = string.Empty;
                            CaseInfoModel.EyeColor = string.Empty;
                            CaseInfoModel.HairColor = string.Empty;
                            CaseInfoModel.TattooDescription = string.Empty;
                            CaseInfoModel.FrequentedPlaces = string.Empty;
                            CaseInfoModel.VehicleYear = string.Empty;
                            CaseInfoModel.VehicleMake = string.Empty;
                            CaseInfoModel.VehicleModel = string.Empty;
                            CaseInfoModel.VehicleColor = string.Empty;
                            CaseInfoModel.LicensePlateNumber = string.Empty;
                            CaseInfoModel.LicensePlateState = string.Empty;
                            CaseInfoModel.LicensePlateColor = string.Empty;
                            CaseInfoModel.CaseDetails = string.Empty;
                            SelectedComplexion.Value = string.Empty;
                            SelectedEthnicity.Value = string.Empty;
                            SelectedLanguage.Value = string.Empty;
                            SelectedAwareness.Value = string.Empty;
                        }//END OF USING

                    }//END OF ELSE

                    SqlModel.SqlConnection.Close();

                }//END OF FIRST IF

            }//END OF TRY BLOCK

            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");
                CaseInfoModel.CaseId = string.Empty;

            }
            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }

        //READ FROM SQL CASEINFOTABLE
        public async void ReadSqlMethod()
        {
            try
            {

                SqlModel.SqlConnection.Open();
                CaseInfoModel.ReadingCase = true;
                CaseInfoModel.UpdatingCase = false;
                CaseInfoModel.CreatingCase = false;
                CaseInfoModel.DeletingCase = false;
                CaseInfoModel.AssigningACase = false;


                SqlCommand clientCommand = new SqlCommand(SqlModel.CaseQuery, SqlModel.SqlConnection);

                SqlDataReader clientReader = clientCommand.ExecuteReader();
                while (clientReader.Read())
                {

                    CaseInfoModel.CaseInfoModelList.Insert(0, new CaseInfoModel
                    {
                        Identifier = Convert.ToInt32(clientReader["Identifier"]),
                        CaseId = clientReader["CaseId"].ToString(),
                        ClientName = clientReader["ClientName"].ToString(),
                        FirstName = clientReader["FirstName"].ToString(),
                        MiddleName = clientReader["MiddleName"].ToString(),
                        LastName = clientReader["LastName"].ToString(),
                        PhoneNumber = clientReader["PhoneNumber"].ToString(),
                        HomeAddress = clientReader["HomeAddress"].ToString(),
                        HomeDescription = clientReader["HomeDescription"].ToString(),
                        WorkAddress = clientReader["WorkAddress"].ToString(),
                        WorkDescription = clientReader["WorkDescription"].ToString(),
                        Height = clientReader["Height"].ToString(),
                        Weight = clientReader["Weight"].ToString(),
                        EyeColor = clientReader["EyeColor"].ToString(),
                        HairColor = clientReader["HairColor"].ToString(),
                        TattooDescription = clientReader["TattooDescription"].ToString(),
                        FrequentedPlaces = clientReader["FrequentedPlaces"].ToString(),
                        VehicleYear = clientReader["VehicleYear"].ToString(),
                        VehicleMake = clientReader["VehicleMake"].ToString(),
                        VehicleModel = clientReader["VehicleModel"].ToString(),
                        VehicleColor = clientReader["VehicleColor"].ToString(),
                        LicensePlateNumber = clientReader["LicensePlateNumber"].ToString(),
                        LicensePlateState = clientReader["LicensePlateState"].ToString(),
                        LicensePlateColor = clientReader["LicensePlateColor"].ToString(),
                        CaseDetails = clientReader["CaseDetails"].ToString(),
                        SelectedComplexion = clientReader["SelectedComplexion"].ToString(),
                        SelectedEthnicity = clientReader["SelectedEthnicity"].ToString(),
                        SelectedLanguage = clientReader["SelectedLanguage"].ToString(),
                        SelectedAwareness = clientReader["SelectedAwareness"].ToString(),
                    });


                }
                clientReader.Close();
                SqlModel.SqlConnection.Close();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");
                SqlModel.SqlConnection.Close();

            }
            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }


        //ISVISIBLE BUTTON
        public void UpdateMethod()
        {
            CaseInfoModel.UpdatingCase = true;
            CaseInfoModel.CreatingCase = false;
            CaseInfoModel.ReadingCase = false;
            CaseInfoModel.DeletingCase = false;
            CaseInfoModel.AssigningACase = false;

        }

        //UPDATE SQL CASEINFOTABLE
        public async void UpdateSqlMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();

                CaseInfoModel.CreatingCase = false;
                CaseInfoModel.ReadingCase = false;
                CaseInfoModel.DeletingCase = false;
                CaseInfoModel.AssigningACase = false;

                string queryString = $"UPDATE dbo.CaseInfoTable SET CaseId = '{CaseInfoModel.CaseId}' , ClientName = '{ClientInfoModel.ClientName}' ,FirstName = '{CaseInfoModel.FirstName}' , MiddleName = '{CaseInfoModel.MiddleName}' , LastName = '{CaseInfoModel.LastName}' , PhoneNumber = '{CaseInfoModel.PhoneNumber}' , HomeAddress = '{CaseInfoModel.HomeAddress}' , HomeDescription = '{CaseInfoModel.HomeDescription}' , WorkAddress = '{CaseInfoModel.WorkAddress}' , WorkDescription = '{CaseInfoModel.WorkDescription}' , Height = '{CaseInfoModel.Height}' , Weight = '{CaseInfoModel.Weight}' , EyeColor = '{CaseInfoModel.EyeColor}' , HairColor = '{CaseInfoModel.HairColor}' , TattooDescription = '{CaseInfoModel.TattooDescription}' , FrequentedPlaces = '{CaseInfoModel.FrequentedPlaces}' , VehicleYear = '{CaseInfoModel.VehicleYear}' , VehicleMake = '{CaseInfoModel.VehicleMake}' , VehicleModel = '{CaseInfoModel.VehicleModel}' , VehicleColor = '{CaseInfoModel.VehicleColor}' , LicensePlateNumber = '{CaseInfoModel.LicensePlateNumber}' ,LicensePlateState = '{CaseInfoModel.LicensePlateState}' , LicensePlateColor = '{CaseInfoModel.LicensePlateColor}' , CaseDetails = '{CaseInfoModel.CaseDetails}' , SelectedComplexion = '{CaseInfoModel.SelectedComplexion}' , SelectedEthnicity = '{CaseInfoModel.SelectedEthnicity}' , SelectedLanguage = '{CaseInfoModel.SelectedLanguage}' , SelectedAwareness = '{CaseInfoModel.SelectedAwareness}' WHERE Identifier ='{CaseInfoModel.Identifier}'";

                using (SqlCommand command = new SqlCommand(queryString, SqlModel.SqlConnection))
                {
                    command.ExecuteNonQuery();
                    SqlModel.SqlConnection.Close();

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

        //ISVISIBLE BUTTON
        public void DeleteMethod()
        {
            CaseInfoModel.DeletingCase = true;
            CaseInfoModel.UpdatingCase = false;
            CaseInfoModel.CreatingCase = false;
            CaseInfoModel.ReadingCase = false;
            CaseInfoModel.AssigningACase = false;

        }
        //DELETE FROM SQL CASEINFOTABLE
        public async void DeleteSqlMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();

                using (SqlCommand deleteCommand = new SqlCommand($"Delete FROM dbo.CaseInfoTable WHERE Identifier = {CaseInfoModel.Identifier}", SqlModel.SqlConnection))
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

        //DELETE FROM SQL ASSIGNEDCASESINFOTABLE
        public async void DeleteAssignedCasesSqlMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();


                using (SqlCommand deleteCommand = new SqlCommand($"Delete FROM dbo.AssignedCasesInfoTable WHERE Identifier = {CaseInfoModel.Identifier}", SqlModel.SqlConnection))
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

        //UPDATE SQL ASSIGNEDCASESINFOTABLE

        public async void UpdateAssignedCasesSqlMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();
                string queryString = $"UPDATE dbo.AssignedCasesInfoTable SET EmployeeName = '{CaseInfoModel.AssignedEmployeeUsername}' , CaseId = '{CaseInfoModel.AssignedCaseId}' ,Date = '{CaseInfoModel.AssignedDate}' , Time = '{CaseInfoModel.AssignedTime}'  WHERE Identifier ='{CaseInfoModel.Identifier}'";

                using (SqlCommand command = new SqlCommand(queryString, SqlModel.SqlConnection))
                {
                    command.ExecuteNonQuery();
                    SqlModel.SqlConnection.Close();

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

        //isvisible for assign a case
        public void AssignACaseMethod()
        {
            CaseInfoModel.AssigningACase = true;

            CaseInfoModel.CreatingCase = false;
            CaseInfoModel.UpdatingCase = false;
            CaseInfoModel.ReadingCase = false;
            CaseInfoModel.DeletingCase = false;


        }

        //CREATE OR POST TO SQL ASSIGNEDCASESINFOTABLE

        public async void CreateSqlAssignACaseMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();
                if (CaseInfoModel.CaseId != string.Empty)
                {
                    SqlCommand sqlSaveAndReadCommand = new SqlCommand("SELECT * FROM dbo.AssignedCasesInfoTable WHERE EmployeeName = '" + CaseInfoModel.AssignedEmployeeUsername + "' AND CaseId = '" + CaseInfoModel.AssignedCaseId + "'AND Date = '" + CaseInfoModel.AssignedDate + "' AND Time = '" + CaseInfoModel.AssignedTime + "'", SqlModel.SqlConnection);

                    SqlModel.SqlDataReader = sqlSaveAndReadCommand.ExecuteReader();

                    if (SqlModel.SqlDataReader.Read())
                    {
                        SqlModel.SqlConnection.Close();

                        await Application.Current.MainPage.DisplayAlert("NOT VALID", "THIS CASE IS ALREADY ASSIGNED.", "OK");
                        CaseInfoModel.AssignedEmployeeUsername = string.Empty;
                        CaseInfoModel.AssignedCaseId = string.Empty;
                    }
                    else
                    {
                        using (SqlCommand sqlInsertCommand = new SqlCommand("INSERT INTO dbo.AssignedCasesInfoTable VALUES (@EmployeeName, @CaseId, @Date, @Time, @Identifier)", SqlModel.SqlConnection))
                        {
                            sqlInsertCommand.Parameters.Add(new SqlParameter("EmployeeName", CaseInfoModel.AssignedEmployeeUsername));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("CaseId", CaseInfoModel.AssignedCaseId));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("Date", CaseInfoModel.AssignedDate.ToShortDateString()));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("Time", CaseInfoModel.AssignedTime.ToShortTimeString()));

                            SqlModel.SqlDataReader.Close();
                            sqlInsertCommand.ExecuteNonQuery();

                            await Application.Current.MainPage.DisplayAlert("SUCCESSFULLY ADDED", "CLICK OK TO PROCEED", "OK");
                            CaseInfoModel.AssignedEmployeeUsername = string.Empty;
                            CaseInfoModel.AssignedCaseId = string.Empty;

                        }//END OF USING

                    }//END OF ELSE

                    SqlModel.SqlConnection.Close();

                }//END OF FIRST IF

            }//END OF TRY BLOCK

            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");
                CaseInfoModel.AssignedEmployeeUsername = string.Empty;
                CaseInfoModel.AssignedCaseId = string.Empty;


            }
            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }


        //PUBLIC LIST PROPERTIES TO POPULATE PICKERS
        ////public properties and lists
        ////each pair of property/list is for the pickers to get these values using LINQ

        public CaseInfoModel SelectedAwareness
        {
            get => _selectedAwareness;
            set
            {
                _selectedAwareness = value;
                OnPropertyChanged(nameof(SelectedAwareness));
            }
        }
        public List<CaseInfoModel> GetAwareness()
        {
            var awareness = new List<CaseInfoModel>()
            {
                new CaseInfoModel(){Key = 1, Value = "Extremely Aware"},
                new CaseInfoModel(){Key = 2, Value = "Very Aware"},
                new CaseInfoModel(){Key = 3, Value = "A Little Aware"},
                new CaseInfoModel(){Key = 4, Value = "Not Aware At All"},
            };
            return awareness;
        }
        public CaseInfoModel SelectedComplexion
        {
            get => _selectedComplexion;
            set
            {
                _selectedComplexion = value;

                OnPropertyChanged(nameof(SelectedComplexion));
            }
        }
        public List<CaseInfoModel> GetComplexions()
        {
            var complexions = new List<CaseInfoModel>()
            {
                new CaseInfoModel(){Key = 1, Value = "Very Light Skin"},
                new CaseInfoModel(){Key = 2, Value = "Light Skin"},
                new CaseInfoModel(){Key = 3, Value = "Tanned Skin"},
                new CaseInfoModel(){Key = 4, Value = "Brown Skin"},
                new CaseInfoModel(){Key = 5, Value = "Very Brown Skin"},
                new CaseInfoModel(){Key = 6, Value = "We Don't Know"}
            };
            return complexions;
        }

        public CaseInfoModel SelectedEthnicity
        {
            get => _selectedEthnicity;
            set
            {
                _selectedEthnicity = value;
                OnPropertyChanged(nameof(SelectedEthnicity));
            }
        }
        public List<CaseInfoModel> GetEthnicity()
        {
            var ethnicities = new List<CaseInfoModel>()
            {
                new CaseInfoModel(){Key = 1, Value = "African American"},
                new CaseInfoModel(){Key = 2, Value = "White"},
                new CaseInfoModel(){Key = 3, Value = "Asian"},
                new CaseInfoModel(){Key = 4, Value = "South American"},
                new CaseInfoModel(){Key = 5, Value = "Indian"},
                new CaseInfoModel(){Key = 6, Value = "African"},
                new CaseInfoModel(){Key = 7, Value = "European"},
                new CaseInfoModel(){Key = 8, Value = "Eastern European"},
                new CaseInfoModel(){Key = 9, Value = "Caribbean Islands"}
            };
            return ethnicities;
        }

        public CaseInfoModel SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;

                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }
        public List<CaseInfoModel> GetLanguage()
        {
            var languages = new List<CaseInfoModel>()
            {
                new CaseInfoModel(){Key = 1, Value = "English"},
                new CaseInfoModel(){Key = 2, Value = "Spanish"},
                new CaseInfoModel(){Key = 3, Value = "Russian"},
                new CaseInfoModel(){Key = 4, Value = "French"},
                new CaseInfoModel(){Key = 5, Value = "Chinese"},
                new CaseInfoModel(){Key = 6, Value = "Hindi"},
                new CaseInfoModel(){Key = 7, Value = "Arabic"},
                new CaseInfoModel(){Key = 8, Value = "Bengali"},
                new CaseInfoModel(){Key = 9, Value = "German"}
            };
            return languages;
        }


        //public properties of type ICommand that create an instance of the Command class and calls it's shared-named method
        public ICommand CreateSqlAssignACaseCommand => _createSqlAssignACaseCommand ?? (_createSqlAssignACaseCommand = new Command(CreateSqlAssignACaseMethod));

        public ICommand AssignACaseCommand => _assignACaseCommand ?? (_assignACaseCommand = new Command(AssignACaseMethod));

        public ICommand CreateSqlCommand => _createSqlCommand ?? (_createSqlCommand = new Command(CreateSqlMethod));
        public ICommand ReadSqlCommand => _readSqlCommand ?? (_readSqlCommand = new Command(ReadSqlMethod));

        public ICommand UpdateSqlCommand => _updateSqlCommand ?? (_updateSqlCommand = new Command(UpdateSqlMethod));

        public ICommand DeleteSqlCommand => _deleteSqlCommand ?? (_deleteSqlCommand = new Command(DeleteSqlMethod));

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(DeleteMethod));

        public ICommand UpdateCommand => _updateCommand ?? (_updateCommand = new Command(UpdateMethod));
        public ICommand CreateNewCaseCommand => _createNewCaseCommand ?? (_createNewCaseCommand = new Command(CreateNewCaseMethod));

        public ICommand DeleteAssignedCasesSqlCommand => _deleteAssignedCasesSqlCommand ?? (_deleteAssignedCasesSqlCommand = new Command(DeleteAssignedCasesSqlMethod));

        public ICommand UpdateAssignedCasesSqlCommand => _updateAssignedCasesSqlCommand ?? (_updateAssignedCasesSqlCommand = new Command(UpdateAssignedCasesSqlMethod));

        public ICommand GetClientAndCasePickersCommand => _getClientAndCasePickersCommand ?? (_getClientAndCasePickersCommand = new Command(GetClientAndCasePickersMethod));


    }
}
