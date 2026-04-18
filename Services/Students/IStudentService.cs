using Domain;

namespace Services.Students;

public interface IStudentService
{
    Task<Student> CreateAsync(Student student, int createdBy);
}
