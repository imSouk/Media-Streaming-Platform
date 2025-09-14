// Functions.tsx
export interface MediaFile {
  id: number;
  fileName: string;
  filePath: string;
  contentType: string;
  fileSize: number;
  type?: number;
  uploadedAt: string;
}

export class MediaUploadService {
  private static readonly API_URL = "http://localhost:5278";

  static async GetAllFiles(): Promise<MediaFile[]> {
    const response = await fetch(`${this.API_URL}/GetAll`, {
      method: "GET",
    });

    if (!response.ok) {
      throw new Error("Upload failed");
    }

    return response.json();
  }
}
