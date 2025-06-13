Model:
- [x] Patient
     - [x] Id, Full_name, DateOfBirth, Gender, ContactInfo, MedicalHistory,
           MedicalRecord, IsDeleted
- [ ] MedicalRecord
     - [ ] Id, PatientId, Patient, CreatedDate, AssignedPhysicianId, Symptoms,
           IsPriority, Diagnoses, IsDeleted
- [ ] Diagnosis
     - [ ] Id, MedicalRecordId, MedicalRecord, DiagnosedDate, Notes, Image,
           IsDeleted
- [ ] Image
     - [ ] Id, Path, AIAnalysis, IsDeleted
DTO:
- [ ] PatientDTOs
CRUD, Delete means modifing the IsDeleted field
- [ ] MedicalRecordDTOs
CRUD, Delete means modifing the IsDeleted field
- [ ] DiagnosisDTOs
CRUD, Delete means modifing the IsDeleted field
- [ ] ImageDTOs 
CRUD, Delete means modifing the IsDeleted field
Service:
- [ ] ReceptionStaffService
- [ ] DoctorService
Controller:
- [ ] ReceptionStaffController
- [ ] DoctorController


- [ ] Understanding how Navigation property works
add the relationship in AppdbContext

Put validation properties, [Required], [StringLength], [EmailAddress], in DTOs
Automapper

