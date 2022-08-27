using System;
using System.Collections.Generic;
using System.Text;

namespace IReport.Services
{
    public  interface ILoginModel
    {
        string AdminUsername { get; set; }
        string AdminPassword { get; set; }
        string EmployeeUsername { get; set; }
        string EmployeePassword { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
