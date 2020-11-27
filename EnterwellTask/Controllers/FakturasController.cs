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
        private ApplicationDbContext appdb = new ApplicationDbContext();

        // GET: Fakturas
        public ActionResult Index()
        {
            List<Faktura> fakture = db.Faktura.ToList();
            List<FakturaViewModel> model = new List<FakturaViewModel>();
            foreach(Faktura f in fakture)
            {
                FakturaViewModel addModel = new FakturaViewModel();
                addModel.PrimateljRacuna = f.PrimateljRacuna;
                addModel.FakturaID = f.FakturaID;
                addModel.DatumDospijeca = f.DatumDospijeca;
                addModel.DatumStvaranja = f.DatumStvaranja;
                addModel.UkupnaCijenaBezPoreza = f.UkupnaCijenaBezPoreza;
                addModel.UkupnaCijenaSaPorezom = f.UkupnaCijenaSaPorezom;
                addModel.UserID = f.UserID;
                addModel.Username = appdb.Users.Find(f.UserID).Email;

                model.Add(addModel);
            }
            return View(model);
        }

        // GET: Fakturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura f = db.Faktura.Find(id);
            FakturaViewModel addModel = new FakturaViewModel();
            addModel.PrimateljRacuna = f.PrimateljRacuna;
            addModel.FakturaID = f.FakturaID;
            addModel.DatumDospijeca = f.DatumDospijeca;
            addModel.DatumStvaranja = f.DatumStvaranja;
            addModel.UkupnaCijenaBezPoreza = f.UkupnaCijenaBezPoreza;
            addModel.UkupnaCijenaSaPorezom = f.UkupnaCijenaSaPorezom;
            addModel.UserID = f.UserID;
            addModel.Username = appdb.Users.Find(f.UserID).Email;


            if (addModel == null)
            {
                return HttpNotFound();
            }
            return View(addModel);
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
        public ActionResult test(RequestModel model)
        {
            Faktura faktura = new Faktura();
            if (ModelState.IsValid)
            {
                faktura.DatumDospijeca = model.DatumDospijeca.AsDateTime();
                faktura.DatumStvaranja = DateTime.Now;
                faktura.PrimateljRacuna = model.Primatelj;
                faktura.UkupnaCijenaBezPoreza = model.Cijena;
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
                return RedirectToAction("Index");
            }
            return View();
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
