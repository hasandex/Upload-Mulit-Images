namespace TestUploadFiles.ViewModel
{
    public class ImagesViewModel
    {
        public int Id { get; set; }
        [MaxFileSize(Settings.maxFileSizeImg)]
        [AllowedExtensions(Settings.allowedExtensions)]
        public List<IFormFile>? FormFiles { get; set; }
        public IEnumerable<string>? Images { get; set; } = Enumerable.Empty<string>();
    }
}
