using Domain;

namespace Services.Students;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllAsync(string? search = null);
    Task<Student?> GetByIdAsync(int id);
    Task<Student> CreateAsync(Student student, int createdBy);
    Task<Student?> UpdateAsync(int id, Student student, int updatedBy);
    Task<bool> DeleteAsync(int id, int deletedBy);
}
