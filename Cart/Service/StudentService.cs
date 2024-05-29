using Cart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.Service
{
    public class StudentService
    {
        private readonly DBContext context;
        private readonly List<Student> _items;

        public StudentService(DBContext context) {
            this.context = context;
        }

        public List<Student> getStudent() {
            return context.Students.ToList();
        }

        public void createStudent(Student s) {
            context.Students.Add(s);
            context.SaveChanges();
        }

        public void deleteStudent(int id) {
            var student = context.Students.Find(id);
            context.Students.Remove(student);
            context.SaveChanges();
        }

        public dynamic editStudent(Student student)
        {
            var s = context.Students.Find(student.Id);
            return s;
        }
    }
}
