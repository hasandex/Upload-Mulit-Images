using System.ComponentModel.DataAnnotations.Schema;


namespace TestUploadFiles.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductImages> ProductImages { get; set; }
    }
}
