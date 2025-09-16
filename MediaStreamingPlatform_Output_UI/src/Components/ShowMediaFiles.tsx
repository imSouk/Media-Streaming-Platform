import { useState, useEffect, useRef, useCallback } from "react";
import { useMediaFiles } from "../CustomHooks/mapFiles";

const IMAGE_DISPLAY_MS = 4000;
const FADE_MS = 500;
const VIDEO_FALLBACK_MS = 10000;

export default function ShowMediaFiles() {
  const { mediaFiles, mediaBlobUrls, isLoading, error } = useMediaFiles();

  const [current, setCurrent] = useState(0);
  const [incoming, setIncoming] = useState<number | null>(null);
  const [topVisible, setTopVisible] = useState(false);
  const [isMuted, setIsMuted] = useState(true);

  const videoRef = useRef<HTMLVideoElement | null>(null);
  const advanceTimer = useRef<number | null>(null);
  const finishTimer = useRef<number | null>(null);

  const currentMedia = mediaFiles[current];
  const currentUrl = currentMedia ? mediaBlobUrls?.[currentMedia.id] : undefined;

  const clearAll = useCallback(() => {
    if (advanceTimer.current != null) { clearTimeout(advanceTimer.current); advanceTimer.current = null; }
    if (finishTimer.current != null) { clearTimeout(finishTimer.current); finishTimer.current = null; }
  }, []);

  const nextIdx = useCallback((i = current) => {
    return mediaFiles.length ? (i + 1) % mediaFiles.length : 0;
  }, [current, mediaFiles.length]);

  const startImageCrossfade = useCallback((next: number) => {
    const url = mediaBlobUrls?.[mediaFiles[next].id];
    if (!url) {
      setCurrent(next);
      return;
    }
    const img = new Image();
    img.onload = () => {
      setIncoming(next);
      requestAnimationFrame(() => {
        setTopVisible(true);
        finishTimer.current = window.setTimeout(() => {
          setCurrent(next);
          setIncoming(null);
          setTopVisible(false);
          finishTimer.current = null;
        }, FADE_MS);
      });
    };
    img.src = url;
  }, [mediaBlobUrls, mediaFiles]);

  const scheduleAdvanceForImage = useCallback(() => {
    clearAll();
    advanceTimer.current = window.setTimeout(() => {
      const next = nextIdx();
      startImageCrossfade(next);
      advanceTimer.current = null;
    }, IMAGE_DISPLAY_MS);
  }, [clearAll, nextIdx, startImageCrossfade]);

  const scheduleAdvanceForVideo = useCallback((durationMs: number) => {
    clearAll();
    advanceTimer.current = window.setTimeout(() => {
      setTopVisible(false);
      finishTimer.current = window.setTimeout(() => {
        setCurrent(prev => nextIdx(prev));
        setTopVisible(true);
        finishTimer.current = null;
      }, FADE_MS);
      advanceTimer.current = null;
    }, durationMs);
  }, [clearAll, nextIdx]);

  useEffect(() => {
    clearAll();
    if (!currentMedia) return;
    setTopVisible(false);
    const img = currentUrl ? new Image() : null;
    if (img && currentUrl) {
      img.onload = () => {
        setTopVisible(true);
        if (currentMedia.type === 1) scheduleAdvanceForImage();
      };
      img.src = currentUrl;
    } else {
      const t = window.setTimeout(() => setTopVisible(true), 50);
      advanceTimer.current = t as unknown as number;
      if (currentMedia.type === 1) scheduleAdvanceForImage();
    }
    if (currentMedia.type !== 1) {
      const fallback = window.setTimeout(() => scheduleAdvanceForVideo(VIDEO_FALLBACK_MS), IMAGE_DISPLAY_MS);
      advanceTimer.current = fallback as unknown as number;
    }
    return () => clearAll();
  }, [currentMedia, currentUrl, scheduleAdvanceForImage, scheduleAdvanceForVideo, clearAll]);

  const onVideoLoadedMetadata = useCallback(() => {
    const v = videoRef.current;
    if (!v) return;
    const durMs = (v.duration && isFinite(v.duration) && v.duration > 0) ? Math.round(v.duration * 1000) : VIDEO_FALLBACK_MS;
    scheduleAdvanceForVideo(durMs + 50);
  }, [scheduleAdvanceForVideo]);

  const onVideoEnded = useCallback(() => {
    clearAll();
    setTopVisible(false);
    finishTimer.current = window.setTimeout(() => {
      setCurrent(prev => nextIdx(prev));
      setTopVisible(true);
      finishTimer.current = null;
    }, FADE_MS);
  }, [clearAll, nextIdx]);

  useEffect(() => {
    const v = videoRef.current;
    if (!v) return;
    v.muted = isMuted;
    try { v.currentTime = 0; v.play().catch(() => {}); } catch {}
  }, [isMuted, current]);

  useEffect(() => () => clearAll(), [clearAll]);

  const toggleMute = useCallback(() => {
    setIsMuted(prev => {
      const next = !prev;
      if (videoRef.current) videoRef.current.muted = next;
      return next;
    });
  }, []);

  if (isLoading) return <div className="flex items-center justify-center h-full text-white bg-gray-800 p-4 rounded">Loading...</div>;
  if (error) return <div className="flex items-center justify-center h-full text-red-400 bg-gray-800 p-4 rounded">Error</div>;

  const base = `absolute inset-0 w-full h-full object-contain transition-opacity duration-[${FADE_MS}ms]`;
  const topClass = topVisible ? "opacity-100" : "opacity-0";
  const bottomClass = incoming == null ? "opacity-100" : "opacity-0";

  const incomingMedia = incoming != null ? mediaFiles[incoming] : null;
  const incomingUrl = incomingMedia ? mediaBlobUrls?.[incomingMedia.id] : undefined;

  return (
    <div className="relative w-full h-full overflow-hidden bg-black">
      {currentMedia && currentUrl && currentMedia.type === 1 && (
        <img src={currentUrl} alt="" className={`${base} ${bottomClass}`} draggable={false} />
      )}

      {incomingMedia && incomingUrl && incomingMedia.type === 1 && (
        <img src={incomingUrl} alt="" className={`${base} ${topClass} pointer-events-none`} draggable={false} />
      )}

      {currentMedia && currentUrl && currentMedia.type !== 1 && (
        <>
          <video
            ref={videoRef}
            key={currentMedia.id}
            className={`${base} ${topClass}`}
            autoPlay
            muted={isMuted}
            onLoadedMetadata={onVideoLoadedMetadata}
            onCanPlay={() => setTopVisible(true)}
            onEnded={onVideoEnded}
            playsInline
          >
            <source src={currentUrl} type={currentMedia.contentType} />
          </video>
          <button onClick={toggleMute} className="absolute bottom-4 right-4 z-30 bg-black bg-opacity-50 text-white px-3 py-1 rounded">
            {isMuted ? "Unmute" : "Mute"}
          </button>
        </>
      )}
    </div>
  );
}
