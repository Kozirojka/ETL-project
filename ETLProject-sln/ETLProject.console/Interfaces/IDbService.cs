using System.Data;
using System.Data.SqlClient;
using ETLProject.console.Models;

namespace ETLProject.console.Interfaces;

public interface IDbService
{
    void ImportDataTodDb(string connectionString, List<DbTripTransport> dbTripTransports);
    void TruncateTable(string connectionString, string name);
    DataTable ToDataTable(List<DbTripTransport> records);



}