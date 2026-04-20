using ADO.NET_API_1.DTOs;
using ADO.NET_API_1.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ADO.NET_API_1.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        // -------------------- GET ALL --------------------
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var students = new List<Student>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(
                @"SELECT Id, FirstName, LastName, Email, Age, Department 
                  FROM Students",
                connection);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                students.Add(MapStudent(reader));
            }

            return students;
        }

        // -------------------- GET BY ID --------------------
        public async Task<Student?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(
                @"SELECT Id, FirstName, LastName, Email, Age, Department 
                  FROM Students 
                  WHERE Id = @Id",
                connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapStudent(reader);
            }

            return null;
        }

        // -------------------- CREATE --------------------
        public async Task<int> CreateAsync(StudentCreateDto student)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                INSERT INTO Students (FirstName, LastName, Email, Age, Department)
                VALUES (@FirstName, @LastName, @Email, @Age, @Department);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var command = new SqlCommand(query, connection);

            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = student.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = student.LastName;
            command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = student.Email;
            command.Parameters.Add("@Age", SqlDbType.Int).Value = student.Age;
            command.Parameters.Add("@Department", SqlDbType.NVarChar, 100).Value = student.Department;

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return (result != null) ? Convert.ToInt32(result) : 0;
        }

        // -------------------- UPDATE --------------------
        public async Task<bool> UpdateAsync(int id, StudentUpdateDto student)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                UPDATE Students
                SET FirstName = @FirstName,
                    LastName = @LastName,
                    Email = @Email,
                    Age = @Age,
                    Department = @Department
                WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = student.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = student.LastName;
            command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = student.Email;
            command.Parameters.Add("@Age", SqlDbType.Int).Value = student.Age;
            command.Parameters.Add("@Department", SqlDbType.NVarChar, 100).Value = student.Department;

            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        // -------------------- DELETE --------------------
        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(
                @"DELETE FROM Students WHERE Id = @Id",
                connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        // -------------------- MAPPING METHOD --------------------
        private static Student MapStudent(SqlDataReader reader)
        {
            return new Student
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                Age = reader.GetInt32(4),
                Department = reader.GetString(5)
            };
        }
    }
}