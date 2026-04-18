using FluentMigrator;
using Migrations.Extensions;

namespace Migrations.Migrations;

[Migration(202604180001, "Create Students table")]
public class _202604180001_CreateStudents : Migration
{
    public override void Up()
    {
        Create.Table("Students")
            .WithId()
            .WithColumn("Status").AsInt32().NotNullable().WithDefaultValue(1)

            // Person fields
            .WithColumn("NationalId").AsString(16).Nullable()
            .WithColumn("DocumentType").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("FirstName").AsString(35).NotNullable()
            .WithColumn("MiddleName").AsString(35).Nullable()
            .WithColumn("FirstLastName").AsString(35).NotNullable()
            .WithColumn("SecondLastName").AsString(35).Nullable()
            .WithColumn("Email").AsString(75).Nullable()
            .WithColumn("Phone").AsString(21).Nullable()
            .WithColumn("PhoneType").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("Gender").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("MaritalStatus").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("Comment").AsString(180).Nullable()
            .WithColumn("RegistrationDate").AsDateTime2().Nullable()
            .WithColumn("NationalityId").AsInt32().NotNullable()

            // Student-specific
            .WithColumn("Enrollment").AsString(15).NotNullable()
            .WithColumn("MinistryId").AsString(20).Nullable()
            .WithColumn("Photo").AsString(260).Nullable()
            .WithColumn("BirthDate").AsDateTime2().Nullable()
            .WithColumn("PreferredShift").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("Allergies").AsString(60).Nullable()
            .WithColumn("Illnesses").AsString(60).Nullable()
            .WithColumn("HealthCondition").AsString(60).Nullable()
            .WithColumn("OtherDifficulties").AsString(60).Nullable()
            .WithColumn("HealthComment").AsString(100).Nullable()
            .WithColumn("SiblingNumber").AsInt32().NotNullable().WithDefaultValue(1)
            .WithColumn("OrderNumber").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("HasConcession").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("HasMedicalLicense").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("HasPendings").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("StudentStatus").AsInt32().NotNullable().WithDefaultValue(2)
            .WithColumn("CircularSending").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("AllowPreviousYearsGrading").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("AdmissionFeeAction").AsInt32().NotNullable().WithDefaultValue(1)
            .WithColumn("EnrollmentType").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("DoesNotRequireAdmission").AsBoolean().NotNullable().WithDefaultValue(false)

            // Foreign key IDs (relations will be added later)
            .WithColumn("FamilyId").AsInt32().NotNullable()
            .WithColumn("CurrentCourseId").AsInt32().NotNullable()
            .WithColumn("AccountId").AsInt32().NotNullable()
            .WithColumn("MedicalReferenceId").AsInt32().Nullable()
            .WithColumn("TaxpayerId").AsInt32().Nullable()
            .WithColumn("ReceiptTypeId").AsInt32().NotNullable()
            .WithColumn("PreviousSchoolId").AsInt32().Nullable()
            .WithColumn("BirthCityId").AsInt32().Nullable()
            .WithColumn("BloodTypeId").AsInt32().Nullable()

            .WithTrackingColumns();

        Create.Index("IX_Students_Enrollment").OnTable("Students").OnColumn("Enrollment").Unique();
    }

    public override void Down()
    {
        Delete.Table("Students");
    }
}
