using System.ComponentModel.DataAnnotations;

namespace TestUploadFiles.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        public AllowedExtensionsAttribute(string allowedExtensions)
        {

            _allowedExtensions = allowedExtensions;

        }
        private readonly string _allowedExtensions;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (!_allowedExtensions.Split(',').Contains(Path.GetExtension(file.FileName.ToLower())))
                {
                    return new ValidationResult(errorMessage:"file type not allowed!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
