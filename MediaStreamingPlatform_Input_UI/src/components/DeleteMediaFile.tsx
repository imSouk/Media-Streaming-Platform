import { useState } from 'react';
import { Button, Popconfirm, message } from 'antd';
import { DeleteOutlined } from '@ant-design/icons';
import MediaService from '../functions';

interface DeleteMediaFileProps {
  fileName: string;
  playlistId: string;
  onDeleted: () => void;
}
export default function DeleteMediaFile({ fileName, playlistId, onDeleted }: DeleteMediaFileProps) {
  const [deleting, setDeleting] = useState(false);

  const handleDelete = async () => {
    setDeleting(true);
    const success = await MediaService.deleteMediaFile(fileName, playlistId);
    setDeleting(false);
    
    if (success) {
      message.success(`${fileName} deleted successfully`);
      onDeleted();
    } else {
      message.error(`Failed to delete ${fileName}`);
    }
  };

  return (
    <Popconfirm
      title="Delete file"
      description={`Are you sure you want to delete ${fileName}?`}
      onConfirm={handleDelete}
      okText="Yes"
      cancelText="No"
      okButtonProps={{ loading: deleting }}
    >
      <Button 
        type="text" 
        danger 
        icon={<DeleteOutlined />}
        size="small"
        loading={deleting}
        style={{ marginLeft: 8, padding: '2px 4px' }}
      />
    </Popconfirm>
  );
}