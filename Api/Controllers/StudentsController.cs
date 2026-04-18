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
    public async Task<ActionResult<IEnumerable<Student>>> GetAll([FromQuery] string? search = null)
    {
        var result = await _students.GetAllAsync(search);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Student>> GetById(int id)
    {
        var student = await _students.GetByIdAsync(id);
        return student is null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudentRequest request)
    {
        var userId = GetUserId();
        var created = await _students.CreateAsync(MapToStudent(request), userId);
        return Created($"/api/v1/students/{created.Id}", new { created.Id, created.Enrollment });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] StudentRequest request)
    {
        var userId = GetUserId();
        var updated = await _students.UpdateAsync(id, MapToStudent(request), userId);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _students.DeleteAsync(id, GetUserId());
        return deleted ? NoContent() : NotFound();
    }

    private int GetUserId() =>
        int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var uid) ? uid : 0;

    private static Student MapToStudent(StudentRequest r) => new()
    {
        NationalId                 = r.NationalId,
        DocumentType               = r.DocumentType,
        FirstName                  = r.FirstName,
        MiddleName                 = r.MiddleName,
        FirstLastName              = r.FirstLastName,
        SecondLastName             = r.SecondLastName,
        Email                      = r.Email,
        Phone                      = r.Phone,
        PhoneType                  = r.PhoneType,
        Gender                     = r.Gender,
        MaritalStatus              = r.MaritalStatus,
        Comment                    = r.Comment,
        RegistrationDate           = r.RegistrationDate,
        NationalityId              = r.NationalityId,
        Enrollment                 = r.Enrollment,
        MinistryId                 = r.MinistryId,
        Photo                      = r.Photo,
        BirthDate                  = r.BirthDate,
        PreferredShift             = r.PreferredShift,
        Allergies                  = r.Allergies,
        Illnesses                  = r.Illnesses,
        HealthCondition            = r.HealthCondition,
        OtherDifficulties          = r.OtherDifficulties,
        HealthComment              = r.HealthComment,
        SiblingNumber              = r.SiblingNumber,
        OrderNumber                = r.OrderNumber,
        HasConcession              = r.HasConcession,
        HasMedicalLicense          = r.HasMedicalLicense,
        HasPendings                = r.HasPendings,
        CircularSending            = r.CircularSending,
        AllowPreviousYearsGrading  = r.AllowPreviousYearsGrading,
        AdmissionFeeAction         = r.AdmissionFeeAction,
        EnrollmentType             = r.EnrollmentType,
        DoesNotRequireAdmission    = r.DoesNotRequireAdmission,
        FamilyId                   = r.FamilyId,
        CurrentCourseId            = r.CurrentCourseId,
        AccountId                  = r.AccountId,
        MedicalReferenceId         = r.MedicalReferenceId,
        TaxpayerId                 = r.TaxpayerId,
        ReceiptTypeId              = r.ReceiptTypeId,
        PreviousSchoolId           = r.PreviousSchoolId,
        BirthCityId                = r.BirthCityId,
        BloodTypeId                = r.BloodTypeId,
    };
}

public record StudentRequest(
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
