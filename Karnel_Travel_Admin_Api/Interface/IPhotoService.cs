using CloudinaryDotNet.Actions;

namespace Karnel_Travel_Admin_Api.Interface;

public interface IPhotoService
{
    public ImageUploadResult AddPhoto(IFormFile file);
    public List<string> AddListPhoto(List<IFormFile> files);
    public DeletionResult DeletePhoto(string publicId);
}
