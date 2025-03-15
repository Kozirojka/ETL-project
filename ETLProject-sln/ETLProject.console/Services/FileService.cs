using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace ETLProject.console.Services;

public class FileService : IFileService
{
    public void SaveToCsv<T>(List<T> content, string fileName)
    {
        try
        {
            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(content);
            }
            Console.WriteLine($"CSV file saved: {fileName}. Total records: {content.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving CSV: {ex.Message}");
        }
    }
}