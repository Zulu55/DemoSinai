namespace DemoSinai.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Models;
    using Helpers;
    using System;
    using System.Linq;

    [Authorize]
    public class StudentsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Students
        public async Task<ActionResult> Index()
        {
            var students = db.Students.Include(s => s.City).Include(s => s.School);
            return View(await students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            ViewBag.CityId = new SelectList(db.Cities.Where(c => c.DepartmentId == db.Departments.FirstOrDefault().DepartmentId), "CityId", "Name");
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "Name");
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Pictures";

                if (view.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var student = ToStudent(view);
                student.Picture = pic;

                db.Students.Add(student);
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                        ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("Index"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un departamento con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", view.CityId);
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "Name", view.SchoolId);
            return View(view);
        }

        private Student ToStudent(StudentView view)
        {
            return new Student
            {
                Address = view.Address,
                City = view.City,
                CityId = view.CityId,
                FirstName = view.FirstName,
                LastName = view.LastName,
                Phone = view.Phone,
                Picture = view.Picture,
                School = view.School,
                SchoolId = view.SchoolId,
                StudentId = view.StudentId,
                UserName = view.UserName,
            };
        }


        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = await db.Students.FindAsync(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", student.CityId);
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "Name", student.SchoolId);
            var view = ToView(student);
            return View(view);
        }

        private StudentView ToView(Student student)
        {
            return new StudentView
            {
                Address = student.Address,
                City = student.City,
                CityId = student.CityId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Phone = student.Phone,
                Picture = student.Picture,
                School = student.School,
                SchoolId = student.SchoolId,
                StudentId = student.StudentId,
                UserName = student.UserName,
            };
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Picture;
                var folder = "~/Content/Pictures";

                if (view.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var student = ToStudent(view);
                student.Picture = pic;

                db.Entry(student).State = EntityState.Modified;
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                        ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("Index"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un estudiante con el mismo email.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", view.CityId);
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "Name", view.SchoolId);
            return View(view);
        }

        // GET: Students/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student student = await db.Students.FindAsync(id);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public JsonResult GetCities(int departmentId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cities = db.Cities.Where(c => c.DepartmentId == departmentId);
            return Json(cities);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
