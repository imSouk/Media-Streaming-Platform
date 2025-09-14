
using MediaStreamingPlatform_API.interfaces;

namespace MediaStreamingPlatform_API
{
    public class MediaFileService : IMediaFileService
    {
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly IMediaFileTypeValidator _mediaFileTypeValidator;
        public MediaFileService(IMediaFileRepository mediaFileRepository, IMediaFileTypeValidator mediaFileTypeValidator)
        {
            _mediaFileRepository = mediaFileRepository;
            _mediaFileTypeValidator = mediaFileTypeValidator;
            
        }
        public Task MapAndSaveMediaFile(IFormFile formFile, string path)
        {
           
            try
            {
                MediaFile file = new MediaFile();
                file.FileName = formFile.FileName;
                file.FilePath = path;
                file.ContentType = formFile.ContentType;
                file.FileSize = formFile.Length;
                if (formFile.ContentType != null)
                {
                    file.Type = _mediaFileTypeValidator.GetMediaType(formFile.ContentType);
                }
                 _mediaFileRepository.AddMediaFile(file);
            }
            catch (Exception e) 
            {
                return Task.FromResult(e);
            }
            return Task.CompletedTask;
        }
    }
}
