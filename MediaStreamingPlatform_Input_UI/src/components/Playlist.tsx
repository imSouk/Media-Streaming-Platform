import { useState } from 'react';
import { Spin } from 'antd';
import { usePlaylists } from '../CustomHooks/Hooks';
import CreatePlaylistCard from './CreatePlaylistCard';
import CreatePlaylistModal from './CreatePlaylistModal';
import PlaylistCard from './PlaylistCard';

export default function Playlist() {
  const { 
    playlists, 
    loading, 
    addPlaylist,
    removePlaylist,
    addFileToPlaylist,
    removeFileFromPlaylist
  } = usePlaylists();
  const [isModalOpen, setIsModalOpen] = useState(false);

  if (loading) return <Spin size="large" />;

  return (
    <div style={{
      display: "flex",
      flexWrap: "wrap",
      gap: "16px",
      padding: "20px"
    }}>
      <CreatePlaylistCard onCreateClick={() => setIsModalOpen(true)} />
      
      <CreatePlaylistModal 
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onPlaylistCreated={addPlaylist}
      />

      {playlists.map((playlist) => (
        <PlaylistCard 
          key={playlist.id} 
          playlist={playlist} 
          onDeleted={() => removePlaylist(playlist.id)}
          onFileUploaded={(file) => addFileToPlaylist(playlist.id, file)}
          onFileDeleted={(fileName) => removeFileFromPlaylist(playlist.id, fileName)}
        />
      ))}
    </div>
  );
}