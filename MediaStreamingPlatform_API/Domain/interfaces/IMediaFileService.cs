using MediaStreamingPlatform_API.Application.DTOs;

namespace MediaStreamingPlatform_API.Domain.interfaces
{
    public interface IMediaFileService
    {
        public Task MapAndSaveMediaFile(IFormFile formFile, byte[] fileContent);
        public Task<string> DeleteMediaFIleById(int id);
        public Task<List<MediaFileDto>> GetAllMediaFilesDtos();
        public Task<List<MediaFileBlobDto>> GetBlobById(int[] ids);
    }
}
