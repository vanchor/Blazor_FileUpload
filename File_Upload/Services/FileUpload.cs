using Microsoft.AspNetCore.Components.Forms;

namespace File_Upload.Services
{
    public interface IFileUpload
    {
        Task UpLoadFile(IBrowserFile file);
    }
    public class FileUpload : IFileUpload
    {
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileUpload> _logger;

        public FileUpload(IWebHostEnvironment webHostEnvironment, ILogger<FileUpload> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task UpLoadFile(IBrowserFile file)
        {
            if(file is not null)
            {
                try
                {
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", file.Name);
                    using (var stream = file.OpenReadStream())
                    {
                        var fileStram = File.Create(uploadPath);
                        await stream.CopyToAsync(fileStram);
                        fileStram.Close();

                    }

                } 
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
            }
        }
    }
}
