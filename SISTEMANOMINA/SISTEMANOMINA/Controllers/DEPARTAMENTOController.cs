using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SISTEMANOMINA;

namespace SISTEMANOMINA.Controllers
{
    public class DEPARTAMENTOController : Controller
    {
        private SISTEMA_DE_NOMINAEntities db = new SISTEMA_DE_NOMINAEntities();

        // GET: DEPARTAMENTO
      //  [Authorize(Roles = "Admin")]
        public ActionResult Index(String Criterio = null)
        {
            return View(db.DEPARTAMENTO.Where(
            p => Criterio == null ||
            p.NOMBRE_DEPARTAMENTO.StartsWith(Criterio) ||
            p.UBICACION_FISICA_DEPARTAMENTO.StartsWith(Criterio)).ToList());
        }

        // GET: DEPARTAMENTO/Details/5
      //  [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTAMENTO dEPARTAMENTO = db.DEPARTAMENTO.Find(id);
            if (dEPARTAMENTO == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTAMENTO);
        }

        // GET: DEPARTAMENTO/Create
      //  [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DEPARTAMENTO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
     //   [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID_DEPARTAMENTO,NOMBRE_DEPARTAMENTO,UBICACION_FISICA_DEPARTAMENTO")] DEPARTAMENTO dEPARTAMENTO)
        {
            if (ModelState.IsValid)
            {
                db.DEPARTAMENTO.Add(dEPARTAMENTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dEPARTAMENTO);
        }

        // GET: DEPARTAMENTO/Edit/5
       // [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTAMENTO dEPARTAMENTO = db.DEPARTAMENTO.Find(id);
            if (dEPARTAMENTO == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTAMENTO);
        }

        // POST: DEPARTAMENTO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID_DEPARTAMENTO,NOMBRE_DEPARTAMENTO,UBICACION_FISICA_DEPARTAMENTO")] DEPARTAMENTO dEPARTAMENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dEPARTAMENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dEPARTAMENTO);
        }

        // GET: DEPARTAMENTO/Delete/5
      //  [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTAMENTO dEPARTAMENTO = db.DEPARTAMENTO.Find(id);
            if (dEPARTAMENTO == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTAMENTO);
        }

        // POST: DEPARTAMENTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
     //   [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            DEPARTAMENTO dEPARTAMENTO = db.DEPARTAMENTO.Find(id);
            db.DEPARTAMENTO.Remove(dEPARTAMENTO);
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
