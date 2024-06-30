using System.ComponentModel.DataAnnotations;

namespace TestUploadFiles.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        public MaxFileSizeAttribute(int maxFileSize)
        {

            _maxFileSize = maxFileSize;

        }
        private readonly int _maxFileSize;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file != null)
            {
                if(file.Length > _maxFileSize)
                {
                    return new ValidationResult(errorMessage: $"Max file size allowed is {_maxFileSize}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
