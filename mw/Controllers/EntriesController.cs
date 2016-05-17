using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mw.Models;
using mw.ViewModels;

namespace mw.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EntriesController : Controller
    {
        private EntryContext db = new EntryContext();
        
        [AllowAnonymous]
        public ActionResult Index(int page = 1)
        {
            return View(db.Entries.ToList().OrderBy(e => e.DateCreated).Reverse().Skip((page - 1) * 10).Take(10));
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entry entry = db.Entries.Find(id);
            if (entry == null) {
                return HttpNotFound();
            }
            return View(entry);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEntryViewModel createEntryViewModel)
        {
            if (ModelState.IsValid) {
                var entry = new Entry { Body = createEntryViewModel.Body, Title = createEntryViewModel.Title };
                db.Entries.Add(entry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(createEntryViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entry entry = db.Entries.Find(id);
            if (entry == null) {
                return HttpNotFound();
            }
            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditEntryViewModel editEntryViewModel)
        {
            if (ModelState.IsValid) {
                var entry = db.Entries.Where(x => x.Id == editEntryViewModel.Id).FirstOrDefault();
                entry.Body = editEntryViewModel.Body;
                entry.Title = editEntryViewModel.Title;
                db.Entry(entry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editEntryViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entry entry = db.Entries.Find(id);
            if (entry == null) {
                return HttpNotFound();
            }
            return View(entry);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entry entry = db.Entries.Find(id);
            db.Entries.Remove(entry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
