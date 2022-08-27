using IReport.Models;
using IReport.Models.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using IReport.Services;

namespace IReport.ViewModels
{
    public  class ClientInfoViewModel : ViewModelBase
    {
        
        public ClientInfoViewModel()
        {
            ClientInfoModel = new ClientInfoModel();
            CaseInfoModel = new CaseInfoModel();
            CreateSqlCommand = new Command(CreateSqlMethod);
            ReadSqlCommand = new Command(ReadSqlMethod);
            UpdateSqlCommand = new Command(UpdateSqlMethod);
            DeleteSqlCommand = new Command(DeleteSqlMethod);
            CreateCommand = new Command(CreateMethod);
            UpdateCommand = new Command(UpdateMethod);
            DeleteCommand = new Command(DeleteMethod);
            SqlModel = new SqlModel();
            CheckConnectionCommand = new Command(CheckConnectionMethod);
            GetClientAndCasePickersCommand = new Command(GetClientAndCasePickersMethod);


            ObservableCollection<ClientInfoModel> clients = new ObservableCollection<ClientInfoModel>();
            ClientInfoModel.ClientInfoModelList = clients;

            ClientInfoModel.YesNoIDontKnowPicker = GetYesNoIDontKnowPicker().OrderBy(t => t.Value).ToList();

        }
        //for picker selecteditem
        ClientInfoModel _selectedYesNoIDontKnowPicker;
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


        private SqlModel _sqlModel;
        public SqlModel SqlModel
        {
            get => _sqlModel;
            set
            {
                _sqlModel = value;
            }
        }

        private ClientInfoModel _clientInfoModel;
        public ClientInfoModel ClientInfoModel
        {
            get => _clientInfoModel;
            set
            {
                _clientInfoModel = value;
                OnPropertyChanged(nameof(ClientInfoModel));

            }
        }

        private CaseInfoModel _caseInfoModel;
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

        public ICommand CreateCommand { get; }

        public void CreateMethod()
        {
            ClientInfoModel.DeletingClient = false;
            ClientInfoModel.UpdatingClient = false;
            ClientInfoModel.CreatingNewClient = true;
            ClientInfoModel.ReadingClient = false;
        }

        public ICommand CreateSqlCommand { get; }
        public async void CreateSqlMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();
                if(ClientInfoModel.ClientName != string.Empty)
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

        public ICommand CheckConnectionCommand { get; }
        public async void CheckConnectionMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();
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


        public ICommand ReadSqlCommand { get; }
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
                    ClientInfoModel.ClientInfoModelList.Add(new ClientInfoModel
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

        public ICommand UpdateCommand { get; }

        public void UpdateMethod()
        {
            ClientInfoModel.DeletingClient = false;
            ClientInfoModel.UpdatingClient = true;
            ClientInfoModel.CreatingNewClient = false;
            ClientInfoModel.ReadingClient = false;
        }
        public ICommand UpdateSqlCommand { get; }
        public async void UpdateSqlMethod()
        {
            try
            {
                SqlModel.SqlConnection.Open();

                //if (ClientInfoModel.ClientName != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET ClientName = '{ClientInfoModel.ClientName}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}

                //if (ClientInfoModel.ClientEmail != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET ClientEmail = '{ClientInfoModel.ClientEmail}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}


                //if (ClientInfoModel.ClientPhoneNumber != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET ClientPhoneNumber = '{ClientInfoModel.ClientPhoneNumber}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}

                //if (ClientInfoModel.ClientAddress != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET ClientAddress = '{ClientInfoModel.ClientAddress}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}

                //if (ClientInfoModel.ClientMainContactName != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET ClientMainContactName = '{ClientInfoModel.ClientMainContactName}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}

                //if (ClientInfoModel.ClientMainContactPhoneNumber != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET ClientMainContactPhoneNumber = '{ClientInfoModel.ClientMainContactPhoneNumber}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}

                //if (ClientInfoModel.PricePerHour.ToString() != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET PricePerHour = '{ClientInfoModel.PricePerHour}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}

                //if (ClientInfoModel.NumberOfHoursBilledToDate.ToString() != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET NumberOfHoursBilledToDate = '{ClientInfoModel.NumberOfHoursBilledToDate}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}

                //if (ClientInfoModel.TotalAmountBilledToDate.ToString() != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET TotalAmountBilledToDate = '{ClientInfoModel.TotalAmountBilledToDate}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}

                //if (ClientInfoModel.TotalPaidToDate.ToString() != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET TotalPaidToDate = '{ClientInfoModel.TotalPaidToDate}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}

                //if (ClientInfoModel.BalanceDue.ToString() != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET BalanceDue = '{ClientInfoModel.BalanceDue}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}

                //if (ClientInfoModel.ClientSince.ToString() != string.Empty)
                //{
                //    string queryString1 = $"UPDATE dbo.ClientInfoTable SET ClientSince = '{ClientInfoModel.ClientSince}'  WHERE Identifier ='{ClientInfoModel.Identifier}'";

                //    using (SqlCommand command = new SqlCommand(queryString1, SqlModel.SqlConnection))
                //    {
                //        command.ExecuteNonQuery();

                //    }

                //}

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

        public ICommand DeleteCommand { get; }

        public void DeleteMethod()
        {
            ClientInfoModel.DeletingClient = true;
            ClientInfoModel.UpdatingClient = false;
            ClientInfoModel.CreatingNewClient = false;
            ClientInfoModel.ReadingClient = false;
        }

        public ICommand DeleteSqlCommand { get; }
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


        public ICommand GetClientAndCasePickersCommand { get; }
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


        
    }
}
