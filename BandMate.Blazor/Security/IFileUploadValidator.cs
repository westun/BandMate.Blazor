namespace BandMate.Blazor.Security
{
    public interface IFileUploadValidator
    {
        bool IsExtensionAllowed(string fileName);
    }
}
