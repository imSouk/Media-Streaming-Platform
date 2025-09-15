import { Spin, Card, Button } from "antd"; // ADICIONAR Button
import { PlusOutlined } from '@ant-design/icons'; // ADICIONAR ícone
import { usePlaylists } from "../CustomHooks/Hooks";
import UploadMediaFile from "./UploadMediaFile";

export default function Playlist() {
  const { playlists, loading } = usePlaylists();
  
  const handleCreatePlaylist = () => {

  };

  if (loading) {
    return <Spin size="large" />;
  }
  
  return (
    <div
      style={{
        display: "flex",
        flexWrap: "wrap",
        gap: "16px",
        padding: "20px",
      }}
    >
      <Card
        style={{ 
          width: 300, 
          minHeight: 200, 
          display: 'flex', 
          alignItems: 'center', 
          justifyContent: 'center',
          border: '2px dashed #d9d9d9'
        }}
      >
        <Button 
          type="dashed" 
          icon={<PlusOutlined />} 
          size="large"
          onClick={handleCreatePlaylist}
        >
          Create Playlist
        </Button>
      </Card>

      {playlists.map((playlist) => (
        <Card
          key={playlist.id}
          title={`Playlist ${playlist.playlistName || playlist.id}`}
          style={{ width: 300, minHeight: 200 }}
        >
          <div>
            <strong>Files:</strong>
            {playlist.mediaFiles && playlist.mediaFiles.length > 0 ? (
              <ul>
                {playlist.mediaFiles.map((file, index) => (
                  <li key={index}>
                    <div>
                      <strong>Name:</strong> {file.fileName}
                    </div>
                    <div>
                      <strong>Type:</strong> {file.contentType}
                    </div>
                  </li>
                ))}
              </ul>
            ) : (
              <p>No files in this playlist</p>
            )}
            <UploadMediaFile playlistId={playlist.id} />
          </div>
        </Card>
      ))}
    </div>
  );
}