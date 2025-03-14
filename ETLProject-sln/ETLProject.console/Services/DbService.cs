using System.Data;
using System.Data.SqlClient;
using ETLProject.console.Models;
namespace ETLProject.console.Services;

public class DbService : IDbService
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

                bulkCopy.ColumnMappings.Add("PickupDatetime", "PickupDatetime");
                bulkCopy.ColumnMappings.Add("DropoffDatetime", "DropoffDatetime");
                bulkCopy.ColumnMappings.Add("PassengerCount", "PassengerCount");
                bulkCopy.ColumnMappings.Add("TripDistance", "TripDistance");
                bulkCopy.ColumnMappings.Add("StoreAndFwdFlag", "StoreAndFwdFlag");
                bulkCopy.ColumnMappings.Add("PULocationID", "PULocationID");
                bulkCopy.ColumnMappings.Add("DOLocationID", "DOLocationID");
                bulkCopy.ColumnMappings.Add("FareAmount", "FareAmount");
                bulkCopy.ColumnMappings.Add("TipAmount", "TipAmount");
                
                
                var table = ToDataTable(dbTripTransports);
                
                bulkCopy.WriteToServer(table);
            }

            using (var command = new SqlCommand("SELECT COUNT(*) FROM Trips", connection))
            {
                var rowCount = (int)command.ExecuteScalar();
                Console.WriteLine($"Кількість записів у таблиці Trips: {rowCount}");
            }
        }
    }   
    public DataTable ToDataTable(List<DbTripTransport> records)
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

    public void TruncateTable(string connectionString, string name)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand($"TRUNCATE TABLE {name}", connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Таблицю Trips успішно очищено за допомогою TRUNCATE.");
            }
        }
    }
}