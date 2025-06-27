import api from "../service/apiSerive";
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
  } catch (error: any) {
    const errors = error?.response?.data?.errors;
    console.error(" Tạo bệnh nhân thất bại:", errors || error.message || error);
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
      gender:
        p.gender.toLowerCase() === "male"
          ? "Nam"
          : p.gender.toLowerCase() === "female"
            ? "Nữ"
            : p.gender,

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
    return res.data || [];
  } catch (error) {
    console.error("Lỗi khi gọi API fetchPatientsDetail:", error);
    return [];
  }
};
//Tìm theo ID
export async function fetchPatientById(idPatient: string): Promise<any | null> {
  try {
    const res = await api.get(`/StaffReception/patients/${idPatient}`);
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
  } catch (error: any) {
    console.error("Chi tiết lỗi cập nhật:", error?.response?.data?.errors || error);
    return false;
  }
}

//API tạo bệnh án
export const createMedicalRecord = async (payload: CreateRecord) => {
  try {
    const res = await api.post("/StaffReception/medicalrecords", payload);
    return res.status === 201 || res.status === 200;
  } catch (error: any) {
    console.error("Lỗi tạo bệnh án:", error.response?.data);
    return false;
  }
};
// Xem lịch sử bệnh án của bệnh nhân
export async function fetchHistoryPatientById(idPatient: string): Promise<any | null> {
  try {
    const res = await api.get(`/StaffReception/medicalrecords/patient/${idPatient}`);
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
      gender: p.gender,
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
  const res = await api.get(`/StaffReception/medicalrecords/${id}`);
  return res.data;
};
// Xoá bệnh án
export const deleteMedicalRecord = async (medicalRecordId: string): Promise<boolean> => {
  try {
    const res = await api.delete(`/StaffReception/medicalrecords/${medicalRecordId}`);
    return res.status === 200 || res.status === 204;
  } catch (error: any) {
    console.error(" Lỗi khi xoá bệnh án:", error?.response?.data || error.message);
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
  } catch (error: any) {
    console.error("Lỗi khi cập nhật bệnh án:", error?.response?.data || error.message);
    return false;
  }
};
//Lịch sử bệnh án của một bệnh nhân.
export const fetchMedicalRecordsByPatientId = async (idPatient: string) => {
  try {
    const response = await api.get(`/StaffReception/medicalrecords/patient/${idPatient}`);
    return response.data;
  } catch (error: any) {
    console.error(" Lỗi khi lấy lịch sử bệnh án:", error.response?.status, error.response?.data);
    return null;
  }
};
//Lấy danh sách bác sĩ.
export const fetchDoctor = async (): Promise<any[]> => {
  try {
    const res = await api.get("/StaffReception/doctors");
    return res.data || [];``
  } catch (error) {
    console.error("Lỗi khi gọi API fetchDoctor:", error);
    return [];
  }
};