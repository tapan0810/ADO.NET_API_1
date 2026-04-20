using ADO.NET_API_1.DTOs;
using ADO.NET_API_1.Models;

namespace ADO.NET_API_1.Data
{
    public interface IStudentRepository
    {
            Task<IEnumerable<Student>> GetAllAsync();
            Task<Student?> GetByIdAsync(int id);
            Task<int> CreateAsync(StudentCreateDto student);
            Task<bool> UpdateAsync(int id,StudentUpdateDto student);
            Task<bool> DeleteAsync(int id);
    }
}
