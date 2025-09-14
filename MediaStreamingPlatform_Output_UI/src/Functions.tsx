export interface MediaFile {
  id: number;
  fileName: string;
  filePath: string;
  contentType: string;
  fileSize: number;
  type?: number;
  uploadedAt: string;
}

export interface Blob {
  fileContent: string;
  contentType: string;
  fileName: string;
}

export class MediaUploadService {
  private static readonly API_URL = "http://localhost:5278";

  static async GetFileContents(ids: number[]): Promise<Blob[]> {
    const response = await fetch(`${this.API_URL}/MediaFile/GetFileContent`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(ids),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Failed to get multiple file contents: ${response.status} - ${errorText}`);
    }

    return response.json();
  }

  static async GetAllFiles(): Promise<MediaFile[]> {
    const response = await fetch(`${this.API_URL}/GetAll`, {
      method: "GET",
    });

    if (!response.ok) {
      throw new Error(`Failed to get all files: ${response.status}`);
    }
    return response.json();
  }
}
