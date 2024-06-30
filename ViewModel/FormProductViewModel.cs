using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestUploadFiles.Attributes;

namespace TestUploadFiles.ViewModel
{
    public class FormProductViewModel
    {
        public int Id { get; set; } //for edit
        [Required]
        public string Name { get; set; } // for edit and create
        [Required]
        public string Description { get; set; } // for edit and create
        [MaxFileSize(Settings.maxFileSizeImg)]
        [AllowedExtensions(Settings.allowedExtensions)]
        public List<IFormFile>? FormFiles { get; set; } // for create
        public IEnumerable<string>? Images { get; set; } = Enumerable.Empty<string>(); // for edit
    }
}
