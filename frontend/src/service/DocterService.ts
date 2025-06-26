import api from "./apiSerive";

// Chinh sua chan doan
export async function editdiagnosisId(
    diagnosisId: string
): Promise<boolean> {
    try {
        const response = await api.put(`/Doctor/doctor/${diagnosisId}`, updatedData);
        return response.status >= 200 && response.status < 300;
    } catch (error: any) {
        console.error("Chi tiết lỗi cập nhật:", error?.response?.data?.errors || error);
        return false;
    }
}

// Tao chan doan
export async function createDiagnosis(newDiagnosis: PaientForm): Promise<boolean> {
    try {
        const response = await api.post("/Doctor/doctor", newDiagnosis);

        return response.status >= 200 && response.status < 300;
    } catch (error: any) {
        const errors = error?.response?.data?.errors;
        console.error(" Tạo chẩn đoán thất bại: ", errors || error.message || error);
        return false;
    }
}
// Tìm kiếm Diagnosis theo MedicalRecordId

export const fetchPatientsDetail = async (medicalRecordId: String): Promise<any[]> => {
    try {
        const res = await api.get(`/Doctor/doctor/medicalRecord/${medicalRecordId}`);
        return res.data || [];
    } catch (error) {
        console.error("Lỗi khi tìm kiếm: ", error);
        return [];
    }
};


