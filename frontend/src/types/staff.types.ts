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
    name:string;
    gender:string;
    birthDate:string;
    phoneOrEmail:string;
    medicalHistory: string;
}