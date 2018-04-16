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
    public class TIPO_DE_DEDUCCIONController : Controller
    {
        private SISTEMA_DE_NOMINAEntities db = new SISTEMA_DE_NOMINAEntities();

        // GET: TIPO_DE_DEDUCCION
        [Authorize(Roles = "Admin")]
        public ActionResult Index(String Criterio = null)
        {
            return View(db.TIPO_DE_DEDUCCION.Where(
                p => Criterio == null ||
                p.NOMBRE_TIPO_DEDUCCION.StartsWith(Criterio) ||
                p.PORCENTAJE.ToString().StartsWith(Criterio)).ToList());
        }

        // GET: TIPO_DE_DEDUCCION/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_DE_DEDUCCION tIPO_DE_DEDUCCION = db.TIPO_DE_DEDUCCION.Find(id);
            if (tIPO_DE_DEDUCCION == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_DE_DEDUCCION);
        }

        // GET: TIPO_DE_DEDUCCION/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPO_DE_DEDUCCION/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID_TIPO_DEDUCCION,NOMBRE_TIPO_DEDUCCION,PORCENTAJE")] TIPO_DE_DEDUCCION tIPO_DE_DEDUCCION)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_DE_DEDUCCION.Add(tIPO_DE_DEDUCCION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPO_DE_DEDUCCION);
        }

        // GET: TIPO_DE_DEDUCCION/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_DE_DEDUCCION tIPO_DE_DEDUCCION = db.TIPO_DE_DEDUCCION.Find(id);
            if (tIPO_DE_DEDUCCION == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_DE_DEDUCCION);
        }

        // POST: TIPO_DE_DEDUCCION/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID_TIPO_DEDUCCION,NOMBRE_TIPO_DEDUCCION,PORCENTAJE")] TIPO_DE_DEDUCCION tIPO_DE_DEDUCCION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_DE_DEDUCCION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPO_DE_DEDUCCION);
        }

        // GET: TIPO_DE_DEDUCCION/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_DE_DEDUCCION tIPO_DE_DEDUCCION = db.TIPO_DE_DEDUCCION.Find(id);
            if (tIPO_DE_DEDUCCION == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_DE_DEDUCCION);
        }

        // POST: TIPO_DE_DEDUCCION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            TIPO_DE_DEDUCCION tIPO_DE_DEDUCCION = db.TIPO_DE_DEDUCCION.Find(id);
            db.TIPO_DE_DEDUCCION.Remove(tIPO_DE_DEDUCCION);
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
