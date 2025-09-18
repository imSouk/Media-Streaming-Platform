import { useState } from 'react';
import { Button, Popconfirm, message } from 'antd';
import { DeleteOutlined } from '@ant-design/icons';
import MediaService from '../functions';

interface DeletePlaylistButtonProps {
  playlistId: string;
  onDeleted: () => void;
}

export default function DeletePlaylistButton({ playlistId, onDeleted }: DeletePlaylistButtonProps) {
  const [deleting, setDeleting] = useState(false);

  const handleDelete = async () => {
    setDeleting(true);
    
    const success = await MediaService.deletePlaylist(playlistId);
    setDeleting(false);
    
    if (success) {
      message.success('Playlist deleted successfully');
      onDeleted();
    } else {
      message.error('Failed to delete playlist');
    }
  };

  return (
    <Popconfirm
      title="Delete playlist"
      description="Are you sure you want to delete this playlist?"
      onConfirm={handleDelete}
      okText="Yes"
      cancelText="No"
      okButtonProps={{ loading: deleting }}
    >
      <Button 
        type="text" 
        danger 
        icon={<DeleteOutlined />}
        loading={deleting}
        size="small"
      >
        Delete
      </Button>
    </Popconfirm>
  );
}