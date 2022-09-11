using IReport.Library;
using Xunit;

namespace IReport.Test
{
    public class IReportTest
    {
        [Fact]

        public void AssertAllSqlMethods()
        {
           // int i = 1;
           // Assert.Equal(1, i);
           Assert.Equal(3,MockSql.CreateSqlMethod());
           Assert.Equal(3,MockSql.ReadSqlMethod());
           Assert.Equal(3,MockSql.UpdateSqlMethod());
           Assert.Equal(3,MockSql.DeleteSqlMethod());
           Assert.Equal(3,MockSql.GetClientAndCasePickersMethod());

        }
    }
}