export interface patientAwait {
    patient_id: string;
    medicalRecord_id: string; // mã bệnh án
    name: string;
    gender: string;
    timeIn: string;
}
export interface patientExamined extends patientAwait {
    attendDoctor: string;

}
export interface medicalRecord {
    medicalRecord_id: string;
    patient_id: string;
    createDate: Date;
    symptoms: string;
    note: string;
    preliminaryDiagnosis: string; // Chẩn đoán sơ bộ
    finalDiagnosis: string;       // Chẩn đoán cuối cùng
    treatmentDirection: string;   // Hướng điều trị
}
export interface patientInfor extends patientAwait {
    dateOfBirth: Date;
    contactInfor: string;


}
export interface DoctorInfor extends patientExamined {
    doctor_id: string;           // Mã bác sĩ
    department: string;          // Khoa khám
    room: string;                // Phòng khám
}

export interface medicalExam {
    weight: number;
    height: number;
    temperature: number;
    pulse: number;
    bloodPressure: string;
    breathingRate: number;

    reasonForVisit: string;
    currentCondition: string;
    medicalHistory: string;
}