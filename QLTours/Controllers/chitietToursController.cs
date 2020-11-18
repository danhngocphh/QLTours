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
    public class chitietToursController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: chitietTours
        public ActionResult Index()
        {
            var chitietTours = db.chitietTours.Include(c => c.diadiem).Include(c => c.tour);
            return View(chitietTours.ToList());
        }

        // GET: chitietTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietTour chitietTour = db.chitietTours.Find(id);
            if (chitietTour == null)
            {
                return HttpNotFound();
            }
            return View(chitietTour);
        }

        // GET: chitietTours/Create
        public ActionResult Create(int id)
        {
            string thanhpho = "";
            ViewBag.ThanhPho = new SelectList(db.diadiems.GroupBy(d=>new {d.ThanhPho}).Select(group=>new { group.Key.ThanhPho }), "ThanhPho","ThanhPho",thanhpho);
            ViewBag.IdDiaDiem = new SelectList(db.diadiems.Where(d => d.ThanhPho.Contains(thanhpho)), "Id", "Ten");


            var tour = db.tours.Find(id);
            ViewBag.TourId = id;
            ViewBag.TourTen = tour.Ten;
            return View();
        }

        // POST: chitietTours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdTour,IdDiaDiem,STTDiaDiem")] chitietTour chitietTour)
        {
            if (ModelState.IsValid)
            {
                db.chitietTours.Add(chitietTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            diadiem dd = new diadiem();
            ViewBag.ThanhPho = new SelectList(db.diadiems.GroupBy(d => new { d.ThanhPho }).Select(group => new { group.Key.ThanhPho }), "ThanhPho", "ThanhPho", dd.ThanhPho);
            ViewBag.IdDiaDiem = new SelectList(db.diadiems.Where(d => d.ThanhPho.Contains(dd.ThanhPho.ToString())), "Id", "Ten");

            int id = Convert.ToInt32(Request.QueryString["id"]);
            var tour = db.tours.Find(id);
            ViewBag.TourId = id;
            ViewBag.TourTen = tour.Ten;
            return View(chitietTour);
        }

        // GET: chitietTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietTour chitietTour = db.chitietTours.Find(id);
            if (chitietTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDiaDiem = new SelectList(db.diadiems, "Id", "ThanhPho", chitietTour.IdDiaDiem);
            ViewBag.IdTour = new SelectList(db.tours, "Id", "Ten", chitietTour.IdTour);
            return View(chitietTour);
        }

        // POST: chitietTours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdTour,IdDiaDiem,STTDiaDiem")] chitietTour chitietTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chitietTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDiaDiem = new SelectList(db.diadiems, "Id", "ThanhPho", chitietTour.IdDiaDiem);
            ViewBag.IdTour = new SelectList(db.tours, "Id", "Ten", chitietTour.IdTour);
            return View(chitietTour);
        }

        // GET: chitietTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietTour chitietTour = db.chitietTours.Find(id);
            if (chitietTour == null)
            {
                return HttpNotFound();
            }
            return View(chitietTour);
        }

        // POST: chitietTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            chitietTour chitietTour = db.chitietTours.Find(id);
            db.chitietTours.Remove(chitietTour);
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
