// components/FileUploadComponent.tsx
import React from 'react';
import { UploadOutlined } from '@ant-design/icons';
import type { UploadProps } from 'antd';
import { Button, Upload } from 'antd';
import { useFileUpload } from '../CustomHooks/Hooks';


const FileUploadComponent: React.FC = () => {
  const { uploadFile, isUploading } = useFileUpload();

  const uploadProps: UploadProps = {
    customRequest: async ({ file, onSuccess, onError }) => {
      try {
        const result = await uploadFile(file as File);
        onSuccess?.(result);
      } catch (error) {
        onError?.(error as Error);
      }
    },
    showUploadList: false,
  };

  return (
    <Upload {...uploadProps}>
      <Button 
        icon={<UploadOutlined />} 
        loading={isUploading}
      >
        Click to Upload
      </Button>
    </Upload>
  );
};

export default FileUploadComponent;