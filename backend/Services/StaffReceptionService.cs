using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.role;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public interface IStaffReceptionService
{
    Task<PatientDto> CreatePatientAsync(CreatePatientDto dto);
    Task<Patient> UpdatePatientAsync(string idPatient, UpdatePatientDto dto);
    Task<Patient> SreachPatientAsync(string idPatient);
    Task<List<Patient>> ListPatientAsync();
    Task<List<User>> ListDoctorAsync();
    Task<string> CreateMedicalRecordAsync(CreateMedicalRecordDto dto);
    Task<List<MedicalRecordWithDoctorDto>> SearchMedicalRecordsByPatientId(string idPatient);
    Task<MedicalRecord> DetailMediaRecordbyId(string medicalRecordId);
    Task<MedicalRecord> UpdateMediaRecordbyId(string medicalRecordId, UpdateMedicalRecordDto dto);
    Task<string> DeleteMediaRecordbyId(string medicalRecordId);
    Task<List<MedicalRecordWithPatientDto>> ShowAllMedicalRecord();
}

public class StaffReceptionService : IStaffReceptionService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    private readonly IAuditService _audit;

    public StaffReceptionService(AppDbContext context, IConfiguration config, IAuditService audit)
    {
        _context = context;
        _config = config;
        _audit = audit;
    }

    private async Task<Patient> FindPatientByIdPatientAsync(string id) =>
        await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == id)
        ?? throw new Exception("Không tìm thấy bệnh nhân.");

    public async Task<PatientDto> CreatePatientAsync(CreatePatientDto dto)
    {
        var exists = await _context.Patients.AnyAsync(p =>
            p.FullName.ToLower() == dto.FullName.ToLower().Trim() &&
            p.DateOfBirth == dto.DateOfBirth &&
            p.Gender.ToLower() == dto.Gender.ToLower().Trim());

        if (exists) throw new Exception("Bệnh nhân đã tồn tại.");

        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            IdPatient = $"BN{Guid.NewGuid():N}"[..6].ToUpper(),
            FullName = dto.FullName.Trim(),
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender.Trim(),
            Email = dto.Email?.Trim(),
            Phone = dto.Phone?.Trim(),
            MedicalHistory = dto.MedicalHistory?.Trim()
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        await _audit.WriteLogAsync(new WriteLogDto
        {
            User = "Staff",
            Action = "Create Patient",
            Details = dto
        });

        return new PatientDto
        {
            Id = patient.Id,
            PatientID = patient.IdPatient,
            FullName = patient.FullName,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            Email = patient.Email,
            Phone = patient.Phone
        };
    }

    public async Task<Patient> UpdatePatientAsync(string idPatient, UpdatePatientDto dto)
    {
        var patient = await FindPatientByIdPatientAsync(idPatient);

        patient.FullName = dto.FullName ?? patient.FullName;
        patient.DateOfBirth = dto.DateOfBirth ?? patient.DateOfBirth;
        patient.Gender = dto.Gender ?? patient.Gender;
        patient.Email = dto.Email ?? patient.Email;
        patient.Phone = dto.Phone ?? patient.Phone;
        patient.MedicalHistory = dto.MedicalHistory ?? patient.MedicalHistory;

        await _context.SaveChangesAsync();

        await _audit.WriteLogAsync(new WriteLogDto
        {
            User = "Staff",
            Action = "Update Patient",
            Details = dto
        });

        return patient;
    }

    public async Task<Patient> SreachPatientAsync(string idPatient)
    {
        var patient = await FindPatientByIdPatientAsync(idPatient);

        await _audit.WriteLogAsync(new WriteLogDto
        {
            User = "Staff",
            Action = "Search Patient by IdPatient",
            Details = idPatient
        });

        return patient;
    }

    public async Task<List<Patient>> ListPatientAsync()
    {
        var patients = await _context.Patients.ToListAsync();

        await _audit.WriteLogAsync(new WriteLogDto
        {
            User = "Staff",
            Action = "List Patients",
            Details = patients
        });

        return patients;
    }

    public async Task<List<User>> ListDoctorAsync()
    {
        var doctors = await _context.Users
            .Where(u => u.Role == Role.Doctor)
            .ToListAsync();

        await _audit.WriteLogAsync(new WriteLogDto
        {
            User = "Staff",
            Action = "List Doctors",
            Details = doctors
        });

        return doctors;
    }

    public async Task<string> CreateMedicalRecordAsync(CreateMedicalRecordDto dto)
    {
        await FindPatientByIdPatientAsync(dto.PatientId);

        var record = new MedicalRecord
        {
            Id = Guid.NewGuid(),
            MedicalRecordId = $"BA{Guid.NewGuid():N}"[..6].ToUpper(),
            PatientId = dto.PatientId,
            CreatedDate = DateTime.UtcNow,
            Symptoms = dto.Symptoms.Trim(),
            AssignedPhysicianId = dto.AssignedPhysicianId,
            IsPriority = dto.IsPriority,
            Status = false
        };

        _context.MedicalRecords.Add(record);
        await _context.SaveChangesAsync();

        await _audit.WriteLogAsync(new WriteLogDto
        {
            User = "Staff",
            Action = "Create Medical Record",
            Details = dto
        });

        return record.MedicalRecordId;
    }

    public async Task<MedicalRecord> DetailMediaRecordbyId(string id)
    {
        return await _context.MedicalRecords.FirstOrDefaultAsync(r => r.MedicalRecordId == id)
            ?? throw new Exception("Không tìm thấy bệnh án.");
    }

    public async Task<MedicalRecord> UpdateMediaRecordbyId(string id, UpdateMedicalRecordDto dto)
    {
        var record = await DetailMediaRecordbyId(id);

        record.Symptoms = dto.Symptoms ?? record.Symptoms;
        record.AssignedPhysicianId = dto.AssignedPhysicianId ?? record.AssignedPhysicianId;
        record.IsPriority = dto.IsPriority ?? record.IsPriority;
        record.Status = dto.Status;

        await _context.SaveChangesAsync();
        return record;
    }

    public async Task<string> DeleteMediaRecordbyId(string id)
    {
        var record = await DetailMediaRecordbyId(id);
        _context.MedicalRecords.Remove(record);
        await _context.SaveChangesAsync();
        return "Xoá thành công";
    }

    public async Task<List<MedicalRecordWithDoctorDto>> SearchMedicalRecordsByPatientId(string idPatient)
    {
        var patient = await FindPatientByIdPatientAsync(idPatient);

        var records = await _context.MedicalRecords
            .Where(r => r.PatientId == idPatient)
            .ToListAsync();

        var physicianIds = records
            .Select(r => r.AssignedPhysicianId)
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Distinct()
            .ToList();

        var doctors = await _context.Users
            .Where(u => physicianIds.Contains(u.PhysicianId))
            .ToDictionaryAsync(u => u.PhysicianId);

        return records.Select(r => new MedicalRecordWithDoctorDto
        {
            MedicalRecordId = r.MedicalRecordId,
            PatientId = r.PatientId,
            NamePatient = patient.FullName,
            PhysicianId = r.AssignedPhysicianId,
            DoctorName = doctors.TryGetValue(r.AssignedPhysicianId, out var doc) ? doc.Full_name : null,
            CreatedAt = DateOnly.FromDateTime(r.CreatedDate)
        }).ToList();
    }

    public async Task<List<MedicalRecordWithPatientDto>> ShowAllMedicalRecord()
    {
        var records = await _context.MedicalRecords.ToListAsync();
        var results = new List<MedicalRecordWithPatientDto>();

        foreach (var r in records)
        {
            var p = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == r.PatientId);
            if (p != null)
            {
                results.Add(new MedicalRecordWithPatientDto
                {
                    Id = r.Id.ToString(),
                    PatientId = r.PatientId,
                    MedicalRecordId = r.MedicalRecordId,
                    PhysicicanId = r.AssignedPhysicianId,
                    CreatedAt = r.CreatedDate,
                    FullName = p.FullName,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    Phone = p.Phone
                });
            }
        }

        return results;
    }
}
