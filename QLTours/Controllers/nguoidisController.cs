using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTours.Models;

namespace QLTours.Controllers
{
    public class nguoidisController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: nguoidis
        public ActionResult Index()
        {
            var nguoidis = db.nguoidis.Include(n => n.doan);
            return View(nguoidis.ToList());
        }

        // GET: nguoidis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nguoidi nguoidi = db.nguoidis.Find(id);
            if (nguoidi == null)
            {
                return HttpNotFound();
            }
            return View(nguoidi);
        }

        // GET: nguoidis/Create
        public ActionResult Create()
        {
            ViewBag.IdDoan = new SelectList(db.doans, "Id", "Ten");
            return View();
        }

        // POST: nguoidis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdDoan,DSNhanvien,DSKhach")] nguoidi nguoidi)
        {
            if (ModelState.IsValid)
            {
                db.nguoidis.Add(nguoidi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDoan = new SelectList(db.doans, "Id", "Ten", nguoidi.IdDoan);
            return View(nguoidi);
        }

        // GET: nguoidis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nguoidi nguoidi = db.nguoidis.Find(id);
            if (nguoidi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDoan = new SelectList(db.doans, "Id", "Ten", nguoidi.IdDoan);
            return View(nguoidi);
        }

        // POST: nguoidis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdDoan,DSNhanvien,DSKhach")] nguoidi nguoidi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoidi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDoan = new SelectList(db.doans, "Id", "Ten", nguoidi.IdDoan);
            return View(nguoidi);
        }

        // GET: nguoidis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nguoidi nguoidi = db.nguoidis.Find(id);
            if (nguoidi == null)
            {
                return HttpNotFound();
            }
            return View(nguoidi);
        }

        // POST: nguoidis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            nguoidi nguoidi = db.nguoidis.Find(id);
            db.nguoidis.Remove(nguoidi);
            db.SaveChanges();
            return RedirectToAction("Index");
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
