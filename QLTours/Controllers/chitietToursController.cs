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
        public ActionResult Create(string thanhpho,int idtour)
        {
            var ThanhPho = db.diadiems.GroupBy(d=>new { d.ThanhPho }).Select(g=>new { g.Key.ThanhPho });
            ViewBag.ThanhPho = new SelectList(ThanhPho, "ThanhPho", "ThanhPho");
            var diadiem = db.diadiems.Where(dd=>dd.ThanhPho.Contains(thanhpho));
            ViewBag.IdDiaDiem = new SelectList(diadiem, "Id", "Ten");

            var Tour = db.tours.Select(t => new { t.Id, t.Ten });
            ViewBag.IdTour = new SelectList(Tour, "Id", "Ten");

            var chitiettour = db.chitietTours.Join(db.diadiems,
                                                    ct=>ct.IdDiaDiem,
                                                    dds=>dds.Id,
                                                    (ct,dds)=>new {TenDiaDiem = dds.Ten,STT = ct.STTDiaDiem, ct.IdTour })
                                                    .Where(j=>j.IdTour==idtour);
            ViewBag.chitiettour = new SelectList(chitiettour,"STT","TenDiaDiem");

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
            string thanhpho = Request.QueryString["thanhpho"];
            var ThanhPho = db.diadiems.GroupBy(d => new { d.ThanhPho }).Select(g => new { g.Key.ThanhPho });
            ViewBag.ThanhPho = new SelectList(ThanhPho, "ThanhPho", "ThanhPho");
            var diadiem = db.diadiems.Where(dd => dd.ThanhPho.Contains(thanhpho));
            ViewBag.IdDiaDiem = new SelectList(diadiem, "Id", "Ten",chitietTour.IdDiaDiem);

            var Tour = db.tours.Select(t => new { t.Id, t.Ten });
            ViewBag.IdTour = new SelectList(Tour, "Id", "Ten",chitietTour.IdTour);

            int idtour = Convert.ToInt32(Request.QueryString["idtour"]);
            var chitiettour = db.chitietTours.Join(db.diadiems,
                                                    ct => ct.IdDiaDiem,
                                                    dds => dds.Id,
                                                    (ct, dds) => new { TenDiaDiem = dds.Ten, STT = ct.STTDiaDiem, ct.IdTour })
                                                    .Where(j => j.IdTour == idtour);
            ViewBag.chitiettour = new SelectList(chitiettour, "STT", "TenDiaDiem");

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
