using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TestUploadFiles.Data;
using TestUploadFiles.Models;
using TestUploadFiles.ViewModel;

namespace TestUploadFiles.Repo
{
    public class UploadFiles : IUploadFiles
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadFiles(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IEnumerable<Product> GetAll()
        {
            return _appDbContext.Products;
        }
        public int Create(FormProductViewModel viewModel)
        {
            List<ProductImages> productImages = new List<ProductImages>();
            foreach (var item in viewModel.FormFiles)
            {
                var image = new ProductImages { Path = SaveImgInServer(item) };
                productImages.Add(image);
            }

            Product product = new Product()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                ProductImages = productImages
            };
            _appDbContext.Products.Add(product);
            return _appDbContext.SaveChanges();
        }

        public int Update(FormProductViewModel viewModel)
        {
            var existingProduct = GetById(viewModel.Id);

            if (existingProduct == null)
            {
                return 0;
            }
            existingProduct.Name = viewModel.Name;
            existingProduct.Description = viewModel.Description;
            _appDbContext.Products.Update(existingProduct);
            return _appDbContext.SaveChanges();
        }

       
        public Product GetById(int productId)
        {
            var product = _appDbContext.Products?.Include(p => p.ProductImages)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return null;
            return product;
        }

        public int Delete(int productId, string image)
        {
            var product = GetById(productId);
            if(product != null)
            {
                var img = product.ProductImages?.Where(pi => pi.Path == image).SingleOrDefault();
                if(img == null)
                {
                    return -1;
                }
                var path = Path.Combine($"{_webHostEnvironment.WebRootPath}{Settings.imagesPathProducts}", image);
                File.Delete(path);
                _appDbContext.ProductImages.Remove(img);
                return _appDbContext.SaveChanges();
            }
            else
                return -1;
        }

        public IEnumerable<ProductImages> GetAllImages(int productId)
        {
            var product = GetById(productId);
            if(product != null)
            {
                return product.ProductImages?.ToList();
            }
            return null;
        }

        public int UpdateImages(ImagesViewModel viewModel)
        {
            var existingProduct = GetById(viewModel.Id);

            if (existingProduct == null)
            {
                return 0;
            }

            if (viewModel.FormFiles != null)
            {
                List<ProductImages> productImages = new List<ProductImages>();
                foreach (var item in viewModel.FormFiles)
                {
                    var image = new ProductImages { Path = SaveImgInServer(item) };
                    productImages.Add(image);
                }
                existingProduct.ProductImages = productImages;
                _appDbContext.Products.Update(existingProduct);
                return _appDbContext.SaveChanges();
            }
            return 0;
        }

        public string SaveImgInServer(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine($"{_webHostEnvironment.WebRootPath}{Settings.imagesPathProducts}", fileName);
            using var stream = File.Create(path);
            file.CopyToAsync(stream);
            return fileName;
        }
    }
}
