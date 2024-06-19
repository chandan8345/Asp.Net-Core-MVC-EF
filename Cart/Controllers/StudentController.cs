using Cart.Models;
using Cart.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            if (TempData["student"] != null)
            {
                TempData.Keep("student");
            }
            else
            {
                TempData.Remove("student");
            }
            return View(Students);
        }

        [HttpPost]
        public IActionResult Create(Student st) 
        {
            TempData.Remove("student");
            studentService.createStudent(st);
            ViewBag.Message = "Data Insert Successfully";
            return RedirectToAction("Index", "Student");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            TempData.Remove("student");
            studentService.deleteStudent(Id);
            return RedirectToAction("Index", "Student");
        }

        [HttpPost]
        public IActionResult Edit(Student s)
        {
            var student = studentService.editStudent(s);
            var serializedStudent = JsonConvert.SerializeObject(student);
            TempData["student"] = serializedStudent;
            return RedirectToAction("Index", "Student");
        }

        [HttpPost]
        public IActionResult Update(Student s)
        {
            TempData.Remove("student");
            studentService.updateStudent(s);
            ViewBag.Message = "Data Update Successfully";
            return RedirectToAction("Index", "Student");
        }
    }
}
