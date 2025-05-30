using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.role; // Make sure this is correctly pointing to your Role enum
using Microsoft.EntityFrameworkCore;

// We'll also use AutoMapper to simplify object mapping.
// If you don't have it installed: dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
// And configure it in Program.cs. For now, I'll add manual mapping logic.

namespace backend.Services
{
    public interface IStaffReceptionService
    {
        Task<PatientDto> CreatePatientAsync(CreatePatientDto patientDto);
        Task<PatientDto> UpdatePatientAsync(Guid patientId, UpdatePatientDto patientDto);
        Task<PatientDto?> GetPatientByIdAsync(Guid patientId);
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();

        Task<MedicalRecordDto> CreateMedicalRecordAsync(
            CreateMedicalRecordDto recordDto,
            Guid createdByStaffId
        );
        Task<MedicalRecordDto> UpdateMedicalRecordAsync(
            Guid recordId,
            UpdateMedicalRecordDto updateDto,
            Guid updatedByStaffId
        );
        Task<MedicalRecordDto> RecordPatientSymptomsAsync(
            Guid medicalRecordId,
            string symptoms,
            Guid staffId
        );
        Task<MedicalRecordDto> AssignMedicalRecordToPhysicianAsync(
            Guid medicalRecordId,
            AssignMedicalRecordPhysicianDto assignDto,
            Guid staffId
        );
        Task<MedicalRecordDto?> GetMedicalRecordForStaffAsync(Guid medicalRecordId);
        Task<IEnumerable<MedicalRecordDto>> GetMedicalRecordsByPatientAsync(Guid patientId);

        // Utility methods (if needed in other services or controllers)
        Task<IEnumerable<UserDto>> GetAllDoctorsAsync(); // Changed to UserDto
    }

    public class StaffReceptionService : IStaffReceptionService
    {
        private readonly AppDbContext _context;

        // private readonly IConfiguration _configuration; // Removed if not used

        // Injecting AppDbContext. Remove IConfiguration if not needed.
        public StaffReceptionService(
            AppDbContext context /*, IConfiguration configuration */
        )
        {
            _context = context;
            // _configuration = configuration;
        }

        // --- Patient Operations ---

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto createPatientDto)
        {
            // Check for existing patient
            var existingPatient = await _context.Patients.FirstOrDefaultAsync(p =>
                p.FullName.ToLower() == createPatientDto.FullName.ToLower().Trim()
                && p.DateOfBirth.Date == createPatientDto.DateOfBirth.Date
                && p.Gender.ToLower() == createPatientDto.Gender.ToLower().Trim()
            );

            if (existingPatient != null)
            {
                throw new InvalidOperationException("Patient already exists in the system."); // More specific exception
            }

            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                FullName = createPatientDto.FullName.Trim(),
                DateOfBirth = createPatientDto.DateOfBirth,
                Gender = createPatientDto.Gender.Trim(),
                ContactInfo = createPatientDto.ContactInfo?.Trim(),
                MedicalHistory = createPatientDto.MedicalHistory?.Trim(),
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            // Map the created Patient model to PatientDto
            return new PatientDto
            {
                Id = patient.Id,
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                ContactInfo = patient.ContactInfo,
                MedicalHistory = patient.MedicalHistory,
            };
        }

        public async Task<PatientDto> UpdatePatientAsync(
            Guid patientId,
            UpdatePatientDto updatePatientDto
        )
        {
            var patient = await _context.Patients.FindAsync(patientId); // Renamed 'id' to 'patientId' for clarity
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found with this ID."); // More specific exception
            }

            // Update fields if new data is provided
            if (!string.IsNullOrEmpty(updatePatientDto.FullName))
                patient.FullName = updatePatientDto.FullName.Trim();

            if (updatePatientDto.DateOfBirth.HasValue)
                patient.DateOfBirth = updatePatientDto.DateOfBirth.Value;

            if (!string.IsNullOrEmpty(updatePatientDto.Gender))
                patient.Gender = updatePatientDto.Gender.Trim();

            // ContactInfo and MedicalHistory can be set to null if the DTO field is explicitly null
            // Check for null or empty string to allow clearing the field
            if (updatePatientDto.ContactInfo != null)
                patient.ContactInfo = updatePatientDto.ContactInfo.Trim();

