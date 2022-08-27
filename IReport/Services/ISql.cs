using IReport.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IReport.Services
{
    public interface ISql
    {
        void CreateSqlMethod();
        void ReadSqlMethod();
        void UpdateSqlMethod();
        void DeleteSqlMethod();
        void GetClientAndCasePickersMethod();

    }
}
