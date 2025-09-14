import { useState } from 'react';
import { message } from 'antd';
import { MediaUploadService } from '../functions';


export const useFileUpload = () => {
  const [isUploading, setIsUploading] = useState(false);

  const uploadFile = async (file: File) => {
    setIsUploading(true);
    try {
      const result = await MediaUploadService.uploadFile(file);
      message.success(`${file.name} uploaded successfully`);
      return result;
    } catch (error) {
      message.error(`${file.name} upload failed`);
      throw error;
    } finally {
      setIsUploading(false);
    }
  };

  return { uploadFile, isUploading };
};