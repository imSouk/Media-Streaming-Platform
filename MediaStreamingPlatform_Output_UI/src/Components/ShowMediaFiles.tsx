// components/ShowMediaFiles.tsx
import { useState, useEffect } from "react";
import { useMediaFiles } from "../CustomHooks/mapFiles";

import { MediaImage } from "./MediaImage";
import { MediaVideo } from "./MediaVideo";
import { useSlideshow } from "../CustomHooks/useSlideShow";

const IMAGE_MS = 4000;

export default function ShowMediaFiles() {
  const { mediaFiles, mediaBlobUrls, isLoading, error } = useMediaFiles();
  const [muted, setMuted] = useState(true);
  const [displayedMedia, setDisplayedMedia] = useState<any>(null);
  const [isTransitioning, setIsTransitioning] = useState(false);

  const { isImage, goToNext, currentMedia } = useSlideshow(
    mediaFiles,
    IMAGE_MS,
    (m) => {
      if (typeof m?.type === "number") return m.type === 1;
      const ct = m?.contentType || "";
      if (ct.startsWith("image/")) return true;
      if (ct.startsWith("video/")) return false;
      return true;
    }
  );

  useEffect(() => {
    if (!currentMedia) return;
    
    if (!displayedMedia) {
      setDisplayedMedia(currentMedia);
      return;
    }
    
    if (displayedMedia.id !== currentMedia.id) {
      setIsTransitioning(true);
      
      const timer = setTimeout(() => {
        setDisplayedMedia(currentMedia);
        setIsTransitioning(false);
      }, 300);
      
      return () => clearTimeout(timer);
    }
  }, [currentMedia, displayedMedia]);

  const displayedUrl = displayedMedia ? mediaBlobUrls?.[displayedMedia.id] : undefined;

  const handleVideoEnd = () => {
    goToNext();
  };

  if (isLoading)
    return (
      <div className="flex items-center justify-center h-full text-white bg-gray-800 p-4 rounded">
        Loading...
      </div>
    );

  if (error)
    return (
      <div className="flex items-center justify-center h-full text-red-400 bg-gray-800 p-4 rounded">
        Error: {error}
      </div>
    );

  if (!displayedMedia || !displayedUrl)
    return (
      <div className="flex items-center justify-center h-full text-white bg-gray-800 p-4 rounded">
        No media available
      </div>
    );

  return (
    <div className="relative w-full h-full overflow-hidden bg-black">
      <div
        className="absolute inset-0 w-full h-full transition-opacity duration-500 ease-in-out"
        style={{ opacity: isTransitioning ? 0 : 1 }}
      >
        {isImage(displayedMedia) ? (
          <MediaImage
            src={displayedUrl}
            className="w-full h-full object-contain"
          />
        ) : (
          <MediaVideo
            src={displayedUrl}
            type={displayedMedia?.contentType || ""}
            className="w-full h-full object-contain"
            muted={muted}
            onEnd={handleVideoEnd}
          />
        )}
      </div>

      {displayedMedia && !isImage(displayedMedia) && (
        <button
          onClick={() => setMuted((m) => !m)}
          className="absolute bottom-4 right-4 z-30 bg-black bg-opacity-50 text-white px-3 py-1 rounded"
        >
          {muted ? "Unmute" : "Mute"}
        </button>
      )}
    </div>
  );
}


