import { useEffect } from 'react';

interface Props {
  src: string;
  className?: string;
}

export function MediaImage({ src, className = '' }: Props) {
  useEffect(() => {
    if (!src) return;
    const img = new Image();
    img.src = src;
  }, [src]);

  return <img src={src} alt="" className={className} draggable={false} />;
}
