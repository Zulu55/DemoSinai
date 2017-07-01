using System.Data.Entity;
using System.Threading.Tasks;
namespace DemoSinai.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using DemoSinai.Models;
    using System;

    [Authorize]
    public class DepartmentsController : Controller
    {
        private DataContext db = new DataContext();

        public async Task<ActionResult> CreateCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var department = await db.Departments.FindAsync(id);

            if (department == null)
            {
                return HttpNotFound();
            }

            var city = new City { DepartmentId = department.DepartmentId, };
            return PartialView(city);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCity(City city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", city.DepartmentId));
            }

            return View(city);
        }


        // GET: Departments
        public async Task<ActionResult> Index()
        {
            var departments = await db.Departments.ToListAsync();
            return View(departments);
        }

        // GET: Departments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
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

            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var department = await db.Departments.FindAsync(id);

            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var department = await db.Departments.FindAsync(id);
            db.Departments.Remove(department);
            try
            {
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ModelState.AddModelError(string.Empty, "No se puede borrar el departamento porque tiene ciudades asociadas.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(department);
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
