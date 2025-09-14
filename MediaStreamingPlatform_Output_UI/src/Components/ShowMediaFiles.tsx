import { useState, useEffect, useRef } from "react";
import { useMediaFiles } from "../CustomHooks/mapFiles";

export default function ShowMediaFiles() {
  const { medias, mediaBlobs, isLoading, error } = useMediaFiles();
  const [currentIndex, setCurrentIndex] = useState(0);
  const [fadeIn, setFadeIn] = useState(false);
  const [isMuted, setIsMuted] = useState(true);

  const media = medias[currentIndex];
  const blobUrl = mediaBlobs?.[media?.id];
  const videoRef = useRef<HTMLVideoElement>(null);

  useEffect(() => {
    if (!media || !blobUrl) return;

    setFadeIn(true);

    if (media.type === 1) {
      const timer = setTimeout(() => {
        setFadeIn(false);
        setTimeout(() => setCurrentIndex((prev) => (prev + 1) % medias.length), 500);
      }, 4000);

      return () => clearTimeout(timer);
    } else {
      // Reset mute for new video
      setIsMuted(true);
    }
  }, [currentIndex, media, blobUrl, medias.length]);

  const nextMedia = () => {
    setFadeIn(false);
    setTimeout(() => setCurrentIndex((prev) => (prev + 1) % medias.length), 500);
  };

  const toggleMute = () => {
    setIsMuted((prev) => !prev);
    if (videoRef.current) {
      videoRef.current.muted = !videoRef.current.muted;
    }
  };

  if (isLoading) return <p className="text-center mt-10 text-white">Loading...</p>;
  if (error) return <p className="text-red-500 text-center mt-10">Error: {error}</p>;
  if (!media) return <p className="text-center mt-10 text-white">No media found.</p>;

  return (
    <div className="fixed inset-0 w-screen h-screen bg-black overflow-hidden">
      {blobUrl && media.type === 1 && (
        <img
          key={media.id}
          src={blobUrl}
          alt={media.fileName}
          className={`absolute inset-0 w-full h-full object-contain sm:object-cover transition-opacity duration-500 ${
            fadeIn ? "opacity-100" : "opacity-0"
          }`}
        />
      )}

      {blobUrl && media.type === 2 && (
        <>
          <video
            ref={videoRef}
            key={media.id}
            autoPlay
            muted={isMuted}
            onEnded={nextMedia}
            className={`absolute inset-0 w-full h-full object-contain sm:object-cover transition-opacity duration-500 ${
              fadeIn ? "opacity-100" : "opacity-0"
            }`}
          >
            <source src={blobUrl} type={media.contentType} />
          </video>
          <button
            onClick={toggleMute}
            className="absolute bottom-4 right-4 bg-black bg-opacity-50 text-white px-3 py-1 rounded"
          >
            {isMuted ? "Unmute" : "Mute"}
          </button>
        </>
      )}
    </div>
  );
}
