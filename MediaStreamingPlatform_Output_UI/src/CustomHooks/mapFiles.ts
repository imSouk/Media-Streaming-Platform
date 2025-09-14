// CustomHooks/mapFiles.tsx
import { useState } from "react";
import { MediaUploadService, type MediaFile } from "../Functions";

export const mapFiles = () => {
  const [Isloading, setIsloading] = useState(false);

  const allMedias = async (): Promise<MediaFile[]> => {
    setIsloading(true);
    const result = await MediaUploadService.GetAllFiles();
    setIsloading(false);
    return result;
  };

  return { allMedias, Isloading };
};
