using Domain;

namespace Services.Students;

public interface IStudentRepository
{
    Task<int> CreateAsync(Student student);
}
