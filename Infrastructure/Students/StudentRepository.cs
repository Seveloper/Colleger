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

    public async Task<IEnumerable<Student>> GetAllAsync(string? search = null)
    {
        using var conn = _factory.Create();
        return await conn.QueryAsync<Student>(@"
            SELECT * FROM Students
            WHERE Status = 1
            AND (@search IS NULL
                 OR FirstName     LIKE '%' + @search + '%'
                 OR FirstLastName LIKE '%' + @search + '%'
                 OR Enrollment    LIKE '%' + @search + '%')
            ORDER BY FirstLastName, FirstName",
            new { search });
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        using var conn = _factory.Create();
        return await conn.QuerySingleOrDefaultAsync<Student>(
            "SELECT * FROM Students WHERE Id = @id AND Status = 1",
            new { id });
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

    public async Task UpdateAsync(Student student)
    {
        using var conn = _factory.Create();
        await conn.ExecuteAsync(@"
            UPDATE Students SET
                NationalId = @NationalId, DocumentType = @DocumentType,
                FirstName = @FirstName, MiddleName = @MiddleName,
                FirstLastName = @FirstLastName, SecondLastName = @SecondLastName,
                Email = @Email, Phone = @Phone, PhoneType = @PhoneType,
                Gender = @Gender, MaritalStatus = @MaritalStatus,
                Comment = @Comment, RegistrationDate = @RegistrationDate, NationalityId = @NationalityId,
                Enrollment = @Enrollment, MinistryId = @MinistryId, Photo = @Photo,
                BirthDate = @BirthDate, PreferredShift = @PreferredShift,
                Allergies = @Allergies, Illnesses = @Illnesses, HealthCondition = @HealthCondition,
                OtherDifficulties = @OtherDifficulties, HealthComment = @HealthComment,
                SiblingNumber = @SiblingNumber, OrderNumber = @OrderNumber,
                HasConcession = @HasConcession, HasMedicalLicense = @HasMedicalLicense, HasPendings = @HasPendings,
                StudentStatus = @StudentStatus, CircularSending = @CircularSending,
                AllowPreviousYearsGrading = @AllowPreviousYearsGrading,
                AdmissionFeeAction = @AdmissionFeeAction, EnrollmentType = @EnrollmentType,
                DoesNotRequireAdmission = @DoesNotRequireAdmission,
                FamilyId = @FamilyId, CurrentCourseId = @CurrentCourseId, AccountId = @AccountId,
                MedicalReferenceId = @MedicalReferenceId, TaxpayerId = @TaxpayerId,
                ReceiptTypeId = @ReceiptTypeId, PreviousSchoolId = @PreviousSchoolId,
                BirthCityId = @BirthCityId, BloodTypeId = @BloodTypeId,
                UpdatedBy = @UpdatedBy, UpdatedDate = @UpdatedDate
            WHERE Id = @Id AND Status = 1",
            student);
    }

    public async Task DeleteAsync(int id, int deletedBy)
    {
        using var conn = _factory.Create();
        await conn.ExecuteAsync(@"
            UPDATE Students SET Status = -1, UpdatedBy = @deletedBy, UpdatedDate = GETUTCDATE()
            WHERE Id = @id",
            new { id, deletedBy });
    }
}
