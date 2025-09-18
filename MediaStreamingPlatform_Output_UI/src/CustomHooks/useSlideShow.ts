import { useState, useEffect, useCallback } from 'react';

export interface MinimalMedia {
  id?: string | number;
  type?: number;
  contentType?: string;
}

export function useSlideshow<T extends MinimalMedia>(
  media: T[],
  imageDuration = 4000,
  isImage: (m: T) => boolean = (m) => {
    if (typeof m?.type === 'number') return m.type === 1;
    const ct = m?.contentType || '';
    if (ct.startsWith('image/')) return true;
    if (ct.startsWith('video/')) return false;
    return true;
  }
) {
  const [current, setCurrent] = useState(0);

  const goToNext = useCallback(() => {
    if (media.length === 0) return;
    setCurrent(prev => {
      const next = (prev + 1) % media.length;
      return next;
    });
  }, [media.length]);

  useEffect(() => {
    if (media.length === 0) return;
    
    const currentMedia = media[current];
    if (!currentMedia || !isImage(currentMedia)) {
      return;
    }

    const timer = setTimeout(() => {
      goToNext();
    }, imageDuration);

    return () => clearTimeout(timer);
  }, [current, media, imageDuration, isImage, goToNext]);

  return {
    current,
    isImage,
    goToNext,
    currentMedia: media[current] || null
  };
}
