I'm writing the backend for a hospital website that lets staff and doctor do
CRUD stuff on patients and medical records. And there'd be AI(gemini) anaylysis on images.


There's be images involved, usually people would use something like firebase or
azure to host there image(post it and it would spit and the link). But my friend
has his own server and he's telling me to at first try to get the "save image"
feature working, that is, moving the uploaded image to a folder in my computer,
so when the back end is hosted on his server the "folder in my computer" would
be the server's storage.

The reason for this is this is a college group project, so we'd only upload
maybe 1 or 2 images for demonstration. 


This is the context of the project:
I'm not gonna give the Admin detail since It's not related to my part of the job.
enum:Role: Staff,Doctor,Admin
Models : 
Users: Full_name,Email,PasswordHash,PhoneNumber,Role(Role),Specialty
Patient : Id,Fullname,DateOfBirth,Gender,ContactInfo,MedicalHistory,MedicalRecord(navigation property)
MedicalRecord: Id,PatientId,Patient(navigation property),CreatedDate,AssignedPhysicianId,Symptoms,IsPriority,Diagnosis(navigation property,only 1)
Diagnosis: ID,MedicalRecordId,MedicalRecord(Navigation property),DiagnosedDate,Notes,ImageThatInformedDiagnosis(navigation property,nullable)
Image:Id,Path,AIAnalysis,Diagnosis(navigation property)

DTOs:
PatientDTO: CreatePatientDto,UpdatePatientDto
MedicalRecordDTO:CreateMedicalRecordDto,UpdateMedicalRecordDto
DiagnosisDTO: CreateDiagnosisDto,UpdateDiagnosisDto
Image: I don't know what to put here

Service: Just calling those DTOs
Controller :Calling the Services



Question:
How do I see the name of the AssignedPhysician when GET(http request) the MedicalRecord?
What Should I do for the Image(model,dto,service)?
Is there anything that needs improvement?
Give me a step by step instruction, DO NOT PROVIDE ANY CODE. I want to understand the project first.


# New prompt
I'm using ASP.NET . The flow of my project is Models->DTOs->Service->Controller

Here's my Image Model and DTOs.

Image.cs (Models/)
```cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Path { get; set; } = string.Empty;

        [Required]
        public Guid DiagnosisId { get; set; } // Foreign Key to Diagnosis

        [ForeignKey("DiagnosisId")]
        public Diagnosis? Diagnosis { get; set; } // Navigation property

        [Required]
        public DateTime UploadDate { get; set; }
        public string? AIAnalysis { get; set; }
    }

}

```

ImageDTOs.cs

```cs
using System.ComponentModel.DataAnnotations;
namespace backend.DTOs
{

    public class UploadImageDto
    {
        [Required(ErrorMessage = "An image file is required.")]
        public IFormFile File { get; set; } = null!; // This will hold the actual image data

        [Required(ErrorMessage = "Diagnosis ID is required.")]
        public Guid DiagnosisId { get; set; }
    }


    public class ImageDto
    {
        public Guid Id { get; set; }
        public DateTime UploadDate { get; set; }
        public string AIAnalysis { get; set; } = string.Empty;
        public int DiagnosisId { get; set; }
    }
}

```

What I want is to "upload" an image , Since I'm running locally, I simply want to copy that image to another directory in my computer. When I host it on the server, I'll change the directory to the server, but for now, implement the "upload" function for me in the Service and Controller

