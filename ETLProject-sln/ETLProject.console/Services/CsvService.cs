using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ETLProject.console.Models;

namespace ETLProject.console.Services;

//in this moment i understand that there is white spaces in this 18807 line
public class CsvService
{

    public List<DbTripTransport> ReadCsvFile(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            TrimOptions = TrimOptions.Trim,
            MissingFieldFound = null, 
            BadDataFound = context => Console.WriteLine($"Bad data at row {context.Field}: {context.RawRecord}")
        };

        var toInsert = new List<DbTripTransport>();
        var estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        
        
        using (var reader = new StreamReader(filePath))

        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<TripRecord>().ToList();
            Console.WriteLine($"Read amount: {records.Count}");

            int count = 0;
            
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
                    PassengerCount = string.IsNullOrEmpty(record.passenger_count) ? 0 : int.Parse(record.passenger_count),                    TripDistance = decimal.Parse(record.trip_distance),
                    StoreAndFwdFlag = string.IsNullOrEmpty(record.store_and_fwd_flag) ? "No" : record.store_and_fwd_flag.ToUpper() == "Y" ? "Yes" : "No",
                    PULocationID = int.Parse(record.PULocationID),
                    DOLocationID = int.Parse(record.DOLocationID),
                    FareAmount = decimal.Parse(record.fare_amount),
                    TipAmount = decimal.Parse(record.tip_amount)
                };
                
                toInsert.Add(dbRecord);
                count++;

                if (count > 19000)
                {
                    return toInsert;
                }
            }

        }

        return toInsert;
    }
}