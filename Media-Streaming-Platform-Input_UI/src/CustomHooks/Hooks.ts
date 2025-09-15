import { useEffect, useState } from 'react';
import { message } from 'antd';
import MediaService from '../functions';
import Playlist from '../components/Playlist';

interface MediaFile {
  fileName: string;
  contentType: string;
}

interface Playlist {
  id: number;
  playlistName?: string;
  mediaFiles?: MediaFile[];
}

export function useFileUpload() {
  const [isUploading, setIsUploading] = useState(false);
  
  const uploadFile = async (file: File, playlistId: number) => { 
    setIsUploading(true);
    try {
      const result = await MediaService.uploadFile(file, playlistId);
      message.success(`${file.name} uploaded successfully`);
      return result;
    } catch (error) {
      message.error(`${file.name} upload failed`);
      throw error;
    } finally {
      setIsUploading(false);
    }
  };

  return { uploadFile, isUploading };
}

export function usePlaylists() {
  const [playlists, setPlaylists] = useState<Playlist[]>([]);
  const [loading, setLoading] = useState(true);

  const fetchPlaylists = async () => {
    setLoading(true);
    try {
      const result: Playlist[] = await MediaService.getPlaylists();
      setPlaylists(result);
    } catch (error) {
      message.error("Failed to load playlists");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchPlaylists();
  }, []);
  return { playlists, loading, fetchPlaylists };
}
