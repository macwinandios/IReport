using IReport.Models;
using IReport.Models.Base;
using IReport.Views;
using Reports.Locator;
using System;
using System.Data.SqlClient;
using System.Windows.Input;
using Xamarin.Forms;

namespace IReport.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        public LoginViewModel(LoginModel loginModel)
        {
            LoginModel = loginModel;

            //LoginCommand = new Command(LoginMethod);
            //GuestLoginCommand = new Command(GuestLoginMethod);
            //SignUpCreateSqlCommand = new Command(SignUpCreateSqlMethod);
        }


        //private members for each command
        //the public properties for these private members are at the end of this class to avoid clutter
        ICommand _loginCommand;
        ICommand _guestLoginCommand;
        ICommand _signUpCreateSqlCommand;



        //PUBLIC PROPERTY BEGINS
        LoginModel _loginModel;
        public LoginModel LoginModel
        {
            get => _loginModel;
            set => SetProperty(ref _loginModel, value);
        }


        //METHOD FOR GUESTLOGIN BUTTON-PUSHASYNC TO BYPASS LOGIN CREDENTIALS
        public async void GuestLoginMethod()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TabAccessor());
        }

        //LOGIN METHOD READS FROM SQL. ONLY IF USERNAME IS IN DATABASE ACCESS IS GRANTED.
        public async void LoginMethod()
        {
            try
            {

                SqlModel.SqlConnection.Open();
                if (LoginModel.Username == LoginModel.AdminUsername && LoginModel.Password == LoginModel.AdminPassword)
                {
                    await Application.Current.MainPage.DisplayAlert($"Welcome, {LoginModel.Username}", "For support call 800 Support 24/7!", "I WILL");
                    await Application.Current.MainPage.Navigation.PushAsync(new TabAccessor());
                    LoginModel.Username = string.Empty;
                    LoginModel.Password = string.Empty;
                }

                if (LoginModel.Username != string.Empty)
                {
                    SqlCommand sqlSaveAndReadCommand = new SqlCommand("SELECT * FROM dbo.LoginInfoTable WHERE EmployeeUsername = '" + LoginModel.Username + "' AND EmployeePassword = '" + LoginModel.Password + "'", SqlModel.SqlConnection);



                    SqlModel.SqlDataReader = sqlSaveAndReadCommand.ExecuteReader();

                    if (SqlModel.SqlDataReader.Read())
                    {
                        SqlModel.SqlConnection.Close();

                        await Application.Current.MainPage.DisplayAlert($"Welcome, {LoginModel.Username}", "We hope you enjoy your experience with us!", "I WILL");

                        var reportInfoView = ViewModelLocator.Resolve<ReportInfoView>();

                        await Application.Current.MainPage.Navigation.PushAsync(reportInfoView);
                        LoginModel.Username = string.Empty;
                        LoginModel.Password = string.Empty;
                    }
                    else
                    {
                        SqlModel.SqlDataReader.Read();
                        SqlModel.SqlConnection.Close();

                        await Application.Current.MainPage.DisplayAlert("Wrong Username Or Password", "Try Again", "OK");
                        LoginModel.Username = string.Empty;
                        LoginModel.Password = string.Empty;
                    }
                    SqlModel.SqlConnection.Close();


                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");
                LoginModel.Username = string.Empty;
                LoginModel.Password = string.Empty;

            }
            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }

        //SIGNUP METHOD.  READS USERNAMES FROM SQL.  IF USERNAME ENTERED DOES NOT EXIST, IT POSTS TO SQL AND CREATES (INSERTS) THE ENTERED USERNAME. 
        public async void SignUpCreateSqlMethod()
        {

            try
            {
                SqlModel.SqlConnection.Open();
                if (LoginModel.Username != string.Empty && LoginModel.Password != string.Empty)
                {
                    SqlCommand sqlSaveAndReadCommand = new SqlCommand("SELECT * FROM dbo.LoginInfoTable WHERE EmployeeUsername = '" + LoginModel.Username + "' AND EmployeePassword = '" + LoginModel.Password + "'", SqlModel.SqlConnection);

                    SqlModel.SqlDataReader = sqlSaveAndReadCommand.ExecuteReader();

                    if (SqlModel.SqlDataReader.Read())
                    {
                        SqlModel.SqlConnection.Close();

                        await Application.Current.MainPage.DisplayAlert("NOT VALID", "THIS USERNAME OR PASSWORD IS ALREADY IN THE DATABASE", "OK");
                        LoginModel.Username = string.Empty;
                        LoginModel.Password = string.Empty;
                    }
                    else
                    {
                        using (SqlCommand sqlInsertCommand = new SqlCommand("INSERT INTO dbo.LoginInfoTable VALUES (@EmployeeUsername, @EmployeePassword)", SqlModel.SqlConnection))
                        {
                            sqlInsertCommand.Parameters.Add(new SqlParameter("EmployeeUsername", LoginModel.Username));
                            sqlInsertCommand.Parameters.Add(new SqlParameter("EmployeePassword", SqlModel.HashedString(LoginModel.Password)));

                            SqlModel.SqlDataReader.Close();
                            sqlInsertCommand.ExecuteNonQuery();

                            await Application.Current.MainPage.DisplayAlert("SUCCESSFULLY ADDED", "CLICK OK TO CONTINUE", "OK");
                            LoginModel.Username = string.Empty;
                            LoginModel.Password = string.Empty;
                        }

                    }

                    SqlModel.SqlConnection.Close();

                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("NOT YET", ex.Message, "OK");
                LoginModel.Username = string.Empty;
                LoginModel.Password = string.Empty;
            }

            finally
            {
                SqlModel.SqlConnection.Close();

            }
        }


        //public properties of type ICommand that create an instance of the Command class and calls it's shared-named method
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new Command(LoginMethod));
        public ICommand GuestLoginCommand => _guestLoginCommand ?? (_guestLoginCommand = new Command(GuestLoginMethod));
        public ICommand SignUpCreateSqlCommand => _signUpCreateSqlCommand ?? (_signUpCreateSqlCommand = new Command(SignUpCreateSqlMethod));
    }
}
