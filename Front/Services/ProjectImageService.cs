using System.IO;

namespace Front.Services
{
    public class ProjectImageService : IProjectImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ProjectImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IReadOnlyList<string> GetAvailableImageFileNames(string? selectedFileName = null)
        {
            var imagesFolder = Path.Combine(_environment.WebRootPath, "ProjectFile", "img");
            if (!Directory.Exists(imagesFolder))
            {
                return Array.Empty<string>();
            }

            var allowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".jpg", ".jpeg", ".png", ".webp", ".gif"
            };

            var result = Directory
                .GetFiles(imagesFolder)
                .Where(path => allowedExtensions.Contains(Path.GetExtension(path)))
                .Select(Path.GetFileName)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Cast<string>()
                .OrderBy(name => name)
                .ToList();

            if (!string.IsNullOrWhiteSpace(selectedFileName) && !result.Contains(selectedFileName, StringComparer.OrdinalIgnoreCase))
            {
                result.Insert(0, selectedFileName);
            }

            return result;
        }
    }
}
