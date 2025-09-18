using MediaStreamingPlatform_API.Application.DTOs;
using MediaStreamingPlatform_API.Domain.Entities;
using MediaStreamingPlatform_API.Domain.interfaces;


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
            if (playlistDto == null)
                return "Cannot create playlist, check all of the needed parameters";
            var playlist = new MediaPlaylist
            {
                PlaylistName = playlistDto.PlaylistName
            };
            _mediaPlaylistRepository.CreatePlaylist(playlist);
            await _mediaPlaylistRepository.SaveAsync();
            return $"Playlist created {playlist.PlaylistName}";
        }
        public async Task<string> DeletePlaylist(int id)
        {
            var playlist = await _mediaPlaylistRepository.GetPlaylistByIdAsync(id);
            if (playlist == null)
                return $"Playlist with id {id} not found";

            _mediaPlaylistRepository.DeletePlaylist(playlist);
            await _mediaPlaylistRepository.SaveAsync();
            return $"Playlist deleted {playlist.Id} {playlist.PlaylistName}";

        }
        public async Task<MediaPlaylistDto> GetPlaylistItemsByIdAsync(int id)
        {
            return await _mediaPlaylistRepository.GetPlaylistItems(id);
        }
        public async Task<List<MediaPlaylistDto>> GetAllPlaylists()
        {
            return await _mediaPlaylistRepository.GetAllPlaylistsWithFiles();
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
