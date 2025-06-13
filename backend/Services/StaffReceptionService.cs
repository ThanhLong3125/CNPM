// Services/StaffReceptionService.cs
using backend.Data;
using backend.DTOs;
using backend.Exceptions;
using backend.Models;
using backend.role;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public interface IStaffReceptionService
    {
        Task<PatientDto> CreatePatientAsync(CreatePatientDto createPatientDto);
        Task<PatientDto> UpdatePatientAsync(Guid id, UpdatePatientDto updatePatientDto);
        Task SoftDeletePatientAsync(Guid id); // Added for soft deletion
        Task<PatientDto> GetPatientByIdAsync(Guid id); // Renamed for clarity
        Task<List<PatientDto>> ListAllPatientsAsync(); // Renamed and changed return type
        Task<List<PatientDto>> SearchPatientsAsync(string searchTerm); // Added for more general search
        Task<List<UserDto>> ListDoctorAsync(); // Changed return type to DTO
        Task<MedicalRecordDto> CreateMedicalRecordAsync(
            CreateMedicalRecordDto createMedicalRecordDto
        ); // Changed return type
        Task<MedicalRecordDto> GetMedicalRecordByIdAsync(Guid medicalRecordId); // Added specific get by ID
        Task<List<MedicalRecordDto>> ListMedicalRecordsByPatientIdAsync(Guid patientId); // Renamed and changed return type
        Task<MedicalRecordDto> UpdateMedicalRecordAsync(
            Guid id,
            UpdateMedicalRecordDto updateMedicalRecordDto
        ); // Added
        Task SoftDeleteMedicalRecordAsync(Guid id); // Added for soft deletion
    }

    public class StaffReceptionService : IStaffReceptionService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAuditService _auditService;

        public StaffReceptionService(
            AppDbContext context,
            IConfiguration configuration,
            IAuditService auditService
        )
        {
            _context = context;
            _configuration = configuration;
            _auditService = auditService;
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto createPatientDto)
        {
            var existingPatient = await _context.Patients.FirstOrDefaultAsync(p =>
                p.FullName.ToLower() == createPatientDto.FullName.ToLower().Trim()
                && p.DateOfBirth == createPatientDto.DateOfBirth
                && p.Gender.ToLower() == createPatientDto.Gender.ToLower().Trim()
            // Consider adding !p.IsDeleted to this check if you want to allow re-creating a soft-deleted patient
            );
            if (existingPatient != null)
            {
                throw new ConflictException("Patient already exists in the system."); // Use custom exception
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
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "Create Patient",
                    Details = System.Text.Json.JsonSerializer.Serialize(createPatientDto),
                }
            );
            return new PatientDto
            {
                Id = patient.Id,
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                ContactInfo = patient.ContactInfo,
                MedicalHistory = patient.MedicalHistory,
                // MedicalRecordCount will be calculated in a dedicated Get method if needed,
                // or loaded explicitly here if this DTO requires it on creation response.
                MedicalRecordCount = await _context.MedicalRecords.CountAsync(mr =>
                    mr.PatientId == patient.Id
                ),
            };
        }

        public async Task<PatientDto> UpdatePatientAsync(Guid id, UpdatePatientDto updatePatientDto)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p =>
                p.Id == id && !p.IsDeleted
            ); // Include IsDeleted check
            if (patient == null)
            {
                throw new NotFoundException($"Patient with ID {id} not found."); // Use custom exception
            }

            // Cập nhật các trường nếu có dữ liệu mới
            if (!string.IsNullOrEmpty(updatePatientDto.FullName))
                patient.FullName = updatePatientDto.FullName;

            if (updatePatientDto.DateOfBirth.HasValue)
            {
                patient.DateOfBirth = updatePatientDto.DateOfBirth.Value;
            }

            if (!string.IsNullOrEmpty(updatePatientDto.Gender))
                patient.Gender = updatePatientDto.Gender;

            if (!string.IsNullOrEmpty(updatePatientDto.ContactInfo))
                patient.ContactInfo = updatePatientDto.ContactInfo;

            if (!string.IsNullOrEmpty(updatePatientDto.MedicalHistory))
                patient.MedicalHistory = updatePatientDto.MedicalHistory;
            await _context.SaveChangesAsync();
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "Update Patient",
                    Details = System.Text.Json.JsonSerializer.Serialize(updatePatientDto),
                }
            );
            return new PatientDto
            {
                Id = patient.Id,
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                ContactInfo = patient.ContactInfo,
                MedicalHistory = patient.MedicalHistory,
                // MedicalRecordCount will be calculated in a dedicated Get method if needed,
                // or loaded explicitly here if this DTO requires it on creation response.
                MedicalRecordCount = await _context.MedicalRecords.CountAsync(mr =>
                    mr.PatientId == patient.Id
                ),
            };
        }

        public async Task SoftDeletePatientAsync(Guid id) // Added for soft deletion
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p =>
                p.Id == id && !p.IsDeleted
            );
            if (patient == null)
            {
                throw new NotFoundException($"Patient with ID {id} not found.");
            }

            patient.IsDeleted = true; // Set IsDeleted flag
            await _context.SaveChangesAsync();

            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = $"Soft Delete Patient (ID: {id})",
                    Details = $"Patient {id} marked as deleted.",
                }
            );
        }

        public async Task<PatientDto> GetPatientByIdAsync(Guid id) // Renamed from SearchPatientAsync
        {
            var patient = await _context
                .Patients.Include(p => p.MedicalRecords.Where(mr => !mr.IsDeleted)) // Only include non-deleted medical records
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (patient == null)
            {
                throw new NotFoundException($"Patient with ID {id} not found."); // Use custom exception
            }

            // Audit log
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = $"Get Patient by ID: {id}",
                    Details = id.ToString(),
                }
            );
            // Map to PatientDto and return
            return new PatientDto
            {
                Id = patient.Id,
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                ContactInfo = patient.ContactInfo,
                MedicalHistory = patient.MedicalHistory,
                MedicalRecordCount = patient.MedicalRecords?.Count ?? 0, // Calculate count from loaded records
            };
        }

        public async Task<List<PatientDto>> ListAllPatientsAsync() // Renamed from ListPatientAsync
        {
            var patients = await _context
                .Patients.Include(p => p.MedicalRecords.Where(mr => !mr.IsDeleted))
                .ToListAsync(); // Global filter handles IsDeleted already

            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "List All Patients",
                    Details = "Fetched all active patients.",
                }
            );

            // Map to list of PatientDto
            return patients
                .Select(patient => new PatientDto
                {
                    Id = patient.Id,
                    FullName = patient.FullName,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    ContactInfo = patient.ContactInfo,
                    MedicalHistory = patient.MedicalHistory,
                    MedicalRecordCount = patient.MedicalRecords?.Count ?? 0,
                })
                .ToList();
        }

        public async Task<List<PatientDto>> SearchPatientsAsync(string searchTerm) // Added for more general search
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await ListAllPatientsAsync(); // Return all if search term is empty
            }

            searchTerm = searchTerm.Trim().ToLower();
            var patients = await _context
                .Patients.Include(p => p.MedicalRecords.Where(mr => !mr.IsDeleted))
                .Where(p =>
                    p.FullName.ToLower().Contains(searchTerm)
                    || (p.ContactInfo != null && p.ContactInfo.ToLower().Contains(searchTerm))
                )
                .ToListAsync();

            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = $"Search Patients by term: {searchTerm}",
                    Details = $"Found {patients.Count} patients.",
                }
            );

            return patients
                .Select(patient => new PatientDto
                {
                    Id = patient.Id,
                    FullName = patient.FullName,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    ContactInfo = patient.ContactInfo,
                    MedicalHistory = patient.MedicalHistory,
                    MedicalRecordCount = patient.MedicalRecords?.Count ?? 0,
                })
                .ToList();
        }

        public async Task<MedicalRecordDto> CreateMedicalRecordAsync(
            CreateMedicalRecordDto createMedicalRecordDto
        )
        {
            var patientExists = await _context.Patients.AnyAsync(p =>
                p.Id == createMedicalRecordDto.PatientId && !p.IsDeleted
            );
            if (!patientExists)
            {
                throw new NotFoundException("Patient not found.");
            }

            // Validate Assigned Physician exists and is a Doctor
            var physician = await _context.Users.FirstOrDefaultAsync(u =>
                u.Id == createMedicalRecordDto.AssignedPhysicianId && u.Role == Role.Doctor
            );
            if (physician == null)
            {
                throw new NotFoundException("Assigned Physician not found or is not a doctor.");
            }

            var medicalRecord = new MedicalRecord
            {
                Id = Guid.NewGuid(),
                PatientId = createMedicalRecordDto.PatientId,
                CreatedDate = DateTime.UtcNow,
                Symptoms = createMedicalRecordDto.Symptoms.Trim(),
                IsPriority = createMedicalRecordDto.IsPriority,
                AssignedPhysicianId = createMedicalRecordDto.AssignedPhysicianId,
            };

            await _context.MedicalRecords.AddAsync(medicalRecord);
            await _context.SaveChangesAsync();

            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "Create Medical Record",
                    Details = System.Text.Json.JsonSerializer.Serialize(createMedicalRecordDto),
                }
            );

            // Map to MedicalRecordDto
            return new MedicalRecordDto // to.string
            {
                Id = medicalRecord.Id,
                PatientId = medicalRecord.PatientId,
                PatientName =
                    (await _context.Patients.FindAsync(medicalRecord.PatientId))?.FullName
                    ?? "Unknown", // Fetch patient name
                CreatedDate = medicalRecord.CreatedDate,
                Symptoms = medicalRecord.Symptoms,
                AssignedPhysicianId = medicalRecord.AssignedPhysicianId,
                AssignedPhysicianName = physician.Full_name, // Use the fetched physician's name
                IsPriority = medicalRecord.IsPriority,
                // DiagnosisCount is missing in your MedicalRecordDto for now
            };
        }

        public async Task<MedicalRecordDto> GetMedicalRecordByIdAsync(Guid medicalRecordId) // New method
        {
            var record = await _context
                .MedicalRecords.Include(mr => mr.Patient)
                .Include(mr => mr.User) // Assuming User is the AssignedPhysician navigation property
                .FirstOrDefaultAsync(mr => mr.Id == medicalRecordId && !mr.IsDeleted);

            if (record == null)
            {
                throw new NotFoundException($"Medical Record with ID {medicalRecordId} not found.");
            }

            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = $"Get Medical Record by ID: {medicalRecordId}",
                    Details = medicalRecordId.ToString(),
                }
            );

            return new MedicalRecordDto
            {
                Id = record.Id,
                PatientId = record.PatientId,
                PatientName = record.Patient?.FullName ?? "Unknown",
                CreatedDate = record.CreatedDate,
                Symptoms = record.Symptoms,
                AssignedPhysicianId = record.AssignedPhysicianId,
                AssignedPhysicianName = record.User?.Full_name ?? "Unknown", // Assuming User is assigned physician
                IsPriority = record.IsPriority,
            };
        }

        public async Task<MedicalRecordDto> UpdateMedicalRecordAsync(
            Guid id,
            UpdateMedicalRecordDto updateMedicalRecordDto
        ) // New method
        {
            var record = await _context.MedicalRecords.FirstOrDefaultAsync(mr =>
                mr.Id == id && !mr.IsDeleted
            );
            if (record == null)
            {
                throw new NotFoundException($"Medical Record with ID {id} not found.");
            }

            if (updateMedicalRecordDto.Symptoms != null)
                record.Symptoms = updateMedicalRecordDto.Symptoms.Trim();
            if (updateMedicalRecordDto.AssignedPhysicianId.HasValue)
            {
                // Validate new physician
                var physician = await _context.Users.FirstOrDefaultAsync(u =>
                    u.Id == updateMedicalRecordDto.AssignedPhysicianId.Value
                    && u.Role == Role.Doctor
                );
                if (physician == null)
                {
                    throw new NotFoundException("Assigned Physician not found or is not a doctor.");
                }
                record.AssignedPhysicianId = updateMedicalRecordDto.AssignedPhysicianId.Value;
            }
            if (updateMedicalRecordDto.IsPriority.HasValue)
                record.IsPriority = updateMedicalRecordDto.IsPriority.Value;

            await _context.SaveChangesAsync();

            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = $"Update Medical Record (ID: {id})",
                    Details = System.Text.Json.JsonSerializer.Serialize(updateMedicalRecordDto),
                }
            );

            return new MedicalRecordDto
            {
                Id = record.Id,
                PatientId = record.PatientId,
                PatientName =
                    (await _context.Patients.FindAsync(record.PatientId))?.FullName ?? "Unknown",
                CreatedDate = record.CreatedDate,
                Symptoms = record.Symptoms,
                AssignedPhysicianId = record.AssignedPhysicianId,
                AssignedPhysicianName =
                    (await _context.Users.FindAsync(record.AssignedPhysicianId))?.Full_name
                    ?? "Unknown",
                IsPriority = record.IsPriority,
            };
        }

        public async Task SoftDeleteMedicalRecordAsync(Guid id) // New method
        {
            var record = await _context.MedicalRecords.FirstOrDefaultAsync(mr =>
                mr.Id == id && !mr.IsDeleted
            );
            if (record == null)
            {
                throw new NotFoundException($"Medical Record with ID {id} not found.");
            }

            record.IsDeleted = true;
            await _context.SaveChangesAsync();

            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = $"Soft Delete Medical Record (ID: {id})",
                    Details = $"Medical Record {id} marked as deleted.",
                }
            );
        }

        public async Task<List<UserDto>> ListDoctorAsync() // Changed return type
        {
            var doctors = await _context
                .Users.Where(u => u.Role == backend.role.Role.Doctor && !u.IsDeleted)
                .ToListAsync(); // Assuming IsDeleted for users
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "Get list of Doctors",
                    Details = "Fetched all active doctors.",
                }
            );
            // Map to list of UserDto (you need to define this DTO)
            return doctors
                .Select(d => new UserDto
                {
                    Id = d.Id,
                    Full_name = d.Full_name,
                    Email = d.Email,
                    PhoneNumber = d.PhoneNumber,
                    Role = d.Role, // Convert enum to string
                    Specialty = d.Specialty,
                })
                .ToList();
        }

        public async Task<List<MedicalRecordDto>> ListMedicalRecordsByPatientIdAsync(Guid patientId) // Renamed from SearchlistMediaRecordbyId
        {
            var records = await _context
                .MedicalRecords.Where(r => r.PatientId == patientId && !r.IsDeleted) // Filter by PatientId and IsDeleted
                .Include(r => r.Diagnosis) // If MedicalRecordDto needs diagnosis info
                .Include(r => r.Patient)
                .Include(r => r.User) // Include assigned physician
                .ToListAsync();

            if (
                !records.Any()
                && !await _context.Patients.AnyAsync(p => p.Id == patientId && !p.IsDeleted)
            )
            {
                throw new NotFoundException($"Patient with ID {patientId} not found.");
            }

            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = $"List Medical Records for Patient ID: {patientId}",
                    Details = $"Found {records.Count} medical records.",
                }
            );

            // Map to list of MedicalRecordDto
            return records
                .Select(record => new MedicalRecordDto
                {
                    Id = record.Id,
                    PatientId = record.PatientId,
                    PatientName = record.Patient?.FullName ?? "Unknown",
                    CreatedDate = record.CreatedDate,
                    Symptoms = record.Symptoms,
                    IsPriority = record.IsPriority,
                    AssignedPhysicianId = record.AssignedPhysicianId,
                    AssignedPhysicianName = record.User?.Full_name ?? "Unknown",
                    // DiagnosisCount = record.Diagnoses?.Count ?? 0, // If you add Diagnoses collection to MedicalRecord model
                })
                .ToList();
        }
    }
}
