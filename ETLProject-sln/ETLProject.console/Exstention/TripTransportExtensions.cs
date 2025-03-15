using ETLProject.console.Models;

namespace ETLProject.console.Exstention;

/// Extenstion class for List of DbTripTransport
public static class TripTransportExtensions
{
    public static List<DbTripTransport> RemoveDuplicates(this List<DbTripTransport> listOfTaxis)
    {
        var uniqueTrips = listOfTaxis
            .GroupBy(x => new { x.PickupDatetimeUtc, x.DropoffDatetimeUtc, x.PassengerCount })
            .Where(g => g.Count() == 1)
            .SelectMany(g => g)
            .ToList();

        return uniqueTrips;
    }

    public static List<DbTripTransport> FindDuplicates(this List<DbTripTransport> listOfTaxis)
    {
        var duplicates = listOfTaxis
            .GroupBy(x => new
            {
                x.PickupDatetimeUtc,
                x.DropoffDatetimeUtc,
                x.PassengerCount
            })
            .Where(g => g.Count() > 1)
            .SelectMany(g => g.Skip(1))
            .ToList();

        return duplicates;
    }
}