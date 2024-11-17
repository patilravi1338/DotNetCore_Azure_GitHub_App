using Api.Interfaces;
using Api.Models;

namespace Api.Services
{
  public class StudentService
  {
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
      _studentRepository = studentRepository;
    }

    public Task<IEnumerable<Student>> GetStudentsAsync() => _studentRepository.GetStudentsAsync();
    public Task AddStudentAsync(Student student) => _studentRepository.AddStudentAsync(student);
    public Task UpdateStudentAsync(Student student) => _studentRepository.UpdateStudentAsync(student);
    public Task DeleteStudentAsync(int id) => _studentRepository.DeleteStudentAsync(id);
  }
}