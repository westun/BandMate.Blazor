namespace BandMate.Shared.Security
{
    public interface IFileUploadValidator
    {
        bool IsExtensionAllowed(string fileName);
    }
}
