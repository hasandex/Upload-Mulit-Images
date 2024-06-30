using TestUploadFiles.Models;
using TestUploadFiles.ViewModel;

namespace TestUploadFiles.Repo
{
    public interface IUploadFiles
    {
        IEnumerable<Product> GetAll();
        int Create(FormProductViewModel product);
        int Update(FormProductViewModel product);
        int Delete(int productId , string image);
        IEnumerable<ProductImages> GetAllImages(int productId);
        int UpdateImages(ImagesViewModel product);
        Product GetById(int productId);
    }
}
