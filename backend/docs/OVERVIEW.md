Model:
- [x] Patient
     - [x] Id, Full_name, DateOfBirth, Gender, ContactInfo, MedicalHistory,
           MedicalRecord, IsDeleted
- [x] MedicalRecord
     - [x] Id, PatientId, Patient, CreatedDate, AssignedPhysicianId, User, Symptoms
           IsPriority, Diagnoses, IsDeleted
- [x] Diagnosis
     - [x] Id, MedicalRecordId, MedicalRecord, DiagnosedDate, Notes, Image,
           IsDeleted
- [x] Image
     - [x] Id, Path, AIAnalysis, IsDeleted
DTO:
- [ ] PatientDTOs
CRUD, Delete means modifying the IsDeleted field
- [ ] MedicalRecordDTOs
CRUD, Delete means modifying the IsDeleted field
- [ ] DiagnosisDTOs
CRUD, Delete means modifying the IsDeleted field
- [ ] ImageDTOs 
CRUD, Delete means modifying the IsDeleted field
Service:
- [ ] ReceptionStaffService
CreatePatientAsync(CreatePatientDto dto), 
UpdatePatientAsync(Guid patientId, UpdatePatientDto dto)
GetPatientByIdAsync(Guid patientId)
SearchPatientsAsync(string searchTerm)
ListAllDoctorsAsync()
CreateMedicalRecordAsync(CreateMedicalRecordDto dto)
GetMedicalRecordByIdAsync(Guid medicalRecordId)
ListMedicalRecordsByPatientIdAsync(Guid patientId)
- [ ] DoctorService
CreateDiagnosisAsync(CreateDiagnosisDto dto)
UpdateDiagnosisAsync(Guid diagnosisId, UpdateDiagnosisDto dto)
GetDiagnosisByMedicalRecordIdAsync(Guid medicalRecordId)
ListDiagnosesByDoctorIdAsync(Guid doctorId)
- [ ] ImageService
UploadImageAsync(UploadImageDto dto, Guid diagnosisId)
DeleteImageAsync(Guid imageId)
AnalyzeImageAsync(Guid imageId)
Controller:
- [ ] ReceptionStaffController
- [ ] DoctorController
- [ ] ImageController


- [ ] Understanding how Navigation property works
add the relationship in AppdbContext

Put validation properties, [Required], [StringLength], [EmailAddress], in DTOs
Automapper


Describing the behavior

