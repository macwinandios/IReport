using IReport.Models.Base;
using IReport.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace IReport.Models
{
    public class LoginModel : ViewModelBase
    {
        string _adminUsername; 
        public string AdminUsername
        {
            get { return _adminUsername = "admin"; } 
            set
            {
                _adminUsername = value;
                OnPropertyChanged(AdminUsername);
            }
        } 

        string _adminPassword;
        public string AdminPassword
        {
            get { return _adminPassword = "123"; }
            set
            {
                _adminPassword = value;
                OnPropertyChanged(AdminPassword);

            }
        }

        string _employeeUsername;
        public string EmployeeUsername
        {
            get { return _employeeUsername; }
            set
            {
                _employeeUsername = value;
                OnPropertyChanged(EmployeeUsername);
            }
        }

        string _employeePassword;
        public string EmployeePassword
        {
            get { return _employeePassword; }
            set
            {
                _employeePassword = value;
                OnPropertyChanged(EmployeePassword);

            }
        }

        string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(Username);
            }
        }

        string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(Password);
            }
        }
    }
}
