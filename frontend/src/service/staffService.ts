
import api from "../service/apiService"
import { mapGender } from "../utils/formatters";
import type {
  PatientForm,
  MainStaffDeclare,
  CreateRecord,
  CreatedRecordDeclare,
} from "../types/staff.types";

//API tạo hồ sơ bệnh nhân
export async function createPatient(newPatient: PatientForm): Promise<boolean> {
  try {
    const response = await api.post("/StaffReception/patients", newPatient);

    return response.status >= 200 && response.status < 300;
  } catch (error: unknown) {
    console.error(" Tạo bệnh nhân thất bại:", error);
    return false;
  }
}

//API hiển thị bảng ở MainStaff
export const fetchPatients = async (): Promise<MainStaffDeclare[]> => {
  try {
    const response = await api.get("/StaffReception/patients");

    const patients: MainStaffDeclare[] = response.data.map((p: any) => ({
      patientID: p.idPatient,
      fullName: p.fullName,
      gender: mapGender(p.gender),
      phone: p.phone,
    }));

    return patients;
  } catch (error) {
    console.error("Lỗi khi gọi API bệnh nhân:", error);
    return [];
  }
};

//API hiển thị ở PatientRecord
export const fetchPatientsDetail = async (): Promise<any[]> => {
  try {
    const res = await api.get("/StaffReception/patients");
    
    // Map gender cho tất cả patients
    const mappedPatients = res.data.map((patient: any) => ({
      ...patient,
      gender: mapGender(patient.gender)
    }));
    
    return mappedPatients || [];
  } catch (error) {
    console.error("Lỗi khi gọi API fetchPatientsDetail:", error);
    return [];
  }
};

//Tìm theo ID
export async function fetchPatientById(idPatient: string): Promise<any | null> {
  try {
    const res = await api.get(`/StaffReception/patients/${idPatient}`);
    
    // Map gender cho patient
    if (res.data) {
      res.data.gender = mapGender(res.data.gender);
    }
    
    return res.data;
  } catch (error) {
    console.error("Lỗi khi gọi chi tiết bệnh nhân:", error);
    return null;
  }
}

//Cập nhật thông tin bệnh nhân
export async function updatePatientById(
  idPatient: string,
  updatedData: PatientForm
): Promise<boolean> {
  try {
    const response = await api.put(`/StaffReception/patients/${idPatient}`, updatedData);
    return response.status >= 200 && response.status < 300;
  } catch (error: unknown) {
    console.error("Chi tiết lỗi cập nhật:", error);
    return false;
  }
}

//API tạo bệnh án
export const createMedicalRecord = async (payload: CreateRecord) => {
  try {
    const res = await api.post("/StaffReception/medicalrecords", payload);
    return res.status === 201 || res.status === 200;
  } catch (error: unknown) {
    console.error("Lỗi tạo bệnh án:", error);
    return false;
  }
};

// Xem lịch sử bệnh án của bệnh nhân
export async function fetchHistoryPatientById(idPatient: string): Promise<any | null> {
  try {
    const res = await api.get(`/StaffReception/medicalrecords/patient/${idPatient}`);
    
    // Map gender cho tất cả records
    if (res.data && Array.isArray(res.data)) {
      res.data = res.data.map((record: any) => ({
        ...record,
        gender: mapGender(record.gender)
      }));
    }
    
    return res.data;
  } catch (error) {
    console.error("Lỗi khi gọi chi tiết lịch sử bệnh án của bệnh nhân:", error);
    return null;
  }
}

// Xem lịch sử bệnh án đã tạo
export const fetchAllHistoryRecord = async (): Promise<CreatedRecordDeclare[]> => {
  try {
    const response = await api.get("/StaffReception/medicalrecords");

    const history: CreatedRecordDeclare[] = response.data.map((p: any) => ({
      patientID: p.patientId,                
      medicalRecordId: p.medicalRecordId,
      fullName: p.fullName,
      gender: mapGender(p.gender),
      phone: p.phone,
      createdDate: p.createdAt,                 
    }));

    return history;
  } catch (error) {
    console.error("Lỗi khi gọi API bệnh án:", error);
    return [];
  }
};

// Xem chi tiết bệnh án đã tạo
export const fetchMedicalRecordById = async (id: string) => {
  try {
    const res = await api.get(`/StaffReception/medicalrecords/${id}`);
    
    // Map gender nếu có
    if (res.data && res.data.gender) {
      res.data.gender = mapGender(res.data.gender);
    }
    
    return res.data;
  } catch (error: unknown) {
    console.error("Lỗi khi lấy chi tiết bệnh án:", error);
    throw error;
  }
};

// Xoá bệnh án
export const deleteMedicalRecord = async (medicalRecordId: string): Promise<boolean> => {
  try {
    const res = await api.delete(`/StaffReception/medicalrecords/${medicalRecordId}`);
    return res.status === 200 || res.status === 204;
  } catch (error: unknown) {
    console.error(" Lỗi khi xoá bệnh án:", error)
    return false;
  }
};

// Cập nhật bệnh án
export const updateMedicalRecord = async (
  medicalRecordId: string,
  updatedData: {
    symptoms: string;
    assignedPhysicianId: string;
    isPriority: boolean;
  }
): Promise<boolean> => {
  try {
    const res = await api.put(`/StaffReception/medicalrecords/${medicalRecordId}`, updatedData);
    return res.status >= 200 && res.status < 300;
  } catch (error: unknown) {
    console.error("Lỗi khi cập nhật bệnh án:", error)
    return false;
  }
};

//Lịch sử bệnh án của một bệnh nhân.
export const fetchMedicalRecordsByPatientId = async (idPatient: string) => {
  try {
    const response = await api.get(`/StaffReception/medicalrecords/patient/${idPatient}`);
    
    // Map gender cho tất cả records
    if (response.data && Array.isArray(response.data)) {
      response.data = response.data.map((record: any) => ({
        ...record,
        gender: mapGender(record.gender)
      }));
    }
    
    return response.data;
  } catch (error: unknown) {
    console.error(" Lỗi khi lấy lịch sử bệnh án:", error);
    return null;
  }
};

//Lấy danh sách bác sĩ.
export const fetchDoctor = async (): Promise<any[]> => {
  try {
    const res = await api.get("/StaffReception/doctors");
    return res.data || [];
  } catch (error) {
    console.error("Lỗi khi gọi API fetchDoctor:", error);
    return [];
  }
};

//Lấy danh sách bệnh nhân đang chờ khám
export const fetchWaitingPatients = async (): Promise<any[]> => {
  try {
    const response = await api.get("/StaffReception/waiting-patients");
    console.log("Waiting patients:", response.data);
    
    // Map gender cho tất cả patients
    const mappedPatients = response.data.map((patient: any) => ({
      ...patient,
      gender: mapGender(patient.gender)
    }));
    
    return mappedPatients;
  } catch (error: unknown) {
    console.error("Lỗi khi lấy danh sách bệnh nhân đang chờ:", error);
    return [];
  }
};

//Lấy danh sách bệnh nhân đã được khám
export const fetchTreatedPatients = async (): Promise<any[]> => {
  try {
    const response = await api.get("/StaffReception/treated-patients");
    console.log("Treated patients:", response.data);
    
    // Map gender cho tất cả patients
    const mappedPatients = response.data.map((patient: any) => ({
      ...patient,
      gender: mapGender(patient.gender)
    }));
    
    return mappedPatients;
  } catch (error: unknown) {
    console.error("Lỗi khi lấy danh sách bệnh nhân đã khám:", error);
    return [];
  }
};
