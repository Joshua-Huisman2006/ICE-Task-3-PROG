using Microsoft.AspNetCore.Mvc;
using StudentManagementMVC.Models;
using StudentManagementMVC.Services;

namespace StudentManagementMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentStore _studentStore;

        public StudentController(StudentStore studentStore)
        {
            _studentStore = studentStore;
        }

        // GET: /Student
        public IActionResult Index()
        {
            var students = _studentStore.GetAll();
            return View(students);
        }

        // GET: /Student/Details/5
        public IActionResult Details(int id)
        {
            var student = _studentStore.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: /Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Email,DateOfBirth,Grade,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _studentStore.Add(student);
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        // GET: /Student/Edit/5
        public IActionResult Edit(int id)
        {
            var student = _studentStore.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: /Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Email,DateOfBirth,Grade,EnrollmentDate")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updated = _studentStore.Update(student);
                if (!updated)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        // GET: /Student/Delete/5
        public IActionResult Delete(int id)
        {
            var student = _studentStore.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _studentStore.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
