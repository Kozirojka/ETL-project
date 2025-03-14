using System.Data;
using ETLProject.console.Models;

namespace ETLProject.console.Services;

public interface IDbService
{
    void ImportDataTodDb(string connectionString, List<DbTripTransport> dbTripTransports);
    void TruncateTable(string connectionString, string name); 
    DataTable ToDataTable(List<DbTripTransport> records);
}