using ETLProject.console.Exstention;
using ETLProject.console.Interfaces;
using ETLProject.console.Services;

namespace ETLProject.console.Manager;

/// MenuManager for implementation possibility to select option.
public class MenuManager
{
    private readonly IDbService _dbService;
    private readonly ICsvService _csvService;
    private readonly IFileService _fileService;
    private readonly string _pathToFile;
    private readonly string _connectionString;
    public MenuManager(IDbService dbService, ICsvService csvService, IFileService fileService, string pathToFile, string connectionString)
    {
        _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
        _csvService = csvService ?? throw new ArgumentNullException(nameof(csvService));
        _pathToFile = pathToFile ?? throw new ArgumentNullException(nameof(pathToFile));
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    }

    public void Run()
    {
        bool exit = false;
        while (!exit)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ImportDataFromCsv();
                    break;

                case "2":
                    TruncateTable();
                    break;

                case "0":
                    Console.WriteLine("Exit");
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select 1, 2, or 0.");
                    break;
            }

            if (!exit)
            {
                Console.WriteLine("\nPlease press Enter to return to the menu");
                Console.ReadLine();
            }
        }
    }

    private void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Console App ===");
        Console.WriteLine("1. Read CSV and import data into the database");
        Console.WriteLine("2. Truncate table");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
    }

    private void ImportDataFromCsv()
    {
        Console.WriteLine("Loading data from CSV and importing into the database...");
        try
        {
            
            //It's extension methods for List<DbTrioTransport> look interesting and great 
            /*   ╱|、
                (˚ˎ 。7  
                |、˜〵          
                じしˍ,)ノ
             */

            var list = _csvService.ReadCsvFile(_pathToFile);

            var duplicateList = list.FindDuplicates();
            var uniqueList = list.RemoveDuplicates();
            

            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(projectPath, "duplicates.csv");

            _fileService.SaveToCsv(duplicateList, filePath);
            
            _dbService.ImportDataTodDb(_connectionString, uniqueList);
            
            
            Console.WriteLine("Data successfully imported!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during data import: {ex.Message}");
        }
    }

    private void TruncateTable()
    {
        Console.WriteLine("Truncating the Trips table...");
        try
        {
            _dbService.TruncateTable(_connectionString, "Trips");
            Console.WriteLine("Table successfully truncated!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while truncating the table: {ex.Message}");
        }
    }
}