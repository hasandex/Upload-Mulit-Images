using System.ComponentModel.DataAnnotations.Schema;

namespace TestUploadFiles.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        public string Path { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
