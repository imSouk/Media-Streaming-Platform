import { useRef } from 'react';

interface Props {
  src: string;
  type?: string;
  className?: string;
  muted: boolean;
  onEnd?: () => void;
}

export function MediaVideo({ src, className = '', muted, onEnd }: Props) {
  const ref = useRef<HTMLVideoElement>(null);

  const handleEnded = () => {
    onEnd?.();
  };

  return (
    <video 
      ref={ref}
      src={src}
      className={className} 
      autoPlay 
      muted={muted}
      playsInline
      onEnded={handleEnded}
    >
    </video>
  );
}