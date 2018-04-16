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
    public class PUESTOController : Controller
    {
        private SISTEMA_DE_NOMINAEntities db = new SISTEMA_DE_NOMINAEntities();

        // GET: PUESTO
       // [Authorize(Roles = "Admin")]
        public ActionResult Index(String Criterio = null)
        {
            var pUESTO = db.PUESTO.Include(p => p.NIVEL_RIESGO);
            return View(pUESTO.Where(
                p => Criterio == null ||
                p.NOMBRE_PUESTO.StartsWith(Criterio) ||
                p.NIVEL_SALARIO_MIN.ToString().StartsWith(Criterio) ||
                p.NIVEL_SALARIO_MAX.ToString().StartsWith(Criterio) ||
                p.NIVEL_RIESGO.TIPO_RIESGO.StartsWith(Criterio)).ToList());
        }

        // GET: PUESTO/Details/5
       // [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUESTO pUESTO = db.PUESTO.Find(id);
            if (pUESTO == null)
            {
                return HttpNotFound();
            }
            return View(pUESTO);
        }

        // GET: PUESTO/Create
       // [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ID_NIVEL_RIESGO = new SelectList(db.NIVEL_RIESGO, "ID_NIVEL_RIESGO", "TIPO_RIESGO");
            return View();
        }

        // POST: PUESTO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
     //   [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID_PUESTO,NOMBRE_PUESTO,ID_NIVEL_RIESGO,NIVEL_SALARIO_MIN,NIVEL_SALARIO_MAX")] PUESTO pUESTO)
        {
            if (ModelState.IsValid)
            {
                db.PUESTO.Add(pUESTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_NIVEL_RIESGO = new SelectList(db.NIVEL_RIESGO, "ID_NIVEL_RIESGO", "TIPO_RIESGO", pUESTO.ID_NIVEL_RIESGO);
            return View(pUESTO);
        }

        // GET: PUESTO/Edit/5
     //   [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUESTO pUESTO = db.PUESTO.Find(id);
            if (pUESTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_NIVEL_RIESGO = new SelectList(db.NIVEL_RIESGO, "ID_NIVEL_RIESGO", "TIPO_RIESGO", pUESTO.ID_NIVEL_RIESGO);
            return View(pUESTO);
        }

        // POST: PUESTO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID_PUESTO,NOMBRE_PUESTO,ID_NIVEL_RIESGO,NIVEL_SALARIO_MIN,NIVEL_SALARIO_MAX")] PUESTO pUESTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pUESTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_NIVEL_RIESGO = new SelectList(db.NIVEL_RIESGO, "ID_NIVEL_RIESGO", "TIPO_RIESGO", pUESTO.ID_NIVEL_RIESGO);
            return View(pUESTO);
        }

        // GET: PUESTO/Delete/5
      //  [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUESTO pUESTO = db.PUESTO.Find(id);
            if (pUESTO == null)
            {
                return HttpNotFound();
            }
            return View(pUESTO);
        }

        // POST: PUESTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
      //  [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            PUESTO pUESTO = db.PUESTO.Find(id);
            db.PUESTO.Remove(pUESTO);
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
