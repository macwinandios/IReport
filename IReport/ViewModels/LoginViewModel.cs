using IReport.Models;
using IReport.Models.Base;
using IReport.Views;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using IReport.Services;

namespace IReport.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        public LoginViewModel()
        {
            //BEFORE CREATING AN INSTANCE OF THE CLASS USING THE PROPERTY,
            //IT WAS CRASHING SAYING OBJECT OF CLASS NOT CREATED OR SOMETHING
            
            LoginModel = new LoginModel();
            LoginCommand = new Command(LoginMethod);
            GuestLoginCommand = new Command(GuestLoginMethod);
            SignUpCreateSqlCommand = new Command(SignUpCreateSqlMethod);
        }

        LoginModel _loginModel;
        public LoginModel LoginModel
        {
            get => _loginModel;
            set
            {
                _loginModel = value;
            }
        }

        public ICommand GuestLoginCommand { get; }

        public async void GuestLoginMethod()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TabAccessor());
        }

        public ICommand LoginCommand { get; }
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
                        await Application.Current.MainPage.Navigation.PushAsync(new ReportInfoView());
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

            }//END OF TRY BLOCK
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
        public ICommand SignUpCreateSqlCommand { get; }
        public async void SignUpCreateSqlMethod()
        {
            //posts to sql
            try
            {
                SqlModel.SqlConnection.Open();
                if (LoginModel.Username != string.Empty && LoginModel.Password != string.Empty)
                {
                    SqlCommand sqlSaveAndReadCommand = new SqlCommand("SELECT * FROM dbo.LoginInfoTable WHERE EmployeeUsername = '" + LoginModel.Username + "' AND EmployeePassword = '"+LoginModel.Password+"'", SqlModel.SqlConnection);

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
                        }//END OF USING

                    }//END OF ELSE

                    SqlModel.SqlConnection.Close();

                }//END OF FIRST IF

            }//END OF TRY BLOCK
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

    }
}
