import { useEffect, useState, useCallback } from 'react';
import { message } from 'antd';
import MediaService from '../functions';

export interface MediaFile {
  fileName: string;
  contentType: string;
}

export interface Playlist {
  id: string;
  playlistName?: string;
  mediaFiles?: MediaFile[];
}

export function useFileUpload() {
  const [isUploading, setIsUploading] = useState(false);
  
  const uploadFile = useCallback(async (file: File, playlistId: string) => { 
    setIsUploading(true);
    
    const result = await MediaService.uploadFile(file, playlistId);
    setIsUploading(false);
    
    const success = !!result;
    message[success ? 'success' : 'error'](`${file.name} ${success ? 'uploaded successfully' : 'upload failed'}`);
    
    return result;
  }, []);

  return { uploadFile, isUploading };
}

export function usePlaylists() {
  const [playlists, setPlaylists] = useState<Playlist[]>([]);
  const [loading, setLoading] = useState(true);

  const fetchPlaylists = useCallback(async () => {
    setLoading(true);
    const result = await MediaService.getPlaylists();
    
    setPlaylists(result || []);
    setLoading(false);
    
    if (!result) message.error("Failed to load playlists");
  }, []);

  const updatePlaylist = useCallback((playlistId: string, updater: (playlist: Playlist) => Playlist) => {
    setPlaylists(prev => prev.map(p => p.id === playlistId ? updater(p) : p));
  }, []);

  const addPlaylist = useCallback((newPlaylist: Playlist) => {
    setPlaylists(prev => [...prev, newPlaylist]);
  }, []);

  const removePlaylist = useCallback((playlistId: string) => {
    setPlaylists(prev => prev.filter(p => p.id !== playlistId));
  }, []);

  const addFileToPlaylist = useCallback((playlistId: string, newFile: MediaFile) => {
    updatePlaylist(playlistId, playlist => ({
      ...playlist,
      mediaFiles: [...(playlist.mediaFiles || []), newFile]
    }));
  }, [updatePlaylist]);

  const removeFileFromPlaylist = useCallback((playlistId: string, fileName: string) => {
    updatePlaylist(playlistId, playlist => ({
      ...playlist,
      mediaFiles: playlist.mediaFiles?.filter(f => f.fileName !== fileName)
    }));
  }, [updatePlaylist]);

  useEffect(() => {
    fetchPlaylists();
  }, [fetchPlaylists]);

  return { 
    playlists, 
    loading, 
    fetchPlaylists,
    addPlaylist,
    removePlaylist,
    addFileToPlaylist,
    removeFileFromPlaylist
  };
}