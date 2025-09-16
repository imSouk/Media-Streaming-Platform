import { useState } from 'react';
import { Modal, Input, message } from 'antd';
import MediaService from '../functions';
import type { Playlist } from '../CustomHooks/Hooks';

interface CreatePlaylistModalProps {
  isOpen: boolean;
  onClose: () => void;
  onPlaylistCreated: (playlist: Playlist) => void;
}

export default function CreatePlaylistModal({ isOpen, onClose, onPlaylistCreated }: CreatePlaylistModalProps) {
  const [playlistName, setPlaylistName] = useState('');
  const [creating, setCreating] = useState(false);
  const [error, setError] = useState('');

  const handleClose = () => {
    setPlaylistName('');
    setError('');
    onClose();
  };

  const handleCreate = async () => {
    const name = playlistName.trim();
    if (!name) return setError('Please enter a playlist name');
    if (creating) return;
    
    setError('');
    setCreating(true);
    
    const success = await MediaService.createPlaylist(name);
    setCreating(false);
    
    if (success) {
      message.success('Playlist created successfully');
      const newPlaylist: Playlist = {
        id: Date.now().toString(), 
        playlistName: name,
        mediaFiles: []
      };
      
      onPlaylistCreated(newPlaylist);
      handleClose();
    } else {
      message.error('Failed to create playlist');
    }
  };

  return (
    <Modal
      title="Create New Playlist"
      open={isOpen}
      onOk={handleCreate}
      onCancel={handleClose}
      confirmLoading={creating}
    >
      <Input
        placeholder="Enter playlist name"
        value={playlistName}
        onChange={(e) => {
          setPlaylistName(e.target.value);
          if (error) setError('');
        }}
        onPressEnter={handleCreate}
        status={error ? 'error' : ''}
      />
      {error && <div style={{ color: 'red', marginTop: 8 }}>{error}</div>}
    </Modal>
  );
}