            if (updatePatientDto.MedicalHistory != null)
                patient.MedicalHistory = updatePatientDto.MedicalHistory.Trim();

            await _context.SaveChangesAsync();

            // Map the updated Patient model to PatientDto
            return new PatientDto
            {
                Id = patient.Id,
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                ContactInfo = patient.ContactInfo,
                MedicalHistory = patient.MedicalHistory,
            };
        }

        public async Task<PatientDto?> GetPatientByIdAsync(Guid patientId) // Changed method name for consistency
        {
            var patient = await _context.Patients.FindAsync(patientId);

            if (patient == null)
            {
                return null; // Return null if not found, as per interface signature
            }

            // Map the Patient model to PatientDto
            return new PatientDto
            {
                Id = patient.Id,
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                ContactInfo = patient.ContactInfo,
                MedicalHistory = patient.MedicalHistory,
            };
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync() // Changed method name for consistency
        {
            var patients = await _context.Patients.ToListAsync();

            // Map list of Patient models to PatientDto list
            return patients
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    ContactInfo = p.ContactInfo,
                    MedicalHistory = p.MedicalHistory,
                })
                .ToList();
        }

        // --- Medical Record Operations ---

        public async Task<MedicalRecordDto> CreateMedicalRecordAsync(
            CreateMedicalRecordDto createMedicalRecordDto,
            Guid createdByStaffId
        )
        {
            var patientExists = await _context.Patients.AnyAsync(p =>
                p.Id == createMedicalRecordDto.PatientId
            );
            if (!patientExists)
            {
                throw new KeyNotFoundException("Patient not found.");
            }

            var medicalRecord = new MedicalRecord
            {
                Id = Guid.NewGuid(),
                PatientId = createMedicalRecordDto.PatientId,
                CreatedDate = DateTime.UtcNow,
                CreatedByStaffId = createdByStaffId, // Set the creator
                Symptoms = createMedicalRecordDto.Symptoms.Trim(),
                IsPriority = createMedicalRecordDto.IsPriority,
                AssignedPhysicianId = createMedicalRecordDto.AssignedPhysicianId, // Can be null
            };

            await _context.MedicalRecords.AddAsync(medicalRecord);
            await _context.SaveChangesAsync();

            // Fetch related data for DTO mapping
            var patient = await _context.Patients.FindAsync(medicalRecord.PatientId);
            var createdByStaff = await _context.Users.FindAsync(medicalRecord.CreatedByStaffId);

            // Map MedicalRecord model to MedicalRecordDto
            return new MedicalRecordDto
            {
                Id = medicalRecord.Id,
                PatientId = medicalRecord.PatientId,
                PatientName = patient?.FullName ?? "Unknown",
                CreatedDate = medicalRecord.CreatedDate,
                CreatedByStaffId = medicalRecord.CreatedByStaffId,
                CreatedByStaffName = createdByStaff?.Full_name ?? "Unknown",
                Symptoms = medicalRecord.Symptoms,
                IsPriority = medicalRecord.IsPriority,
                AssignedPhysicianId = medicalRecord.AssignedPhysicianId,
            };
        }

        public async Task<MedicalRecordDto> UpdateMedicalRecordAsync(
            Guid recordId,
            UpdateMedicalRecordDto updateDto,
            Guid updatedByStaffId
        )
        {
            var medicalRecord = await _context
                .MedicalRecords.Include(mr => mr.Patient) // Include Patient to get PatientName for DTO
                .Include(mr => mr.CreatedByStaff) // Include Creator
                .Include(mr => mr.AssignedPhysician) // Include Assigned Physician
                .FirstOrDefaultAsync(mr => mr.Id == recordId);

            if (medicalRecord == null)
            {
                throw new KeyNotFoundException("Medical record not found.");
            }

            // Apply updates from DTO
            if (updateDto.Symptoms != null)
                medicalRecord.Symptoms = updateDto.Symptoms.Trim();

            if (updateDto.IsPriority.HasValue)
                medicalRecord.IsPriority = updateDto.IsPriority.Value;

            // Staff can change assigned physician
            if (updateDto.AssignedPhysicianId.HasValue)
            {
                var newPhysician = await _context
                    .Users.Where(u =>
                        u.Id == updateDto.AssignedPhysicianId.Value && u.Role == Role.Doctor
                    )
                    .FirstOrDefaultAsync();

                if (newPhysician == null)
                {
                    throw new KeyNotFoundException(
                        "Assigned physician not found or is not a doctor."
                    );
                }
                medicalRecord.AssignedPhysicianId = newPhysician.Id;
            }
            else if (
                updateDto.AssignedPhysicianId == null
                && medicalRecord.AssignedPhysicianId != null
            )
            {
                // If assigned physician is explicitly set to null (e.g. unassign)
                medicalRecord.AssignedPhysicianId = null;
            }

            medicalRecord.LastUpdatedAt = DateTime.UtcNow;
            medicalRecord.LastUpdatedByUserId = updatedByStaffId;

            await _context.SaveChangesAsync();

            // Map to DTO
            return new MedicalRecordDto
            {
                Id = medicalRecord.Id,
                PatientId = medicalRecord.PatientId,
                PatientName = medicalRecord.Patient?.FullName ?? "Unknown",
                CreatedDate = medicalRecord.CreatedDate,
                CreatedByStaffId = medicalRecord.CreatedByStaffId,
                CreatedByStaffName = medicalRecord.CreatedByStaff?.Full_name ?? "Unknown",
                Symptoms = medicalRecord.Symptoms,
                IsPriority = medicalRecord.IsPriority,
                AssignedPhysicianId = medicalRecord.AssignedPhysicianId,
                AssignedPhysicianName = medicalRecord.AssignedPhysician?.Full_name,
            };
        }

        public async Task<MedicalRecordDto> RecordPatientSymptomsAsync(
            Guid medicalRecordId,
            string symptoms,
            Guid staffId
        )
        {
            var medicalRecord = await _context
                .MedicalRecords.Include(mr => mr.Patient)
                .Include(mr => mr.CreatedByStaff)
                .Include(mr => mr.AssignedPhysician)
                .FirstOrDefaultAsync(mr => mr.Id == medicalRecordId);

            if (medicalRecord == null)
            {
                throw new KeyNotFoundException("Medical record not found.");
            }

            // Only update symptoms (and potentially status/audit fields)
            medicalRecord.Symptoms = symptoms.Trim();
            medicalRecord.LastUpdatedAt = DateTime.UtcNow;
            medicalRecord.LastUpdatedByUserId = staffId;

            await _context.SaveChangesAsync();

            // Map to DTO
            return new MedicalRecordDto
            {
                Id = medicalRecord.Id,
                PatientId = medicalRecord.PatientId,
                PatientName = medicalRecord.Patient?.FullName ?? "Unknown",
                CreatedDate = medicalRecord.CreatedDate,
                CreatedByStaffId = medicalRecord.CreatedByStaffId,
                CreatedByStaffName = medicalRecord.CreatedByStaff?.Full_name ?? "Unknown",
                Symptoms = medicalRecord.Symptoms,
                IsPriority = medicalRecord.IsPriority,
                AssignedPhysicianId = medicalRecord.AssignedPhysicianId,
                AssignedPhysicianName = medicalRecord.AssignedPhysician?.Full_name,
            };
        }

        public async Task<MedicalRecordDto> AssignMedicalRecordToPhysicianAsync(
            Guid medicalRecordId,
            AssignMedicalRecordPhysicianDto assignDto,
            Guid staffId
        )
        {
            var medicalRecord = await _context
                .MedicalRecords.Include(mr => mr.Patient)
                .Include(mr => mr.CreatedByStaff)
                .Include(mr => mr.AssignedPhysician) // Include existing physician if any
                .FirstOrDefaultAsync(mr => mr.Id == medicalRecordId);

            if (medicalRecord == null)
            {
                throw new KeyNotFoundException("Medical record not found.");
            }

            var physician = await _context
                .Users.Where(u => u.Id == assignDto.PhysicianId && u.Role == Role.Doctor)
                .FirstOrDefaultAsync();

            if (physician == null)
            {
                throw new KeyNotFoundException("Physician not found or is not a doctor.");
            }

            medicalRecord.AssignedPhysicianId = physician.Id;
            medicalRecord.PhysicianNotes = assignDto.Notes?.Trim(); // Store assignment notes if any
            medicalRecord.LastUpdatedAt = DateTime.UtcNow;
            medicalRecord.LastUpdatedByUserId = staffId;

            await _context.SaveChangesAsync();

            // Map to DTO
            return new MedicalRecordDto
            {
                Id = medicalRecord.Id,
                PatientId = medicalRecord.PatientId,
                PatientName = medicalRecord.Patient?.FullName ?? "Unknown",
                CreatedDate = medicalRecord.CreatedDate,
                CreatedByStaffId = medicalRecord.CreatedByStaffId,
                CreatedByStaffName = medicalRecord.CreatedByStaff?.Full_name ?? "Unknown",
                Symptoms = medicalRecord.Symptoms,
                IsPriority = medicalRecord.IsPriority,
                AssignedPhysicianId = medicalRecord.AssignedPhysicianId,
                AssignedPhysicianName = physician.Full_name, // Use the newly assigned physician's name
            };
        }

        public async Task<MedicalRecordDto?> GetMedicalRecordForStaffAsync(Guid medicalRecordId)
        {
            var medicalRecord = await _context
                .MedicalRecords.Include(mr => mr.Patient) // Include Patient for patient name
                .Include(mr => mr.CreatedByStaff) // Include the staff who created it
                .Include(mr => mr.AssignedPhysician) // Include assigned doctor
                .FirstOrDefaultAsync(mr => mr.Id == medicalRecordId);

            if (medicalRecord == null)
            {
                return null;
            }

            // Map to DTO
            return new MedicalRecordDto
            {
                Id = medicalRecord.Id,
                PatientId = medicalRecord.PatientId,
                PatientName = medicalRecord.Patient?.FullName ?? "Unknown",
                CreatedDate = medicalRecord.CreatedDate,
                CreatedByStaffId = medicalRecord.CreatedByStaffId,
                CreatedByStaffName = medicalRecord.CreatedByStaff?.Full_name ?? "Unknown",
                Symptoms = medicalRecord.Symptoms,
                IsPriority = medicalRecord.IsPriority,
                AssignedPhysicianId = medicalRecord.AssignedPhysicianId,
                AssignedPhysicianName = medicalRecord.AssignedPhysician?.Full_name,
            };
        }

        public async Task<IEnumerable<MedicalRecordDto>> GetMedicalRecordsByPatientAsync(
            Guid patientId
        )
        {
            var medicalRecords = await _context
                .MedicalRecords.Where(mr => mr.PatientId == patientId)
                .Include(mr => mr.Patient) // Include Patient to ensure PatientName is available
                .Include(mr => mr.CreatedByStaff)
                .Include(mr => mr.AssignedPhysician)
                .OrderByDescending(mr => mr.CreatedDate) // Order by creation date for better view
                .ToListAsync();

            // Map list of MedicalRecord models to MedicalRecordDto list
            return medicalRecords
                .Select(mr => new MedicalRecordDto
                {
                    Id = mr.Id,
                    PatientId = mr.PatientId,
                    PatientName = mr.Patient?.FullName ?? "Unknown",
                    CreatedDate = mr.CreatedDate,
                    CreatedByStaffId = mr.CreatedByStaffId,
                    CreatedByStaffName = mr.CreatedByStaff?.Full_name ?? "Unknown",
                    Symptoms = mr.Symptoms,
                    IsPriority = mr.IsPriority,
                    AssignedPhysicianId = mr.AssignedPhysicianId,
                    AssignedPhysicianName = mr.AssignedPhysician?.Full_name,
                })
                .ToList();
        }

        // --- Utility Methods ---
        public async Task<IEnumerable<UserDto>> GetAllDoctorsAsync()
        {
            // Now, u.Role will be type 'Role' and Role.Doctor will be type 'Role', allowing comparison
            var doctors = await _context.Users.Where(u => u.Role == Role.Doctor).ToListAsync();

            return doctors
                .Select(d => new UserDto
                {
                    Id = d.Id,
                    Full_name = d.Full_name,
                    Email = d.Email,
                    Role = d.Role.ToString(), // Here, we correctly convert the enum to a string for the DTO
                    Specialty = d.Specialty,
                })
                .ToList();
        }
    }
}

