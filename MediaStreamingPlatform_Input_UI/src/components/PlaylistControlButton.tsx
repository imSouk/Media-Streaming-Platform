import { useState } from 'react';
import MediaService from '../functions';


interface PlaylistControlButtonProps {
  playlistId: string;
  playlistName?: string;
}

export default function PlaylistControlButton({ playlistId, playlistName }: PlaylistControlButtonProps) {
  const [isLoading, setIsLoading] = useState(false);

  const handleClick = async () => {
    setIsLoading(true);
    
    await MediaService.startPlaylist(playlistId);
    
    setIsLoading(false);
  };

  return (
    <button
      onClick={handleClick}
      disabled={isLoading}
      className="bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded-lg"
    >
      {isLoading ? "Loading..." : `▶️ Play ${playlistName || playlistId}`}
    </button>
  );
}