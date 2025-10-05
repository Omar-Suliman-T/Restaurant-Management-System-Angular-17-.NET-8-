using My_Resturant.Entities;

namespace My_Resturant.Helper
{
    public class SaveImages
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SaveImages(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> saveImages(IFormFile image)
        {
            if(image == null || image.Length == 0)
                throw new Exception("No image uploaded.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{image.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            var request = _httpContextAccessor.HttpContext.Request;
            var imageUrl = $"{request.Scheme}://{request.Host}/Uploads/{fileName}";
            return imageUrl;
        }
    }
}
