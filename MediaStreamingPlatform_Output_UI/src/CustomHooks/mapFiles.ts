import { useState, useEffect, useCallback } from "react";
import { MediaUploadService, type MediaFile, type Blob } from "../Functions";
import { SignalRService } from "../Services/SignalRService";

export const useMediaFiles = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [mediaFiles, setMediaFiles] = useState<MediaFile[]>([]);
  const [mediaBlobUrls, setMediaBlobUrls] = useState<Record<number, string>>({});
  const [currentPlaylistId, setCurrentPlaylistId] = useState<number | null>(null);

  const createBlobUrl = useCallback((blob: Blob, contentType: string) => {
    const binaryString = atob(blob.fileContent);
    const bytes = new Uint8Array(binaryString.length);
    for (let i = 0; i < binaryString.length; i++) {
      bytes[i] = binaryString.charCodeAt(i);
    }
    const blobObject = new Blob([bytes], { type: contentType });
    return URL.createObjectURL(blobObject);
  }, []);

  const loadPlaylistData = useCallback(async (playlistId: number) => {
    setIsLoading(true);
    setError(null);

    try {
      const files = await MediaUploadService.GetPlaylistFilesById(playlistId);
      
      if (!files?.length) {
        setMediaFiles([]);
        setIsLoading(false);
        return;
      }
      
      setMediaFiles(files);

      const ids = files.map(x => x.id);
      const blobs = await MediaUploadService.GetFileContents(ids);
      
      if (!blobs) {
        setError("Failed to load media content");
        setIsLoading(false);
        return;
      }

      const urls: Record<number, string> = {};
      blobs.forEach((blob, index) => {
        if (blob?.fileContent && files[index]) {
          const url = createBlobUrl(blob, files[index].contentType);
          if (url) urls[files[index].id] = url;
        }
      });

      setMediaBlobUrls(urls);
      setIsLoading(false);
      
    } catch (err) {
      setError(`Failed to load playlist: ${err}`);
      setIsLoading(false);
    }
  }, [createBlobUrl]);

  useEffect(() => {
    const setupSignalR = async () => {
      try {
        await SignalRService.connect();
        
        SignalRService.onPlaylistStarted((data: any) => {
          const id = data.PlaylistId || data.playlistId || data.id;
          
          if (id) {
            setCurrentPlaylistId(id);
            loadPlaylistData(id);
          }
        });
        
      } catch (err) {
        setError(`SignalR connection failed: ${err}`);
      }
    };

    setupSignalR();

    return () => {
      SignalRService.off('PlaylistStarted');
      Object.values(mediaBlobUrls).forEach(url => URL.revokeObjectURL(url));
    };
  }, [loadPlaylistData]);

  return { 
    mediaFiles, 
    mediaBlobUrls, 
    isLoading, 
    error, 
    currentPlaylistId
  };
};