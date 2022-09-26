using IReport.Models;
using IReport.Models.Base;
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
    /// VIEWMODEL FOR CLIENTINFOVIEW

    /// </summary>
    /// 
    public class ClientInfoViewModel : ViewModelBase
    {
        public ClientInfoViewModel(SqlModel sqlModel, ClientInfoModel clientInfoModel, CaseInfoModel caseInfoModel)
        {
            SqlModel = sqlModel;
            ClientInfoModel = clientInfoModel;
            CaseInfoModel = caseInfoModel;

            ObservableCollection<ClientInfoModel> clients = new ObservableCollection<ClientInfoModel>();
            ClientInfoModel.ClientInfoModelList = clients;

            ClientInfoModel.YesNoIDontKnowPicker = GetYesNoIDontKnowPicker().OrderBy(t => t.Value).ToList();

        }

        //PRIVATE MEMBER OF PICKER'S SELECTED ITEM
        ClientInfoModel _selectedYesNoIDontKnowPicker;

        //PRIVATE MEMBERS OF MODELS
        SqlModel _sqlModel;
        ClientInfoModel _clientInfoModel;
        CaseInfoModel _caseInfoModel;

        //PRIVATE ICOMMAND MEMBERS
        //PUBLIC ICOMMAND PROPERTIES AT END OF CLASS
        ICommand _createSqlCommand;
        ICommand _readSqlCommand;
        ICommand _updateSqlCommand;
        ICommand _deleteSqlCommand;
        ICommand _createCommand;
        ICommand _updateCommand;
        ICommand _deleteCommand;
        ICommand _getClientAndCasePickersCommand;


        public SqlModel SqlModel
        {
            get => _sqlModel;
            set
            {
                _sqlModel = value;
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

        public CaseInfoModel CaseInfoModel
        {
            get => _caseInfoModel;
            set
            {
                _caseInfoModel = value;
                OnPropertyChanged(nameof(CaseInfoModel));

            }
        }


        //these 4 provide ISVISIBLE only
        public ICommand CreateNewClientCommand { get; }
        public void CreateNewClientMethod()
        {
            ClientInfoModel.CreatingNewClient = true;
        }

        public ICommand ReadClientCommand { get; }
        public void ReadClientMethod()
        {
            ClientInfoModel.ReadingClient = true;
        }

        public ICommand UpdateClientCommand { get; }
        public void UpdateClientMethod()
        {
            ClientInfoModel.UpdatingClient = true;
        }

        public ICommand DeleteClientCommand { get; }
        public void DeleteClientMethod()
        {
            ClientInfoModel.DeletingClient = true;
        }

        //isvisble CREATE A CLIENT BUTTON
        public void CreateMethod()
        {
            ClientInfoModel.DeletingClient = false;
            ClientInfoModel.UpdatingClient = false;
            ClientInfoModel.CreatingNewClient = true;
            ClientInfoModel.ReadingClient = false;
        }

        //CREATE OR POST TO SQL CLIENTINFOTABLE
        public async void CreateSqlMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();
                if (ClientInfoModel.ClientName != string.Empty)
                {
                    SqlCommand sqlSaveAndReadCommand = new SqlCommand("SELECT * FROM dbo.ClientInfoTable WHERE ClientName = '" + ClientInfoModel.ClientName + "'", SqlModel.SqlConnection);

                    SqlModel.SqlDataReader = sqlSaveAndReadCommand.ExecuteReader();

                    if (SqlModel.SqlDataReader.Read())
                    {
                        SqlModel.SqlConnection.Close();

                        await Application.Current.MainPage.DisplayAlert("NOT VALID", "ITS ALREADY IN THE DATABASE", "OK");
                        ClientInfoModel.ClientName = string.Empty;
                    }
                    else
                    {
                        using (SqlCommand sqlInsertCommand = new SqlCommand(SqlModel.ClientInsertQuery, SqlModel.SqlConnection))
                        {
                            sqlInsertCommand.Parameters.Add(new SqlParameter("ClientName", ClientInfoModel.ClientName));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("ClientEmail", ClientInfoModel.ClientEmail));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("ClientPhoneNumber", ClientInfoModel.ClientPhoneNumber));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("ClientAddress", ClientInfoModel.ClientAddress));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("ClientMainContactName", ClientInfoModel.ClientMainContactName));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("ClientMainContactPhoneNumber", ClientInfoModel.ClientMainContactPhoneNumber));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("PricePerHour", ClientInfoModel.PricePerHour));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("NumberOfHoursBilledToDate", ClientInfoModel.NumberOfHoursBilledToDate));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("TotalAmountBilledToDate", ClientInfoModel.TotalAmountBilledToDate));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("TotalPaidToDate", ClientInfoModel.TotalPaidToDate));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("BalanceDue", ClientInfoModel.BalanceDue));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("ClientSince", ClientInfoModel.ClientSince.ToShortDateString()));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("SelectedYesNoIDontKnowPicker", SelectedYesNoIDontKnowPicker.Value));

                            SqlModel.SqlDataReader.Close();
                            sqlInsertCommand.ExecuteNonQuery();

                            await Application.Current.MainPage.DisplayAlert("SUCCESSFULLY ADDED", "CLICK OK TO PROCEED", "OK");
                            ClientInfoModel.ClientName = string.Empty;
                            ClientInfoModel.ClientEmail = string.Empty;
                            ClientInfoModel.ClientAddress = string.Empty;
                            ClientInfoModel.ClientPhoneNumber = string.Empty;
                            ClientInfoModel.ClientMainContactName = string.Empty;
                            ClientInfoModel.ClientMainContactPhoneNumber = string.Empty;
                            ClientInfoModel.PricePerHour = 0;
                            ClientInfoModel.NumberOfHoursBilledToDate = 0;
                            ClientInfoModel.TotalPaidToDate = 0;
                            SelectedYesNoIDontKnowPicker.Value = string.Empty;

                        }//END OF USING

                    }//END OF ELSE

                    SqlModel.SqlConnection.Close();

                }//END OF FIRST IF

            }//END OF TRY BLOCK

            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");
                ClientInfoModel.ClientName = string.Empty;

            }
            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }

        //READ FROM SQL CLIENTINFOTABLE
        public async void ReadSqlMethod()
        {
            try
            {
                ClientInfoModel.DeletingClient = false;
                ClientInfoModel.UpdatingClient = false;
                ClientInfoModel.CreatingNewClient = false;
                ClientInfoModel.ReadingClient = true;

                SqlModel.SqlConnection.Open();
                SqlCommand clientCommand = new SqlCommand(SqlModel.ClientQuery, SqlModel.SqlConnection);

                SqlDataReader clientReader = clientCommand.ExecuteReader();
                while (clientReader.Read())
                {

                    ClientInfoModel.ClientInfoModelList.Insert(0, new ClientInfoModel
                    {
                        Identifier = Convert.ToInt32(clientReader["Identifier"]),
                        ClientName = clientReader["ClientName"].ToString(),
                        ClientEmail = clientReader["ClientEmail"].ToString(),
                        ClientPhoneNumber = clientReader["ClientPhoneNumber"].ToString(),
                        ClientAddress = clientReader["ClientAddress"].ToString(),
                        ClientMainContactName = clientReader["ClientMainContactName"].ToString(),
                        ClientMainContactPhoneNumber = clientReader["ClientMainContactPhoneNumber"].ToString(),
                        PricePerHour = Convert.ToInt32(clientReader["PricePerHour"]),
                        NumberOfHoursBilledToDate = Convert.ToInt32(clientReader["NumberOfHoursBilledToDate"]),
                        TotalPaidToDate = Convert.ToInt32(clientReader["TotalPaidToDate"]),
                        ClientSince = Convert.ToDateTime(clientReader["ClientSince"])
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

        //isvisble UPDATE CLIENT BUTTON
        public void UpdateMethod()
        {
            ClientInfoModel.DeletingClient = false;
            ClientInfoModel.UpdatingClient = true;
            ClientInfoModel.CreatingNewClient = false;
            ClientInfoModel.ReadingClient = false;
        }
        //UPDATE SQL CLIENTINFOTABLE
        public async void UpdateSqlMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();


                string queryString = $"UPDATE dbo.ClientInfoTable SET ClientName = '{ClientInfoModel.ClientName}' , ClientEmail = '{ClientInfoModel.ClientEmail}' , ClientPhoneNumber = '{ClientInfoModel.ClientPhoneNumber}' , ClientAddress = '{ClientInfoModel.ClientAddress}' , ClientMainContactName = '{ClientInfoModel.ClientMainContactName}' , ClientMainContactPhoneNumber = '{ClientInfoModel.ClientMainContactPhoneNumber}' , PricePerHour = '{ClientInfoModel.PricePerHour}' , NumberOfHoursBilledToDate = '{ClientInfoModel.NumberOfHoursBilledToDate}' , TotalAmountBilledToDate = '{ClientInfoModel.TotalAmountBilledToDate}' , TotalPaidToDate = '{ClientInfoModel.TotalPaidToDate}' , BalanceDue = '{ClientInfoModel.BalanceDue}' , ClientSince = '{ClientInfoModel.ClientSince}' , SelectedYesNoIDontKnowPicker = '{SelectedYesNoIDontKnowPicker.Value}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

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
        //isvisble DELETE CLIENT BUTTON

        public void DeleteMethod()
        {
            ClientInfoModel.DeletingClient = true;
            ClientInfoModel.UpdatingClient = false;
            ClientInfoModel.CreatingNewClient = false;
            ClientInfoModel.ReadingClient = false;
        }

        //DELETE FROM SQL CLIENTINFOTABLE
        public async void DeleteSqlMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();
                //ill have to make an identifier for these tables too for this to work

                using (SqlCommand deleteCommand = new SqlCommand($"Delete FROM dbo.ClientInfoTable WHERE Identifier = {ClientInfoModel.Identifier}", SqlModel.SqlConnection))
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

        //READS FROM CASEINFOTABLE AND CLIENTINFOTABLE TO POPULATE BOTH PICKERS
        public async void GetClientAndCasePickersMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();

                SqlCommand caseCommand = new SqlCommand(SqlModel.CaseQuery, SqlModel.SqlConnection);

                SqlDataReader caseReader = caseCommand.ExecuteReader();
                while (caseReader.Read())
                {
                    CaseInfoModel.CaseInfoModelList.Add(new CaseInfoModel
                    {
                        CaseId = caseReader["CaseId"].ToString()
                    });
                }
                caseReader.Close();


                SqlCommand clientCommand = new SqlCommand(SqlModel.ClientQuery, SqlModel.SqlConnection);

                SqlDataReader clientReader = clientCommand.ExecuteReader();
                while (clientReader.Read())
                {
                    ClientInfoModel.ClientInfoModelList.Add(new ClientInfoModel
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

        ////PUBLIC PROPERTY AND LIST TO POPULATE PICKER
        ////each pair of property/list is for the pickers to get these values using LINQ

        public ClientInfoModel SelectedYesNoIDontKnowPicker
        {
            get => _selectedYesNoIDontKnowPicker;
            set
            {
                _selectedYesNoIDontKnowPicker = value;
                OnPropertyChanged(nameof(SelectedYesNoIDontKnowPicker));
            }
        }
        public List<ClientInfoModel> GetYesNoIDontKnowPicker()
        {
            var yesOrno = new List<ClientInfoModel>()
            {
                new ClientInfoModel(){Key = 1, Value = "YES"},
                new ClientInfoModel(){Key = 2, Value = "NO"},
                new ClientInfoModel(){Key = 3, Value = "I Don't Know"}
            };
            return yesOrno;
        }

        //INSTANCES OF THE COMMAND CLASS CALLING EACH METHOD
        public ICommand CreateSqlCommand => _createSqlCommand ?? (_createSqlCommand = new Command(CreateSqlMethod));
        public ICommand ReadSqlCommand => _readSqlCommand ?? (_readSqlCommand = new Command(ReadSqlMethod));
        public ICommand UpdateSqlCommand => _updateSqlCommand ?? (_updateSqlCommand = new Command(UpdateSqlMethod));
        public ICommand DeleteSqlCommand => _deleteSqlCommand ?? (_deleteSqlCommand = new Command(DeleteSqlMethod));
        public ICommand CreateCommand => _createCommand ?? (_createCommand = new Command(CreateMethod));
        public ICommand UpdateCommand => _updateCommand ?? (_updateCommand = new Command(UpdateMethod));
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(DeleteMethod));
        public ICommand GetClientAndCasePickersCommand => _getClientAndCasePickersCommand ?? (_getClientAndCasePickersCommand = new Command(GetClientAndCasePickersMethod));



        //ATTEMPT AT RESTFUL WEB SERVICE - FINAL STEP

        //private async void GetClients()
        //{

        //    using ( var httpClient = new HttpClient())
        //    {
        //        //send and get request
        //        var uri = "http://geturi";
        //        var response = await httpClient.GetStringAsync(uri);

        //        //handle response
        //        var ClientList = JsonConvert.DeserializeObject<List<ClientInfoModel>>(response);

        //        ClientJsonModels = new ObservableCollection<ClientInfoModel>(ClientList);
        //    }


        //}

        //ObservableCollection<ClientInfoModel> _clientModels;

        //public ObservableCollection<ClientInfoModel> ClientJsonModels
        //{
        //    //this was suggested
        //    //get => _clientModels ?? (_clientModels = new ObservableCollection<ClientInfoModel>());  

        //    get => _clientModels;
        //    set
        //    {
        //        _clientModels = value;
        //        OnPropertyChanged(nameof(ClientJsonModels));
        //    }
        //}


    }
}
