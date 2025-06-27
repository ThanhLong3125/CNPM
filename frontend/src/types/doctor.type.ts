export interface patientAwait {
    patientId:string;
    medicalRecordId:string;
    fullName:string;
    gender:string;
    createdAt:string;
}
export interface patientExamined extends patientAwait {
    attendDoctor: string;

}
