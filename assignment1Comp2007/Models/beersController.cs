﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace assignment1Comp2007.Models
{
    public class beersController : Controller
    {
        private breweryModel db = new breweryModel();

        // GET: beers
        public ActionResult Index()
        {
            var beers = db.beers.Include(b => b.brewery);
            return View(beers.ToList());
        }

        // GET: beers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            beer beer = db.beers.Find(id);
            if (beer == null)
            {
                return HttpNotFound();
            }
            return View(beer);
        }

        // GET: beers/Create
        public ActionResult Create()
        {
            ViewBag.breweryID = new SelectList(db.breweries, "breweryID", "breweryLocation");
            return View();
        }

        // POST: beers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "beerID,breweryID,beerName,beerType,canOrBottle")] beer beer)
        {
            if (ModelState.IsValid)
            {
                db.beers.Add(beer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.breweryID = new SelectList(db.breweries, "breweryID", "breweryLocation", beer.breweryID);
            return View(beer);
        }

        // GET: beers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            beer beer = db.beers.Find(id);
            if (beer == null)
            {
                return HttpNotFound();
            }
            ViewBag.breweryID = new SelectList(db.breweries, "breweryID", "breweryLocation", beer.breweryID);
            return View(beer);
        }

        // POST: beers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "beerID,breweryID,beerName,beerType,canOrBottle")] beer beer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.breweryID = new SelectList(db.breweries, "breweryID", "breweryLocation", beer.breweryID);
            return View(beer);
        }

        // GET: beers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            beer beer = db.beers.Find(id);
            if (beer == null)
            {
                return HttpNotFound();
            }
            return View(beer);
        }

        // POST: beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            beer beer = db.beers.Find(id);
            db.beers.Remove(beer);
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
