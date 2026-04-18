using Domain;

namespace Services.Students;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;

    public StudentService(IStudentRepository repo)
    {
        _repo = repo;
    }

    public async Task<Student> CreateAsync(Student student, int createdBy)
    {
        student.Status = Status.Active;
        student.StudentStatus = StudentStatus.New;
        student.CreatedBy = createdBy;
        student.CreatedDate = DateTime.UtcNow;

        student.Id = await _repo.CreateAsync(student);
        return student;
    }
}
