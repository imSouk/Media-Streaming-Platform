// ShowMediaFiles.tsx
import { useEffect, useState } from "react";
import { mapFiles } from "../CustomHooks/mapFiles";
import type { MediaFile } from "../Functions";

export default function ShowMediaFiles() {
  const { allMedias, Isloading } = mapFiles();
  const [medias, setMedias] = useState<MediaFile[]>([]);

  useEffect(() => {
    const fetchMedias = async () => {
      const result = await allMedias();
      setMedias(result || []);
    };
    fetchMedias();
  }, []);

  if (Isloading) {
    return <p>Carregando mídias...</p>;
  }

  if (medias.length === 0) {
    return <p>Nenhuma mídia encontrada.</p>;
  }

  return (
    <div className="grid grid-cols-3 gap-4">
      {medias.map((media) => (
        <div key={media.id} className="p-2 border rounded shadow">
          {media.type === 1 && (
            <img
              src={media.filePath.replace(/\\/g, "/")} // transforma \ em /
              alt={media.fileName}
              className="w-full h-auto rounded"
            />
          )}
          {media.type === 2 && (
            <video controls className="w-full rounded">
              <source
                src={media.filePath.replace(/\\/g, "/")}
                type="video/mp4"
              />
              Seu navegador não suporta vídeo.
            </video>
          )}
          {media.type === 3 && (
            <audio controls className="w-full">
              <source
                src={media.filePath.replace(/\\/g, "/")}
                type="audio/mpeg"
              />
              Seu navegador não suporta áudio.
            </audio>
          )}
          {!media.type && <p>{media.filePath}</p>}
        </div>
      ))}
    </div>
  );
}
