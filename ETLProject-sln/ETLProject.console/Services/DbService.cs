using System.Data;
using System.Data.SqlClient;
using ETLProject.console.Models;
namespace ETLProject.console.Services;

public class DbService
{
    
    /// function for import data from file to our db
    public void ImportDataTodDb(string connectionString, List<DbTripTransport> dbTripTransports)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = "Trips";
                bulkCopy.WriteToServer(ToDataTable(dbTripTransports));
            }

            using (var command = new SqlCommand("SELECT COUNT(*) FROM Trips", connection))
            {
                var rowCount = (int)command.ExecuteScalar();
                Console.WriteLine($"Кількість записів у таблиці Trips: {rowCount}");
            }
        }
    }
    
    
    
    private static DataTable ToDataTable(List<DbTripTransport> records)
            {
                var table = new DataTable();
                table.Columns.Add("PickupDatetime", typeof(DateTime));
                table.Columns.Add("DropoffDatetime", typeof(DateTime));
                table.Columns.Add("PassengerCount", typeof(int));
                table.Columns.Add("TripDistance", typeof(decimal));
                table.Columns.Add("StoreAndFwdFlag", typeof(string));
                table.Columns.Add("PULocationID", typeof(int));
                table.Columns.Add("DOLocationID", typeof(int));
                table.Columns.Add("FareAmount", typeof(decimal));
                table.Columns.Add("TipAmount", typeof(decimal));
    
                foreach (var record in records)
                {
                    table.Rows.Add(
                        record.PickupDatetimeUtc,
                        record.DropoffDatetimeUtc,
                        record.PassengerCount,
                        record.TripDistance,
                        record.StoreAndFwdFlag,
                        record.PULocationID,
                        record.DOLocationID,
                        record.FareAmount,
                        record.TipAmount
                    );
                }
                return table;
            }
}