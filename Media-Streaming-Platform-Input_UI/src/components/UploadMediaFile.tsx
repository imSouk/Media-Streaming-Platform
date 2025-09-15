import { UploadOutlined } from '@ant-design/icons';
import { Button, Upload } from 'antd';
import { useFileUpload } from '../CustomHooks/Hooks';


interface UploadMediaFileProps {
  playlistId: number; 
}

export default function UploadMediaFile({ playlistId }: UploadMediaFileProps) {
  const { uploadFile, isUploading } = useFileUpload();

  return (
    <Upload
      customRequest={async ({ file, onSuccess, onError }) => {
        try {
          const result = await uploadFile(file as File, playlistId); 
          onSuccess?.(result);
        } catch (error) {
          onError?.(error as Error);
        }
      }}
      showUploadList={false}
    >
      <Button icon={<UploadOutlined />} loading={isUploading}>
        Click to Upload
      </Button>
    </Upload>
  );
}


