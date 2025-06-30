
import api from "../service/apiService";
import { mapGender } from "../utils/formatters";

export interface patient {
  patientId: string;
  medicalRecordId: string;
  fullName: string;
  physicicanId: string;
  createdAt: string;
  gender: string;
  phone: number;
  dateOfBirth: string;
  diagnosisNotes: string;
}

// NEW: Diagnosis History interfaces
export interface DiagnosisHistoryDto {
  diagnosisId: string;
  medicalRecordId: string;
  diagnosedDate: string;
  notes: string;
  patientId: string;
  patientName: string;
  patientGender: string;
  patientDateOfBirth: string;
  imageCount: number;
  hasAIAnalysis: boolean;
  symptoms: string;
  doctorId: string;
}

export interface DiagnosisDetailsDto {
  diagnosisId: string;
  medicalRecordId: string;
  diagnosedDate: string;
  notes: string;
  patient: {
    patientID: string;
    fullName: string;
    dateOfBirth: string;
    gender: string;
    phone: string;
    email?: string;
  };
  symptoms: string;
  createdDate: string;
  doctorId: string;
  images: Array<{
    imageId: string;
    imageName: string;
    path: string;
    uploadDate: string;
    aiAnalysis: string;
    hasAIAnalysis: boolean;
  }>;
}
export interface DiagnosisPayload {
  medicalRecordId: string;
  diagnosedDate: string;
  notes: string;
  imageFile?: File;
  imageName?: string;
}


//Danh sách bệnh nhân đang chờ và thông tin bệnh nhân
export const fetchWaitingPatients = async (): Promise<patient[]> => {
  try {
    const response = await api.get("/Doctor/waiting-patients");
    console.log("Waiting patients:", response.data); // Là một mảng
    
    // Map gender cho tất cả patients
    const mappedPatients = response.data.map((patient: any) => ({
      ...patient,
      gender: mapGender(patient.gender)
    }));
    
    return mappedPatients;
  } catch (error) {
    console.error(
      "Lỗi khi lấy danh sách bệnh nhân đang chờ:",
      error
    );
    throw error;
  }
};

//Danh sách bệnh nhân đã khám
export const fetchTreatedPatients = async (): Promise<patient[]> => {
  try {
    const response = await api.get("/Doctor/treated-patients");
    console.log("Treated patients:", response.data);
    
    // Map gender cho tất cả patients
    const mappedPatients = response.data.map((patient: any) => ({
      ...patient,
      gender: mapGender(patient.gender)
    }));
    
    return mappedPatients;
  } catch (error) {
    console.error(
      "Lỗi khi lấy danh sách bệnh nhân đã khám:",
      error
    );
    throw error;
  }
};


export const createDiagnosis = async (payload: DiagnosisPayload) => {
  const formData = new FormData();
  formData.append("MedicalRecordId", payload.medicalRecordId);
  formData.append("DiagnosedDate", payload.diagnosedDate);
  formData.append("Notes", payload.notes);

  if (payload.imageFile) {
    formData.append("ImageFile", payload.imageFile);
    formData.append("ImageName", payload.imageName || payload.imageFile.name);
  }

  const response = await api.post("/Doctor/diagnosis", formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });

  return response.data;
};

// Chỉnh sửa chẩn đoán
export const updateDiagnosis = async (diagnosisId: string, data: any) => {
  try {
    const response = await api.put(`/Doctor/doctor/${diagnosisId}`, data);
    console.log("Diagnosis updated:", response.data);
    return response.data;
  } catch (error) {
    console.error(
      "Lỗi khi cập nhật chẩn đoán:",
      error
    );
    throw error;
  }
};

// Tìm kiếm Diagnosis theo MedicalRecordId
export const fetchDiagnosisByMedicalRecordId = async (medicalRecordId: string) => {
  try {
    const response = await api.get(`/Doctor/doctor/medicalRecord/${medicalRecordId}`);
    console.log("Diagnosis by medical record:", response.data);
    return response.data;
  } catch (error) {
    console.error(
      "Lỗi khi lấy chẩn đoán theo medical record:",
      error
    );
    throw error;
  }
};
