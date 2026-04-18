using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Student : Entity
{
    [MaxLength(16)]
    public string? NationalId { get; set; }

    public PersonDocumentType DocumentType { get; set; }

    [MaxLength(35)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(35)]
    public string? MiddleName { get; set; }

    [MaxLength(35)]
    public string FirstLastName { get; set; } = string.Empty;

    [MaxLength(35)]
    public string? SecondLastName { get; set; }

    [MaxLength(75)]
    public string? Email { get; set; }

    [MaxLength(21)]
    public string? Phone { get; set; }

    public PhoneType PhoneType { get; set; }

    public Gender Gender { get; set; }

    public MaritalStatus MaritalStatus { get; set; }

    [MaxLength(180)]
    public string? Comment { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public int NationalityId { get; set; }

    [MaxLength(15)]
    public string Enrollment { get; set; } = string.Empty;

    /// <summary>Ministry of Education identifier — used for grade record lookups.</summary>
    [MaxLength(20)]
    public string? MinistryId { get; set; }

    [MaxLength(260)]
    public string? Photo { get; set; }

    public DateTime? BirthDate { get; set; }

    public Shift PreferredShift { get; set; }

    [MaxLength(60)]
    public string? Allergies { get; set; }

    [MaxLength(60)]
    public string? Illnesses { get; set; }

    [MaxLength(60)]
    public string? HealthCondition { get; set; }

    [MaxLength(60)]
    public string? OtherDifficulties { get; set; }

    [MaxLength(100)]
    public string? HealthComment { get; set; }

    /// <summary>
    /// Sibling order within the family — drives sibling discount calculations.
    /// Must be recomputed across all siblings when one is added, removed, or retained.
    /// </summary>
    public int SiblingNumber { get; set; }

    public int OrderNumber { get; set; }

    public bool HasConcession { get; set; }

    public bool HasMedicalLicense { get; set; }

    public bool HasPendings { get; set; }

    public StudentStatus StudentStatus { get; set; } = StudentStatus.New;

    public CircularSending CircularSending { get; set; }

    /// <summary>If true, this student may edit grades for prior school years.</summary>
    public bool AllowPreviousYearsGrading { get; set; }

    public AdmissionFeeAction AdmissionFeeAction { get; set; }

    public EnrollmentType EnrollmentType { get; set; }

    /// <summary>True for students migrated from the legacy system who have no admission record.</summary>
    public bool DoesNotRequireAdmission { get; set; }

    public int FamilyId { get; set; }

    public int CurrentCourseId { get; set; }

    public int AccountId { get; set; }

    public int? MedicalReferenceId { get; set; }

    public int? TaxpayerId { get; set; }

    public int ReceiptTypeId { get; set; }

    public int? PreviousSchoolId { get; set; }

    public int? BirthCityId { get; set; }

    public int? BloodTypeId { get; set; }
}
