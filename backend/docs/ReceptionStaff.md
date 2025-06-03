# Reception Staff Module Documentation

## 1. User Requirements (Reception Staff)

### English
**Reception Staff** are responsible for:
- Creating and updating patient records.
- Recording patients' symptoms.
- Forwarding patient records to the appropriate physician based on symptoms and specialties.

### Vietnamese
**Nhân viên tiếp nhận** chịu trách nhiệm:
- Tạo mới và cập nhật hồ sơ bệnh nhân.
- Ghi nhận triệu chứng của bệnh nhân.
- Chuyển hồ sơ bệnh nhân đến bác sĩ phù hợp dựa trên triệu chứng và chuyên khoa.

---

## 2. Use Case Diagram (Reception Staff)

**Actors:** Reception Staff, System, Physician

**Main Use Cases:**
- Create Patient Record
- Update Patient Record
- Record Symptoms
- Forward Record to Physician

### Use Case: Create Patient Record
- **Actor:** Reception Staff
- **Precondition:** Patient arrives at hospital.
- **Main Flow:**
  1. Reception Staff enters patient's personal information (name, DOB, gender, contact, etc.).
  2. System saves the new patient record.
- **Postcondition:** Patient record is created in the system.

### Use Case: Update Patient Record
- **Actor:** Reception Staff
- **Precondition:** Patient record exists.
- **Main Flow:**
  1. Reception Staff searches for the patient.
  2. Reception Staff updates information as needed.
  3. System saves the changes.
- **Postcondition:** Patient record is updated.

### Use Case: Record Symptoms
- **Actor:** Reception Staff
- **Precondition:** Patient record exists.
- **Main Flow:**
  1. Reception Staff records patient's symptoms.
  2. System links symptoms to the patient record.
- **Postcondition:** Symptoms are saved.

### Use Case: Forward Record to Physician
- **Actor:** Reception Staff
- **Precondition:** Patient record and symptoms are available.
- **Main Flow:**
  1. Reception Staff selects appropriate physician/specialty.
  2. System forwards the record.
- **Postcondition:** Physician receives the patient record.

---

## 3. Functional Requirements (Reception Staff)

### English
- The system must allow Reception Staff to create new patient records with all required fields.
- The system must allow Reception Staff to search and update existing patient records.
- The system must provide a form for entering and saving patient symptoms.
- The system must allow Reception Staff to assign/forward patient records to physicians based on symptoms and specialties.

### Vietnamese
- Hệ thống phải cho phép nhân viên tiếp nhận tạo mới hồ sơ bệnh nhân với đầy đủ các trường thông tin cần thiết.
- Hệ thống phải cho phép nhân viên tiếp nhận tìm kiếm và cập nhật hồ sơ bệnh nhân đã có.
- Hệ thống phải cung cấp biểu mẫu để nhập và lưu triệu chứng của bệnh nhân.
- Hệ thống phải cho phép nhân viên tiếp nhận chuyển hồ sơ bệnh nhân đến bác sĩ phù hợp dựa trên triệu chứng và chuyên khoa.

---

## 4. Sample UI Mockup (Reception Staff)

- **Patient Registration Form:** Name, DOB, Gender, Contact, Address, etc.
- **Symptom Entry Form:** List of symptoms, duration, severity, notes.
- **Patient Search & Update:** Search bar, list of patients, edit button.
- **Forward to Physician:** Dropdown to select specialty/physician, forward button.

---

## 5. API Endpoints (Sample)

- `POST /api/patients` – Create new patient
- `PUT /api/patients/{id}` – Update patient info
- `POST /api/patients/{id}/symptoms` – Add symptoms
- `POST /api/patients/{id}/forward` – Forward to physician

---

## 6. Database Design (Reception Staff Related)

**Tables:**
- `Patients` (ID, Name, DOB, Gender, Contact, Address, etc.)
- `Symptoms` (ID, PatientID, Description, Date, Severity, Notes)
- `PatientAssignments` (ID, PatientID, PhysicianID, DateAssigned, Status)

---

## 7. Example User Story

**As a Reception Staff, I want to quickly register new patients and record their symptoms, so that physicians can promptly receive and review the information for diagnosis.**

---

Nếu cần chi tiết hơn về sơ đồ UML, API, hoặc code mẫu, hãy liên hệ lại! 