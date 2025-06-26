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
