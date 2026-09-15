using StudentManagementMVC.Models;

namespace StudentManagementMVC.Services
{
    // Simple in-memory data store for Student records (thread-safe singleton).
    public class StudentStore
    {
        private readonly List<Student> _students = new();
        private int _nextId = 1;
        private readonly object _lock = new();

        public StudentStore()
        {
            // Seed with a couple of sample records
            Add(new Student
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                DateOfBirth = new DateTime(2005, 4, 12),
                Grade = "10",
                EnrollmentDate = new DateTime(2023, 9, 1)
            });

            Add(new Student
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                DateOfBirth = new DateTime(2006, 8, 23),
                Grade = "9",
                EnrollmentDate = new DateTime(2023, 9, 1)
            });
        }

        public List<Student> GetAll()
        {
            lock (_lock)
            {
                return _students.OrderBy(s => s.Id).ToList();
            }
        }

        public Student? GetById(int id)
        {
            lock (_lock)
            {
                return _students.FirstOrDefault(s => s.Id == id);
            }
        }

        public Student Add(Student student)
        {
            lock (_lock)
            {
                student.Id = _nextId++;
                _students.Add(student);
                return student;
            }
        }

        public bool Update(Student student)
        {
            lock (_lock)
            {
                var existing = _students.FirstOrDefault(s => s.Id == student.Id);
                if (existing == null)
                {
                    return false;
                }

                existing.FirstName = student.FirstName;
                existing.LastName = student.LastName;
                existing.Email = student.Email;
                existing.DateOfBirth = student.DateOfBirth;
                existing.Grade = student.Grade;
                existing.EnrollmentDate = student.EnrollmentDate;
                return true;
            }
        }

        public bool Delete(int id)
        {
            lock (_lock)
            {
                var existing = _students.FirstOrDefault(s => s.Id == id);
                if (existing == null)
                {
                    return false;
                }

                return _students.Remove(existing);
            }
        }
    }
}
