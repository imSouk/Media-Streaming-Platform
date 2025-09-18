using MediaStreamingPlatform_API.Application.DTOs;
using MediaStreamingPlatform_API.Domain.interfaces;
using System.Collections.Generic;

namespace MediaStreamingPlatform_API.Application.UseCases
{
    public class MediaFileService : IMediaFileService
    {
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly IMediaFileTypeValidator _mediaFileTypeValidator;
        private readonly IPlaylistFileService _playlistFileService;
        public MediaFileService(IMediaFileRepository mediaFileRepository, IMediaFileTypeValidator mediaFileTypeValidator, IPlaylistFileService playlistFileService)
        {
            _mediaFileRepository = mediaFileRepository;
            _mediaFileTypeValidator = mediaFileTypeValidator;
            _playlistFileService = playlistFileService;
        }
        public async Task<string> MapAndSaveMediaFile(IFormFile formFile, byte[] fileContent, int playlistId)
        {

            MediaFile file = new MediaFile
            {
                FileName = formFile.FileName,
                FileContent = fileContent,
                ContentType = formFile.ContentType,
                FileSize = formFile.Length,
                Type = formFile.ContentType != null ? _mediaFileTypeValidator.GetMediaType(formFile.ContentType) : 0
            };

            _mediaFileRepository.AddMediaFile(file);
            await _playlistFileService.AddPlaylistFile(file, playlistId);
            await _mediaFileRepository.SaveAsync();
            return $"File {file.FileName} save in db with ID = {file.Id}";
        }
        public async Task<string> DeleteMediaFIleById(int id)
        {
           var media = await _mediaFileRepository.GetMediaFileById(id);
            if (media == null)
                return $"Media not found";
            
            _mediaFileRepository.DeleteMediaFile(media);
            await _mediaFileRepository.SaveAsync();
            return $"Delete Media {media.FileName} with ID = {id}";
        }

        public async Task<List<MediaFileDto>> GetAllMediaFilesDtos()
        {
            List<MediaFile> mediaFiles = await _mediaFileRepository.GetAllMediaFiles();
            List<MediaFileDto> mediaFilesDtoList = mediaFiles.Select(m => new MediaFileDto
            {
                Id = m.Id,
                FileName = m.FileName,
                ContentType = m.ContentType,
                FileSize = m.FileSize,
                Type = m.Type,
                UploadedAt = m.UploadedAt
            }).ToList();
            return mediaFilesDtoList;
        }

        public async Task<List<MediaFileBlobDto>> GetBlobById(int[] ids)
        {
            List<MediaFileBlobDto> list = new List<MediaFileBlobDto> ();
            foreach (int id in ids) {            
                MediaFile file = await _mediaFileRepository.GetMediaFileById(id);
                MediaFileBlobDto blob = new MediaFileBlobDto();
                if (file == null)
                    return null;
                list.Add(new MediaFileBlobDto
                {
                    FileName = file.FileName,
                    FileContent = file.FileContent,
                    ContentType = file.ContentType
                });
            }
            return list.Count > 0 ? list : null;
        }

        public async Task<string> DeleteMediaFIleByName(string fileName, int playlistId)
        {
            var media = await _mediaFileRepository.GetMediaFileByName(fileName);
            if (media == null)
                return $"Media not found";

            _mediaFileRepository.DeleteMediaFile(media);
            await _playlistFileService.RemovePlaylistFile(media, playlistId);
            await _mediaFileRepository.SaveAsync();
            return $"Delete Media {media.FileName} with ID = {media.Id} int the playlist with id = {playlistId}";
        }
    }
}
