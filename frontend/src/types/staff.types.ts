export interface MainStaffDeclare {
    patientID: string;
    name:string;
    gender:string;
    phone:string;
}
export interface CreatedRecordDeclare extends MainStaffDeclare {
    timeIn: string;
}
export interface PatientForm {
    fullName:string;
    gender:string;
    birthDate:string;
    phoneOrEmail:string;
    medicalHistory: string;
}
export interface PatientRecordDeclare extends PatientForm {
    patientID: string;
}