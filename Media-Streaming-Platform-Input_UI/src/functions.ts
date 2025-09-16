export default class MediaService {
  private static readonly API_URL = 'http://localhost:5278';

  private static request = async (endpoint: string, options?: RequestInit) => {
    const response = await fetch(`${this.API_URL}${endpoint}`, options);
    if (!response.ok) return false;
    return response.json();
  };

  private static simpleRequest = async (endpoint: string, options?: RequestInit) => {
    const response = await fetch(`${this.API_URL}${endpoint}`, options);
    return response.ok;
  };

  static uploadFile = async (file: File, playlistId: string) => { 
    const formData = new FormData();  
    formData.append('file', file);  
    formData.append('playlistId', playlistId);
    
    return this.simpleRequest('/save', { method: 'POST', body: formData });  
  };

  static getPlaylists = () => this.request('/GetAllPlaylists');

  static createPlaylist = (playlistName: string) => 
    this.simpleRequest(`/CreatePlaylist?name=${encodeURIComponent(playlistName)}`, { method: 'POST' });

  static deletePlaylist = (playlistId: string) => 
    this.simpleRequest(`/DeletePlaylist?playlistId=${playlistId}`, { method: 'DELETE' });

  static deleteMediaFile = (fileName: string, playlistId: string) => 
    this.simpleRequest(`/DeleteFile?fileName=${encodeURIComponent(fileName)}&playlistId=${playlistId}`, { method: 'DELETE' });

  static playPlaylist = (playlistId: string) => 
    this.simpleRequest(`/api/MediaPlaylist/play/${playlistId}`, { method: 'POST' });

  static async startPlaylist(playlistId: string): Promise<boolean> {  
    const response = await fetch(`${this.API_URL}/StartPlaylist?playlistId=${playlistId}`, {  
      method: "POST",  
    });  
    return response.ok;  
  }  
  
  static async stopPlaylist(playlistId: string): Promise<boolean> {  
    const response = await fetch(`${this.API_URL}/Playlist/Stop/${playlistId}`, {  
      method: "POST",  
    });  
    return response.ok;  
  }  
  
  static async getCurrentPlaylist(): Promise<string | null> {  
    const response = await fetch(`${this.API_URL}/Playlist/Current`);  
    if (!response.ok) return null;  
      
    const data = await response.json();  
    return data.playlistId || null;  
  }  
}
