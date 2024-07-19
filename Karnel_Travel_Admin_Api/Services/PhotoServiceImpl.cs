using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;
using System.Net;
using System.Reflection.Metadata;

namespace Karnel_Travel_Admin_Api.Services;

public class PhotoServiceImpl : IPhotoService
{
    private DatabaseContext db;
    private Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
    public PhotoServiceImpl(DatabaseContext _db)
    {
        cloudinary.Api.Secure = true;
        db = _db;
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
            if (uploadResult.StatusCode == HttpStatusCode.OK)
            {
                urls.Add(uploadResult.Url + "");
            }
            else {
                urls.Add("");
            }
            
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

    public bool addPhotoRecords(List<Photo> photos)
    {
        using var transaction = db.Database.BeginTransaction();

        try
        {
            foreach (var photo in photos) {
                db.Photos.Add(photo);
                db.SaveChanges();
            }
            
            transaction.Commit();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public DeletionResult DeletePhoto(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = cloudinary.Destroy(deleteParams);
        return result;
    }
}
