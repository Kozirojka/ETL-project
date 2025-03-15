using ETLProject.console.Models;

namespace ETLProject.console.Interfaces;


public interface ICsvService
{
    List<DbTripTransport> ReadCsvFile(string filePath);
}