using System.Security.Claims;
using Asp.Versioning;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Students;

namespace Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public class StudentsController(IStudentService students) : ControllerBase
{
    private readonly IStudentService _students = students;

    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetAll()
    {
        // TODO: wire repository query
        return Ok(Array.Empty<Student>());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
    {
        var createdBy = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var uid) ? uid : 0;

        var student = new Student
        {
            NationalId          = request.NationalId,
            DocumentType        = request.DocumentType,
            FirstName           = request.FirstName,
            MiddleName          = request.MiddleName,
            FirstLastName       = request.FirstLastName,
            SecondLastName      = request.SecondLastName,
            Email               = request.Email,
            Phone               = request.Phone,
            PhoneType           = request.PhoneType,
            Gender              = request.Gender,
            MaritalStatus       = request.MaritalStatus,
            Comment             = request.Comment,
            RegistrationDate    = request.RegistrationDate,
            NationalityId       = request.NationalityId,
            Enrollment          = request.Enrollment,
            MinistryId          = request.MinistryId,
            Photo               = request.Photo,
            BirthDate           = request.BirthDate,
            PreferredShift      = request.PreferredShift,
            Allergies           = request.Allergies,
            Illnesses           = request.Illnesses,
            HealthCondition     = request.HealthCondition,
            OtherDifficulties   = request.OtherDifficulties,
            HealthComment       = request.HealthComment,
            SiblingNumber       = request.SiblingNumber,
            OrderNumber         = request.OrderNumber,
            HasConcession       = request.HasConcession,
            HasMedicalLicense   = request.HasMedicalLicense,
            HasPendings         = request.HasPendings,
            CircularSending     = request.CircularSending,
            AllowPreviousYearsGrading  = request.AllowPreviousYearsGrading,
            AdmissionFeeAction  = request.AdmissionFeeAction,
            EnrollmentType      = request.EnrollmentType,
            DoesNotRequireAdmission = request.DoesNotRequireAdmission,
            FamilyId            = request.FamilyId,
            CurrentCourseId     = request.CurrentCourseId,
            AccountId           = request.AccountId,
            MedicalReferenceId  = request.MedicalReferenceId,
            TaxpayerId          = request.TaxpayerId,
            ReceiptTypeId       = request.ReceiptTypeId,
            PreviousSchoolId    = request.PreviousSchoolId,
            BirthCityId         = request.BirthCityId,
            BloodTypeId         = request.BloodTypeId
        };

        var created = await _students.CreateAsync(student, createdBy);
        return Created($"/api/v1/students/{created.Id}", new { created.Id, created.Enrollment });
    }
}

public record CreateStudentRequest(
    string FirstName,
    string FirstLastName,
    string Enrollment,
    int NationalityId,
    int FamilyId,
    int CurrentCourseId,
    int AccountId,
    int ReceiptTypeId,
    Gender Gender,
    PersonDocumentType DocumentType,
    Shift PreferredShift,
    AdmissionFeeAction AdmissionFeeAction,
    EnrollmentType EnrollmentType,
    CircularSending CircularSending,
    string? NationalId = null,
    string? MiddleName = null,
    string? SecondLastName = null,
    string? Email = null,
    string? Phone = null,
    PhoneType PhoneType = PhoneType.Personal,
    MaritalStatus MaritalStatus = MaritalStatus.Single,
    string? Comment = null,
    DateTime? RegistrationDate = null,
    string? MinistryId = null,
    string? Photo = null,
    DateTime? BirthDate = null,
    string? Allergies = null,
    string? Illnesses = null,
    string? HealthCondition = null,
    string? OtherDifficulties = null,
    string? HealthComment = null,
    int SiblingNumber = 1,
    int OrderNumber = 0,
    bool HasConcession = false,
    bool HasMedicalLicense = false,
    bool HasPendings = false,
    bool AllowPreviousYearsGrading = false,
    bool DoesNotRequireAdmission = false,
    int? MedicalReferenceId = null,
    int? TaxpayerId = null,
    int? PreviousSchoolId = null,
    int? BirthCityId = null,
    int? BloodTypeId = null
);
