﻿using Cart.Models;
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
            return View(Students);
        }

        [HttpPost]
        public IActionResult Create(Student st) 
        {
            studentService.createStudent(st);
            ViewBag.Message = "Data Insert Successfully";
            return RedirectToAction("Index", "Student");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
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
    }
}
