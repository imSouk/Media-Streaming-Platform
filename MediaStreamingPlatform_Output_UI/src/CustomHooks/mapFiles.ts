import { useState, useEffect } from "react";
import { MediaUploadService, type MediaFile, type Blob } from "../Functions";

export const useMediaFiles = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [medias, setMedias] = useState<MediaFile[]>([]);
  const [mediaBlobs, setMediaBlobs] = useState<Record<number, string>>({});

  useEffect(() => {
    const loadAllData = async () => {
      setIsLoading(true);
      setError(null);

      try {
        const files = await MediaUploadService.GetAllFiles();
        setMedias(files);

        const ids = files.map(x => x.id);
        const blobs = await MediaUploadService.GetFileContents(ids);

        const urls: Record<number, string> = {};
        blobs.forEach((blob, index) => {
          try {
            const binaryString = atob(blob.fileContent);
            const bytes = new Uint8Array(binaryString.length);
            for (let i = 0; i < binaryString.length; i++) {
              bytes[i] = binaryString.charCodeAt(i);
            }
            const blobObject = new Blob([bytes], { type: blob.contentType });
            const url = URL.createObjectURL(blobObject);
            urls[files[index].id] = url;
          } catch (err) {
            console.warn(`Erro ao processar blob do arquivo ${files[index].id}:`, err);
          }
        });

        setMediaBlobs(urls);
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro ao carregar mídias");
      } finally {
        setIsLoading(false);
      }
    };

    loadAllData();

    return () => {
      Object.values(mediaBlobs).forEach(url => URL.revokeObjectURL(url));
    };
  }, []);

  return { medias, mediaBlobs, isLoading, error };
};
