import type { MediaFile } from '../CustomHooks/Hooks';
import MediaFileItem from './MediaFileItem';

interface PlaylistFilesProps {
  files?: MediaFile[];
  playlistId: string;
  onFileDeleted: (fileName: string) => void;
}

export default function PlaylistFiles({ files, playlistId, onFileDeleted }: PlaylistFilesProps) {
  if (!files?.length) {
    return <p>No files in this playlist</p>;
  }

  return (
    <>
      <strong>Files:</strong>
      <ul style={{ listStyle: 'none', padding: 0 }}>
        {files.map((file, index) => (
          <MediaFileItem 
            key={index} 
            file={file} 
            playlistId={playlistId}
            onDeleted={() => onFileDeleted(file.fileName)}
          />
        ))}
      </ul>
    </>
  );
}