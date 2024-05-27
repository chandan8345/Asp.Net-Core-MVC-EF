using Cart.Models;
using Cart.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService studentService;

        List<Student> Students = new List<Student>();

        public StudentController(DBContext context) {
            this.studentService = new StudentService(context);
        }

        public IActionResult Index()
        {
            var students = studentService.getStudent();
            if (students != null) {
                foreach (var s in students) {
                    Students.Add(s);
                }
            }
            return View(Students);
        }

        [HttpPost]
        public IActionResult Create(Student st) 
        {
            studentService.createStudent(st);
            ViewBag.Message = "Data Insert Successfully";
            return RedirectToAction("Index", "Student");
        }
    }
}
