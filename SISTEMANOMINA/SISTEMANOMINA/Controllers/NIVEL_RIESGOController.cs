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
    public class NIVEL_RIESGOController : Controller
    {
        private SISTEMA_DE_NOMINAEntities db = new SISTEMA_DE_NOMINAEntities();

        // GET: NIVEL_RIESGO
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.NIVEL_RIESGO.ToList());
        }

        // GET: NIVEL_RIESGO/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NIVEL_RIESGO nIVEL_RIESGO = db.NIVEL_RIESGO.Find(id);
            if (nIVEL_RIESGO == null)
            {
                return HttpNotFound();
            }
            return View(nIVEL_RIESGO);
        }

        // GET: NIVEL_RIESGO/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NIVEL_RIESGO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID_NIVEL_RIESGO,TIPO_RIESGO")] NIVEL_RIESGO nIVEL_RIESGO)
        {
            if (ModelState.IsValid)
            {
                db.NIVEL_RIESGO.Add(nIVEL_RIESGO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nIVEL_RIESGO);
        }

        // GET: NIVEL_RIESGO/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NIVEL_RIESGO nIVEL_RIESGO = db.NIVEL_RIESGO.Find(id);
            if (nIVEL_RIESGO == null)
            {
                return HttpNotFound();
            }
            return View(nIVEL_RIESGO);
        }

        // POST: NIVEL_RIESGO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID_NIVEL_RIESGO,TIPO_RIESGO")] NIVEL_RIESGO nIVEL_RIESGO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nIVEL_RIESGO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nIVEL_RIESGO);
        }

        // GET: NIVEL_RIESGO/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NIVEL_RIESGO nIVEL_RIESGO = db.NIVEL_RIESGO.Find(id);
            if (nIVEL_RIESGO == null)
            {
                return HttpNotFound();
            }
            return View(nIVEL_RIESGO);
        }

        // POST: NIVEL_RIESGO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            NIVEL_RIESGO nIVEL_RIESGO = db.NIVEL_RIESGO.Find(id);
            db.NIVEL_RIESGO.Remove(nIVEL_RIESGO);
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
