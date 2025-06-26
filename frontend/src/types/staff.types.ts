export interface MainStaffDeclare {
    patientID: string;
    fullName: string;
    gender: string;
    phone: string;
}
export interface CreatedRecordDeclare extends MainStaffDeclare {
    createdDate: string;
    medicalRecordId: string;
}
export interface PatientForm {
    fullName: string;
    gender: string;
    dateOfBirth: string;
    phone: string;
    email: string;
    medicalHistory: string;
}
export interface PatientRecordDeclare extends PatientForm {
    id: string;
    patientID: string;
}
export interface CreateRecord {
    patientID: string;
    symptoms: string;
    assignedPhysicianId: string;
    isPriority: boolean;
}
export interface PatientHistory {
    patientId: string;
    medicalRecordId: string;
    physicianId: string;
    doctorName: string;
    createdAt: string;
}

