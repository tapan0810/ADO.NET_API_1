using ADO.NET_API_1.Data;
using ADO.NET_API_1.DTOs;
using ADO.NET_API_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NET_API_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repository;

        public StudentsController(IStudentRepository repository)
        {
            _repository = repository;
        }

        // -------------------- GET ALL --------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            var students = await _repository.GetAllAsync();
            return Ok(students);
        }

        // -------------------- GET BY ID --------------------
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student == null)
                return NotFound(new { message = $"Student with Id {id} not found" });

            return Ok(student);
        }

        // -------------------- CREATE --------------------
        [HttpPost]
        public async Task<ActionResult<Student>> Create([FromBody] StudentCreateDto dto)
        {
            // ModelState automatically handled by [ApiController]

            int newId = await _repository.CreateAsync(dto);

            var createdStudent = await _repository.GetByIdAsync(newId);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newId },
                createdStudent
            );
        }

        // -------------------- UPDATE --------------------
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDto dto)
        {
            var exists = await _repository.GetByIdAsync(id);
            if (exists == null)
                return NotFound(new { message = $"Student with Id {id} not found" });

            var updated = await _repository.UpdateAsync(id, dto);

            if (!updated)
                return StatusCode(500, new { message = "Failed to update student" });

            return NoContent();
        }

        // -------------------- DELETE --------------------
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _repository.GetByIdAsync(id);
            if (exists == null)
                return NotFound(new { message = $"Student with Id {id} not found" });

            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return StatusCode(500, new { message = "Failed to delete student" });

            return NoContent();
        }
    }
}