import { Card } from 'antd';
import type { Playlist, MediaFile } from '../CustomHooks/Hooks';
import PlaylistFiles from './PlaylistFiles';
import UploadMediaFile from './UploadMediaFile';
import DeletePlaylistButton from './DeletePlaylist';
import PlaylistControlButton from './StartPlaylistButton';


interface PlaylistCardProps {
  playlist: Playlist;
  onDeleted: () => void;
  onFileUploaded: (file: MediaFile) => void;
  onFileDeleted: (fileName: string) => void;
}

export default function PlaylistCard({ playlist, onDeleted, onFileUploaded, onFileDeleted }: PlaylistCardProps) {
  return (
    <Card
      title={`Playlist ${playlist.playlistName || playlist.id}`}
      style={{ width: 300, minHeight: 200 }}
      extra={
        <div className="flex gap-2">
          <PlaylistControlButton 
            playlistId={playlist.id} 
            playlistName={playlist.playlistName}
            className="text-xs px-2 py-1" 
          />
          <DeletePlaylistButton playlistId={playlist.id} onDeleted={onDeleted} />
        </div>
      }
    >
      <PlaylistFiles 
        files={playlist.mediaFiles} 
        playlistId={playlist.id}
        onFileDeleted={onFileDeleted}
      />
      <UploadMediaFile 
        playlistId={playlist.id} 
        onFileUploaded={onFileUploaded}
      />
    </Card>
  );
}