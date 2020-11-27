using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using EnterwellTask.Models;
using Microsoft.AspNet.Identity;

namespace EnterwellTask.Controllers
{
    public class FakturasController : Controller
    {
        private EnterwellDBContext db = new EnterwellDBContext();

        // GET: Fakturas
        public ActionResult Index()
        {
            return View(db.Faktura.ToList());
        }

        // GET: Fakturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Faktura.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // GET: Fakturas/Create
        public ActionResult Create()
        {
            CreateFakturaModel model = new CreateFakturaModel();
            model.Stavke = db.Stavka.ToList();

            return View(model);
        }
        [Route("test")]
        [HttpPost]
        public void test(RequestModel model)
        {
            Faktura faktura = new Faktura();
            if (ModelState.IsValid)
            {
                faktura.DatumDospijeca = model.DatumDospijeca.AsDateTime();
                faktura.DatumStvaranja = DateTime.Now;
                faktura.PrimateljRacuna = model.Primatelj;
                faktura.UkupnaCijenaBezPoreza = 0;
                faktura.UkupnaCijenaSaPorezom = 0;
                faktura.UserID = User.Identity.GetUserId();

                db.Faktura.Add(faktura);
                db.SaveChanges();

                var lastId = db.Faktura.OrderByDescending(p => p.FakturaID).FirstOrDefault().FakturaID;
                

                foreach (ListRequestModel item in model.Stavke)
                {
                    StavkeFakture newModel = new StavkeFakture();
                    newModel.FakturaID = lastId;
                    newModel.StavkaID = Convert.ToInt32(item.Id);
                    newModel.Kolicina = Convert.ToInt32(item.Kolicina);
                    db.StavkeFakture.Add(newModel);
                    db.SaveChanges();

                }

                //return RedirectToAction("Index");
            }
        }

       
        // GET: Fakturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Faktura.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // POST: Fakturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FakturaID,DatumStvaranja,DatumDospijeca,UkupnaCijenaBezPoreza,UkupnaCijenaSaPorezom,UserID,PrimateljRacuna")] Faktura faktura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faktura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faktura);
        }

        // GET: Fakturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Faktura.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // POST: Fakturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faktura faktura = db.Faktura.Find(id);
            db.Faktura.Remove(faktura);
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
