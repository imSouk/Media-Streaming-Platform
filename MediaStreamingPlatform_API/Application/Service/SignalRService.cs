using Microsoft.AspNetCore.SignalR;

public class SignalRService
{
    private readonly IHubContext<MediaHub> _hubContext;

    public SignalRService(IHubContext<MediaHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyPlaylistStart(int playlistId)
    {
        await _hubContext.Clients.All.SendAsync("PlaylistStarted", new
        {
            PlaylistId = playlistId,
            Action = "START_PLAYLIST"
        });
    }

    public async Task NotifyPlaylistStop()
    {
        await _hubContext.Clients.All.SendAsync("PlaylistStopped", new
        {
            Action = "STOP_PLAYLIST"
        });
    }
}