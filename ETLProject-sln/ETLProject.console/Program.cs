// See https://aka.ms/new-console-template for more information

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ETLProject.console.Models;
using ETLProject.console.Services;

namespace ETLProject.console;

class Program
{
    private const string ConnectionString = @"D:\development\assignmentTest\ETLProject-sln\sample-cab-data.csv";

    static void Main(string[] args)
    {
        DbService dbService = new DbService();
        CsvService csvService = new CsvService();
        
        var list = csvService.ReadCsvFile(ConnectionString);
        foreach (var VARIABLE in list)
        {
            Console.WriteLine(VARIABLE.ToString());
        }
    }
}