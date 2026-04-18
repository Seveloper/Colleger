using Domain;

namespace Services.Students;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync(string? search = null);
    Task<Student?> GetByIdAsync(int id);
    Task<int> CreateAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(int id, int deletedBy);
}
