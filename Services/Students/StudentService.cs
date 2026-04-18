using Domain;

namespace Services.Students;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;

    public StudentService(IStudentRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Student>> GetAllAsync(string? search = null) =>
        _repo.GetAllAsync(search);

    public Task<Student?> GetByIdAsync(int id) =>
        _repo.GetByIdAsync(id);

    public async Task<Student> CreateAsync(Student student, int createdBy)
    {
        student.Status = Status.Active;
        student.StudentStatus = StudentStatus.New;
        student.CreatedBy = createdBy;
        student.CreatedDate = DateTime.UtcNow;

        student.Id = await _repo.CreateAsync(student);
        return student;
    }

    public async Task<Student?> UpdateAsync(int id, Student student, int updatedBy)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return null;

        student.Id = id;
        student.UpdatedBy = updatedBy;
        student.UpdatedDate = DateTime.UtcNow;

        await _repo.UpdateAsync(student);
        return student;
    }

    public async Task<bool> DeleteAsync(int id, int deletedBy)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return false;

        await _repo.DeleteAsync(id, deletedBy);
        return true;
    }
}
