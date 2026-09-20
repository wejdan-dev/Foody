namespace Front.Services
{
    public interface IProjectImageService
    {
        IReadOnlyList<string> GetAvailableImageFileNames(string? selectedFileName = null);
    }
}
