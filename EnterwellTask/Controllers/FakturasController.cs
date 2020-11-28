using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using EnterwellTask.Extensions;
using EnterwellTask.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Rest.ClientRuntime.Azure.Authentication.Utilities;

namespace EnterwellTask.Controllers
{
    [Authorize]
    public class FakturasController : Controller
    {
        [Import(typeof(IPorez))] private IPorez taxCalculator;

        private EnterwellDBContext db = new EnterwellDBContext();
        private ApplicationDbContext appdb = new ApplicationDbContext();

        public FakturasController()
        {
            Mef.Compose(this);
        }
        [Authorize]
        // GET: Fakturas
        public ActionResult Index()
        {

             List<Faktura> fakture = db.Faktura.ToList();
                List<FakturaViewModel> model = new List<FakturaViewModel>();
                foreach (Faktura f in fakture)
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

                Faktura fakturaModel = db.Faktura.Find(id);
                List<StavkeFakture> sveStavke = new List<StavkeFakture>();
                sveStavke = db.StavkeFakture.Where(stavka => stavka.FakturaID == id).ToList();

                List<StavkaViewModel> modelStavke = new List<StavkaViewModel>();

                foreach (var s in sveStavke)
                {
                    StavkaViewModel addStavkaModel = new StavkaViewModel();
                    addStavkaModel.Opis = (db.Stavka.Find(s.StavkaID).Opis);
                    addStavkaModel.Kolicina = s.Kolicina.ToString();
                    modelStavke.Add(addStavkaModel);
                }


                FakturaViewModel model = new FakturaViewModel();
                model.Stavke = modelStavke;
                model.PrimateljRacuna = fakturaModel.PrimateljRacuna;
                model.FakturaID = fakturaModel.FakturaID;
                model.DatumDospijeca = fakturaModel.DatumDospijeca;
                model.DatumStvaranja = fakturaModel.DatumStvaranja;
                model.UkupnaCijenaBezPoreza = fakturaModel.UkupnaCijenaBezPoreza;
                model.UkupnaCijenaSaPorezom = fakturaModel.UkupnaCijenaSaPorezom;
                model.UserID = fakturaModel.UserID;
                model.Username = appdb.Users.Find(fakturaModel.UserID).Email;

                if (model == null)
                {
                    return HttpNotFound();
                }
                return View(model);
          
        }

        // GET: Fakturas/Create
        public ActionResult Create()
        {
                CreateFakturaModel model = new CreateFakturaModel();
                model.Stavke = db.Stavka.ToList();

                return View(model);
        }
        [Route("AddFaktura")]
        [HttpPost]
        public ActionResult AddFaktura(RequestModel model)
        {
            float fullprice = (float)taxCalculator.IzracunajCijenu(model.Cijena,  (Faktura.PorezDrzave)Enum.Parse(typeof(Faktura.PorezDrzave), model.Taksa));

            Faktura faktura = new Faktura();

            if (ModelState.IsValid)
                {
                    faktura.DatumDospijeca = model.DatumDospijeca.AsDateTime();
                    faktura.DatumStvaranja = DateTime.Now;
                    faktura.PrimateljRacuna = model.Primatelj;
                    faktura.UkupnaCijenaBezPoreza = model.Cijena;
                    faktura.UkupnaCijenaSaPorezom =fullprice;
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
