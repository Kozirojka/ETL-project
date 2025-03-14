// See https://aka.ms/new-console-template for more information

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ETLProject.console.Models;

namespace ETLProject.console;

class Program
{
    static string ConnectionString = @"D:\development\assignmentTest\ETLProject-sln\sample-cab-data.csv";
    static void Main(string[] args)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            TrimOptions = TrimOptions.Trim
        };
        
        using (var reader = new StreamReader(ConnectionString))
            
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<TripRecord>().ToList();
            Console.WriteLine($"Read amount: {records.Count}");
            
            var toInsert = new List<DbTripTransport>();
            var estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            
            foreach (var record in records)
            {
                var pickupDatetimeEst = DateTime.ParseExact(
                    record.tpep_pickup_datetime,
                    "MM/dd/yyyy hh:mm:ss tt",
                    CultureInfo.InvariantCulture
                );

                var pickupDatetimeUtc = TimeZoneInfo.ConvertTimeToUtc(pickupDatetimeEst, estZone);
                
                var dropoffDatetime = DateTime.ParseExact(
                    record.tpep_dropoff_datetime,
                    "MM/dd/yyyy hh:mm:ss tt",
                    CultureInfo.InvariantCulture
                );
                var dropoffDatetimeUtc = TimeZoneInfo.ConvertTimeToUtc(dropoffDatetime, estZone);
                
                var dbRecord = new DbTripTransport
                {
                    PickupDatetimeUtc = pickupDatetimeUtc,
                    DropoffDatetimeUtc = dropoffDatetimeUtc,
                    PassengerCount = int.Parse(record.passenger_count),
                    TripDistance = decimal.Parse(record.trip_distance),
                    StoreAndFwdFlag = record.store_and_fwd_flag.ToUpper() == "Y" ? "Yes" : "No",
                    PULocationID = int.Parse(record.PULocationID),
                    DOLocationID = int.Parse(record.DOLocationID),
                    FareAmount = decimal.Parse(record.fare_amount),
                    TipAmount = decimal.Parse(record.tip_amount)
                };

                toInsert.Add(dbRecord);
            }
            
            
        }
    }
}