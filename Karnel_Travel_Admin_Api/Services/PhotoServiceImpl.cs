using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class PhotoServiceImpl : IPhotoService
{
    private Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
    public PhotoServiceImpl(DatabaseContext _db)
    {
        cloudinary.Api.Secure = true;
    }

    public List<string> AddListPhoto(List<IFormFile> files)
    {
        List<string> urls = new List<string>();
        foreach (IFormFile file in files)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };
                uploadResult = cloudinary.Upload(uploadParams);
            }
            urls.Add(uploadResult.Url+"");
        }
        return urls;
        
    }

    public ImageUploadResult AddPhoto(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream)
            };
            uploadResult = cloudinary.Upload(uploadParams);
        }
        return uploadResult;
    }

    public DeletionResult DeletePhoto(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = cloudinary.Destroy(deleteParams);
        return result;
    }
}
