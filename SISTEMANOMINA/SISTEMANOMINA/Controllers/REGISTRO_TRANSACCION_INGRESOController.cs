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
    public class REGISTRO_TRANSACCION_INGRESOController : Controller
    {
        private SISTEMA_DE_NOMINAEntities db = new SISTEMA_DE_NOMINAEntities();

        // GET: REGISTRO_TRANSACCION_INGRESO
        public ActionResult Index(String Criterio = null)
        {
            var rEGISTRO_TRANSACCION_INGRESO = db.REGISTRO_TRANSACCION_INGRESO.Include(r => r.EMPLEADO);
            return View(rEGISTRO_TRANSACCION_INGRESO.Where(
                p => Criterio == null ||
                p.FECHA.ToString().StartsWith(Criterio) ||
                p.EMPLEADO.NOMBRE_EMPLEADO.StartsWith(Criterio) ||
                p.MONTO.ToString().StartsWith(Criterio)).ToList());
        }

        // GET: REGISTRO_TRANSACCION_INGRESO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRO_TRANSACCION_INGRESO rEGISTRO_TRANSACCION_INGRESO = db.REGISTRO_TRANSACCION_INGRESO.Find(id);
            if (rEGISTRO_TRANSACCION_INGRESO == null)
            {
                return HttpNotFound();
            }
            return View(rEGISTRO_TRANSACCION_INGRESO);
        }

        // GET: REGISTRO_TRANSACCION_INGRESO/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "COD_EMPLEADO");
            return View();
        }

        // POST: REGISTRO_TRANSACCION_INGRESO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TRANSACCION_INGRESO,ID_EMPLEADO,TIPO_INGRESO,FECHA,MONTO")] REGISTRO_TRANSACCION_INGRESO rEGISTRO_TRANSACCION_INGRESO)
        {
            if (ModelState.IsValid)
            {
                db.REGISTRO_TRANSACCION_INGRESO.Add(rEGISTRO_TRANSACCION_INGRESO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "COD_EMPLEADO", rEGISTRO_TRANSACCION_INGRESO.ID_EMPLEADO);
            return View(rEGISTRO_TRANSACCION_INGRESO);
        }

        // GET: REGISTRO_TRANSACCION_INGRESO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRO_TRANSACCION_INGRESO rEGISTRO_TRANSACCION_INGRESO = db.REGISTRO_TRANSACCION_INGRESO.Find(id);
            if (rEGISTRO_TRANSACCION_INGRESO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "COD_EMPLEADO", rEGISTRO_TRANSACCION_INGRESO.ID_EMPLEADO);
            return View(rEGISTRO_TRANSACCION_INGRESO);
        }

        // POST: REGISTRO_TRANSACCION_INGRESO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TRANSACCION_INGRESO,ID_EMPLEADO,TIPO_INGRESO,FECHA,MONTO")] REGISTRO_TRANSACCION_INGRESO rEGISTRO_TRANSACCION_INGRESO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEGISTRO_TRANSACCION_INGRESO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "COD_EMPLEADO", rEGISTRO_TRANSACCION_INGRESO.ID_EMPLEADO);
            return View(rEGISTRO_TRANSACCION_INGRESO);
        }

        // GET: REGISTRO_TRANSACCION_INGRESO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRO_TRANSACCION_INGRESO rEGISTRO_TRANSACCION_INGRESO = db.REGISTRO_TRANSACCION_INGRESO.Find(id);
            if (rEGISTRO_TRANSACCION_INGRESO == null)
            {
                return HttpNotFound();
            }
            return View(rEGISTRO_TRANSACCION_INGRESO);
        }

        // POST: REGISTRO_TRANSACCION_INGRESO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REGISTRO_TRANSACCION_INGRESO rEGISTRO_TRANSACCION_INGRESO = db.REGISTRO_TRANSACCION_INGRESO.Find(id);
            db.REGISTRO_TRANSACCION_INGRESO.Remove(rEGISTRO_TRANSACCION_INGRESO);
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
