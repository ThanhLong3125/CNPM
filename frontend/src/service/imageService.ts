import api from "./apiService";

export interface ImageUploadResponse {
  id: string;
  imageId: string;
  path: string;
  diagnosisId: string;
  uploadDate: string;
  aiAnalysis: string | null;
  imageName: string;
}

export interface ImageInfo {
  id: string;
  imageId: string;
  path: string;
  diagnosisId: string;
  uploadDate: string;
  aiAnalysis: string | null;
  imageName: string;
  isDeleted: boolean;
}

export interface ImageAnalysisResult {
  result(result: any): unknown;
  id: string;
  uploadDate: string;
  imageName: string;
  aiAnalysis: string;
  diagnosisId: string;
  path: string;
}

// Upload ảnh - FIXED to match backend API
export const uploadImage = async (
  diagnosisId: string, 
  file: File, 
  imageName?: string
): Promise<ImageUploadResponse> => {
  try {
    const formData = new FormData();
    formData.append('DiagnosisId', diagnosisId); // Required by backend
    formData.append('File', file); // Required by backend
    if (imageName) {
      formData.append('ImageName', imageName); // Optional
    }

    const response = await api.post("/Image/upload", formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });

    console.log("Image uploaded:", response.data);
    return response.data;
  } catch (error: unknown) {
    console.error("Lỗi khi upload ảnh:", error);
    throw error;
  }
};

//Lấy thông tin ảnh theo ID
export const fetchImageById = async (imageId: string): Promise<ImageInfo> => {
  try {
    const response = await api.get(`/Image/${imageId}`);
    console.log("Image info:", response.data);
    return response.data;
  } catch (error: unknown) {
    console.error("Lỗi khi lấy thông tin ảnh:", error);
    throw error;
  }
};

//Xóa ảnh
export const deleteImage = async (imageId: string): Promise<boolean> => {
  try {
    const response = await api.delete(`/Image/${imageId}`);
    console.log("Image deleted:", response.status);
    return response.status === 200 || response.status === 204;
  } catch (error: unknown) {
    console.error("Lỗi khi xóa ảnh:", error);
    return false;
  }
};

// Phân tích ảnh bằng AI - FIXED to use real API
export const analyzeImage = async (imageId: string): Promise<ImageAnalysisResult> => {
  try {
    const response = await api.post(`/Image/${imageId}/analyze`);
    console.log("Image analysis result:", response.data);
    return response.data;
  } catch (error: unknown) {
    console.error("Lỗi khi phân tích ảnh:", error);
    throw error;
  }
};

//Lấy URL để download ảnh
export const getImageDownloadUrl = (imageId: string): string => {
  return `${api.defaults.baseURL}/Image/${imageId}`;
};

//Lấy ảnh dưới dạng blob để hiển thị
export const fetchImageBlob = async (imageId: string): Promise<Blob> => {
  try {
    const response = await api.get(`/Image/${imageId}`, {
      responseType: 'blob',
    });
    return response.data;
  } catch (error: unknown) {
    console.error("Lỗi khi lấy ảnh blob:", error);
    throw error;
  }
};

// Lấy tất cả ảnh của một diagnosis (dựa vào diagnosisId)
export const fetchImagesByDiagnosisId = async (diagnosisId: string): Promise<ImageInfo[]> => {
  try {
    // Backend chưa có API này, tạm thời dùng mảng rỗng
    // Cần backend implement: GET /api/Image/diagnosis/{diagnosisId}
    console.log(`Fetching images for diagnosisId: ${diagnosisId}`);
    return [];
  } catch (error: unknown) {
    console.error("Lỗi khi lấy ảnh theo diagnosis:", error);
    throw error;
  }
}; 