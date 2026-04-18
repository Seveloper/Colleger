using Dapper;
using Domain;
using Infrastructure.Data;
using Services.Students;

namespace Infrastructure.Students;

public class StudentRepository : IStudentRepository
{
    private readonly IDbConnectionFactory _factory;

    public StudentRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> CreateAsync(Student student)
    {
        using var conn = _factory.Create();
        return await conn.ExecuteScalarAsync<int>(@"
            INSERT INTO Students (
                Status, NationalId, DocumentType, FirstName, MiddleName, FirstLastName, SecondLastName,
                Email, Phone, PhoneType, Gender, MaritalStatus, Comment, RegistrationDate, NationalityId,
                Enrollment, MinistryId, Photo, BirthDate, PreferredShift,
                Allergies, Illnesses, HealthCondition, OtherDifficulties, HealthComment,
                SiblingNumber, OrderNumber, HasConcession, HasMedicalLicense, HasPendings,
                StudentStatus, CircularSending, AllowPreviousYearsGrading, AdmissionFeeAction,
                EnrollmentType, DoesNotRequireAdmission,
                FamilyId, CurrentCourseId, AccountId, MedicalReferenceId, TaxpayerId,
                ReceiptTypeId, PreviousSchoolId, BirthCityId, BloodTypeId,
                CreatedBy, CreatedDate
            )
            OUTPUT INSERTED.Id
            VALUES (
                @Status, @NationalId, @DocumentType, @FirstName, @MiddleName, @FirstLastName, @SecondLastName,
                @Email, @Phone, @PhoneType, @Gender, @MaritalStatus, @Comment, @RegistrationDate, @NationalityId,
                @Enrollment, @MinistryId, @Photo, @BirthDate, @PreferredShift,
                @Allergies, @Illnesses, @HealthCondition, @OtherDifficulties, @HealthComment,
                @SiblingNumber, @OrderNumber, @HasConcession, @HasMedicalLicense, @HasPendings,
                @StudentStatus, @CircularSending, @AllowPreviousYearsGrading, @AdmissionFeeAction,
                @EnrollmentType, @DoesNotRequireAdmission,
                @FamilyId, @CurrentCourseId, @AccountId, @MedicalReferenceId, @TaxpayerId,
                @ReceiptTypeId, @PreviousSchoolId, @BirthCityId, @BloodTypeId,
                @CreatedBy, @CreatedDate
            );",
            student);
    }
}
