using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.Models;
using Service.Generator;

namespace Service.Controllers
{
    public class BCardsController : Controller
    {
        private CAHDB db = new CAHDB();

        // GET: BCards
        public ActionResult Index()
        {
            return View(db.BCards.ToList());
        }

        // GET: BCards/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BCard bCard = db.BCards.Find(id);
            if (bCard == null)
            {
                return HttpNotFound();
            }
            return View(bCard);
        }

        // GET: BCards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BCards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Text")] BCard bCard)
        {
            if (ModelState.IsValid)
            {
                bCard.BCardID = CardIDGen.Randomize(6);
                db.BCards.Add(bCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bCard);
        }

        // GET: BCards/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BCard bCard = db.BCards.Find(id);
            if (bCard == null)
            {
                return HttpNotFound();
            }
            return View(bCard);
        }

        // POST: BCards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Text")] BCard bCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bCard);
        }

        // GET: BCards/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BCard bCard = db.BCards.Find(id);
            if (bCard == null)
            {
                return HttpNotFound();
            }
            return View(bCard);
        }

        // POST: BCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BCard bCard = db.BCards.Find(id);
            db.BCards.Remove(bCard);
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
