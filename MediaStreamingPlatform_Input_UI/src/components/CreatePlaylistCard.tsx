import { Card, Button } from 'antd';
import { PlusOutlined } from '@ant-design/icons';

interface CreatePlaylistCardProps {
  onCreateClick: () => void;
}

export default function CreatePlaylistCard({ onCreateClick }: CreatePlaylistCardProps) {
  return (
    <Card style={{
      width: 300,
      minHeight: 200,
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      border: '2px dashed #d9d9d9'
    }}>
      <Button 
        type="dashed" 
        icon={<PlusOutlined />} 
        size="large"
        onClick={onCreateClick}
      >
        Create Playlist
      </Button>
    </Card>
  );
}