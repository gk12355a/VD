using VD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using VD.Data; // Chứa StudentContext

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

        //action
        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent(Student student)
        {

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student added successfully", student });

        }
        [HttpPut("UpdatePhoto/{id}")]
        public async Task<IActionResult> UpdatePhoto(int id, [FromBody] string base64ImageString)
        {
            // Tìm sinh viên trong cơ sở dữ liệu bằng ID
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound(); // Trả về mã lỗi 404 nếu không tìm thấy sinh viên
            }

            // Cập nhật trường Photo của sinh viên
            student.Photo = base64ImageString;

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return Ok(); // Trả về mã lỗi 200 OK nếu cập nhật thành công
        }

    }
}