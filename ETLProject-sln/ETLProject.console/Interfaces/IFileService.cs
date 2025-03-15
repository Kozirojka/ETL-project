namespace ETLProject.console.Services;

public interface IFileService
{

    void SaveToCsv<T>(List<T> content, string fileName);
}