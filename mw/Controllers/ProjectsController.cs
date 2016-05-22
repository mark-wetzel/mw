using mw.Models;
using mw.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace mw.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProjectsController : Controller
    {
        private ProjectContext db = new ProjectContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Projects);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProjectViewModel createProjectViewModel)
        {
            if (ModelState.IsValid) {
                var project = new Project
                {
                    Description = createProjectViewModel.Description,
                    Name = createProjectViewModel.Name,
                    ProjectImage = createProjectViewModel.Image
                };

                if (project.ProjectImage != null) {
                    string image = System.IO.Path.GetFileName(project.ProjectImage.FileName);
                    System.IO.Directory.CreateDirectory(Server.MapPath(string.Format("~/Content/Images/Projects/{0}/", project.Name)));
                    string path = System.IO.Path.Combine(Server.MapPath(string.Format("~/Content/Images/Projects/{0}/", project.Name)), image);
                    string relPath = System.IO.Path.Combine(string.Format("~/Content/Images/Projects/{0}/", project.Name), image);
                    project.ProjectImage.SaveAs(path);
                    project.Image = new Image { PhysicalPath = path, RelativePath = relPath };
                    db.Images.Add(project.Image);
                }

                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(createProjectViewModel);
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.Projects.Find(id);

            if (project == null) {
                return HttpNotFound();
            }

            return View(project);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.Projects.Find(id);

            if (project == null) {
                return HttpNotFound();
            }

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProjectViewModel editProjectViewModel)
        {
            if (ModelState.IsValid) {
                var project = db.Projects.Where(p => p.Id == editProjectViewModel.Id).FirstOrDefault();
                project.Description = editProjectViewModel.Description;
                project.Name = editProjectViewModel.Name;
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editProjectViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.Projects.Find(id);

            if (project == null) {
                return HttpNotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);


            if (project.Image != null) {
                Directory.Delete(Path.GetDirectoryName(project.Image.PhysicalPath), true);
            }

            db.Images.Remove(project.Image);
            db.Projects.Remove(project);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Manage()
        {
            var projects = db.Projects;
            return View(projects);
        }
    }
}