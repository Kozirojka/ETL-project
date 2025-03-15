// See https://aka.ms/new-console-template for more information

using ETLProject.console.Manager;
using ETLProject.console.Services;

namespace ETLProject.console;

class Program
{
    private const string PathToFile = @"D:\development\assignmentTest\ETLProject-sln\sample-cab-data.csv";
    private const string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=taxiDb;Trusted_Connection=True;";
    
    
    static void Main(string[] args)
    {
        DbService dbService = new DbService();
        CsvService csvService = new CsvService();
        FileService fileService = new FileService();
        
        MenuManager menuManager = new MenuManager(dbService, csvService, fileService, PathToFile, ConnectionString);
        
        menuManager.Run();
    }
}