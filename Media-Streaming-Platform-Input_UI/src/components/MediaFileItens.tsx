import type { MediaFile } from '../CustomHooks/Hooks';
import DeleteMediaFile from './DeleteMediaFile';

interface MediaFileItemProps {
  file: MediaFile;
  playlistId: string;
  onDeleted: () => void;
}
export default function MediaFileItem({ file, playlistId, onDeleted }: MediaFileItemProps) {
  return (
    <li style={{ 
      display: 'flex', 
      justifyContent: 'space-between', 
      alignItems: 'center',
      padding: '4px 0'
    }}>
      <div>
        <div><strong>Name:</strong> {file.fileName}</div>
        <div><strong>Type:</strong> {file.contentType}</div>
      </div>
      <DeleteMediaFile 
        fileName={file.fileName}
        playlistId={playlistId}
        onDeleted={onDeleted}
      />
    </li>
  );
}