using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VD.Data; // Chứa StudentContext
using VD.Models; // Chứa model Student

namespace VD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        // 1️⃣ Lấy danh sách sinh viên
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        // 2️⃣ Lấy thông tin sinh viên theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(new { message = "Không tìm thấy sinh viên!" });
            }
            return Ok(student);
        }

        // 3️⃣ Thêm sinh viên mới
        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // 4️⃣ Cập nhật thông tin sinh viên
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student studentUpdate)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(new { message = "Không tìm thấy sinh viên!" });
            }

            // Cập nhật thông tin
            student.Name = studentUpdate.Name;
            student.Age = studentUpdate.Age;
            student.Class = studentUpdate.Class;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Cập nhật thông tin thành công!", student });
        }

        // 5️⃣ Cập nhật ảnh sinh viên
        [HttpPut("UpdatePhoto/{id}")]
        public async Task<IActionResult> UpdatePhoto(int id, [FromBody] string base64ImageString)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(new { message = "Không tìm thấy sinh viên!" });
            }

            student.Photo = base64ImageString;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cập nhật ảnh thành công!" });
        }

        // 6️⃣ Xóa sinh viên
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(new { message = "Không tìm thấy sinh viên!" });
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa sinh viên thành công!" });
        }
    }
}
