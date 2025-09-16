import { UploadOutlined } from '@ant-design/icons';
import { Button, Upload } from 'antd';
import { useFileUpload } from '../CustomHooks/Hooks';
import type { MediaFile } from '../CustomHooks/Hooks';

interface UploadMediaFileProps {
  playlistId: string; 
  onFileUploaded: (file: MediaFile) => void;
}

export default function UploadMediaFile({ playlistId, onFileUploaded }: UploadMediaFileProps) {
  const { uploadFile, isUploading } = useFileUpload();

  const handleUpload = async ({ file, onSuccess, onError }: any) => {
    try {
      const result = await uploadFile(file as File, playlistId); 
      
      if (result) {
        onFileUploaded({
          fileName: file.name,
          contentType: file.type
        });
        onSuccess?.(result);
      }
    } catch (error) {
      onError?.(error as Error);
    }
  };

  return (
    <Upload customRequest={handleUpload} showUploadList={false}>
      <Button icon={<UploadOutlined />} loading={isUploading}>
        Click to Upload
      </Button>
    </Upload>
  );
}