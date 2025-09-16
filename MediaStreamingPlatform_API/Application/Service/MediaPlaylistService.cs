using MediaStreamingPlatform_API.Application.DTOs;
using MediaStreamingPlatform_API.Domain.Entities;
using MediaStreamingPlatform_API.Domain.interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Runtime.InteropServices.Marshalling;

namespace MediaStreamingPlatform_API.Application.Service
{
    public class MediaPlaylistService : IMediaPlaylistService
    {
        private readonly SignalRService _signalRService;
        private readonly IMediaPlaylistRepository _mediaPlaylistRepository;
        public MediaPlaylistService(IMediaPlaylistRepository repository, SignalRService signalR )
        {
            _mediaPlaylistRepository = repository;
            _signalRService = signalR;
        }
        public async Task<string> CreateAndSavePlaylist(MediaPlaylistCreateDto playlistDto)
        {
            if (playlistDto != null)
            {
                MediaPlaylist playlist = new MediaPlaylist();
                playlist.PlaylistName = playlistDto.PlaylistName;
                _mediaPlaylistRepository.CreatePlaylist(playlist);
                await _mediaPlaylistRepository.SaveAsync();
                return $"Playlist created {playlist.PlaylistName}";
            }
            return $"Cant create ur playlist, check all of the needed parameters";
        }
        public async Task<string> DeletePlaylist(int id)
        {
                MediaPlaylist playlist = await _mediaPlaylistRepository.GetPlaylistByIdAsync(id);
                if (playlist != null)
                {
                    _mediaPlaylistRepository.DeletePlaylist(playlist);
                    await _mediaPlaylistRepository.SaveAsync();
                    return $"Playlist delete {playlist.Id} {playlist.PlaylistName}";
                }
                return $"Playlist with id {id} not found";
           
        }
        public async Task<MediaPlaylistDto> GetPlaylistItemsByIdAsync(int id)
        {
            MediaPlaylistDto playlistDto = await _mediaPlaylistRepository.GetPlaylistItems(id);
            return playlistDto;  
        }
        public async Task<List<MediaPlaylistDto>> GetAllPlaylists()
        {
            List<MediaPlaylistDto> playlist = await _mediaPlaylistRepository.GetAllPlaylistsWithFiles();
            return playlist;
        }
        public async Task<string> SendPlayCommand(int id)
        {
            try
            {
                await _signalRService.NotifyPlaylistStart(id);
                return $"start command sended";
            }
            catch (Exception ex) 
            {
                return $"cant send the command {ex}";
            }
             
        }

        public Task<MediaPlaylist> GetPlaylistByIdAsync(int id)
        {
            return _mediaPlaylistRepository.GetPlaylistByIdAsync( id);
        }
    }
}
