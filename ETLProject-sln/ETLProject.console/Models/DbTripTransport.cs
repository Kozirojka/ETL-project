namespace ETLProject.console.Models;


// this class exist for mapping data from TripRecord class to Data base form
public class DbTripTransport
{
    public DateTime PickupDatetimeUtc { get; set; }
    public DateTime DropoffDatetimeUtc { get; set; }
    public int PassengerCount { get; set; }
    public decimal TripDistance { get; set; }
    public string StoreAndFwdFlag { get; set; }
    public int PULocationID { get; set; }
    public int DOLocationID { get; set; }
    public decimal FareAmount { get; set; }
    public decimal TipAmount { get; set; }

    public override string ToString()
    {
        return $"DbTripTransport {{ " +
               $"PickupDatetimeUtc: {PickupDatetimeUtc:yyyy-MM-dd HH:mm:ss}, " +
               $"DropoffDatetimeUtc: {DropoffDatetimeUtc:yyyy-MM-dd HH:mm:ss}, " +
               $"PassengerCount: {PassengerCount}, " +
               $"TripDistance: {TripDistance:F2} miles, " +
               $"StoreAndFwdFlag: {StoreAndFwdFlag}, " +
               $"PULocationID: {PULocationID}, " +
               $"DOLocationID: {DOLocationID}, " +
               $"FareAmount: ${FareAmount:F2}, " +
               $"TipAmount: ${TipAmount:F2} }}";
    }
}