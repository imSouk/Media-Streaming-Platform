export default class MediaService {
  private static readonly API_URL = 'http://localhost:5278';

  private static request = async (endpoint: string, options?: RequestInit) => {
    const response = await fetch(`${this.API_URL}${endpoint}`, options);
    if (!response.ok) throw new Error(`Request failed: ${response.status}`);
    return response.json();
  };

static uploadFile = (file: File, playlistId: number) => { 
  const formData = new FormData();  
  formData.append('file', file);  
  formData.append('playlistId', playlistId.toString());
  return this.request('/save', { method: 'POST', body: formData });  
};
  static getPlaylists = () => this.request('/GetAllPlaylists');
}