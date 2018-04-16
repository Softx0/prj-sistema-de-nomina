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
    public class REGISTRO_TRANSACCION_DEDUCCIONController : Controller
    {
        private SISTEMA_DE_NOMINAEntities db = new SISTEMA_DE_NOMINAEntities();

        // GET: REGISTRO_TRANSACCION_DEDUCCION
      //  [Authorize(Roles = "Admin")]
        public ActionResult Index(String Criterio = null)
        {
            var rEGISTRO_TRANSACCION_DEDUCCION = db.REGISTRO_TRANSACCION_DEDUCCION.Include(r => r.EMPLEADO).Include(r => r.TIPO_DE_DEDUCCION);
            return View(rEGISTRO_TRANSACCION_DEDUCCION.Where(
                p => Criterio == null ||
                p.FECHA.ToString().StartsWith(Criterio) ||
                p.EMPLEADO.NOMBRE_EMPLEADO.StartsWith(Criterio) ||
                p.TIPO_DE_DEDUCCION.NOMBRE_TIPO_DEDUCCION.StartsWith(Criterio) ||
                p.MONTO.ToString().StartsWith(Criterio)).ToList());
        }

        // GET: REGISTRO_TRANSACCION_DEDUCCION/Details/5
     //   [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRO_TRANSACCION_DEDUCCION rEGISTRO_TRANSACCION_DEDUCCION = db.REGISTRO_TRANSACCION_DEDUCCION.Find(id);
            if (rEGISTRO_TRANSACCION_DEDUCCION == null)
            {
                return HttpNotFound();
            }
            return View(rEGISTRO_TRANSACCION_DEDUCCION);
        }

        // GET: REGISTRO_TRANSACCION_DEDUCCION/Create
    //    [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "NOMBRE_EMPLEADO");
            ViewBag.ID_TIPO_DEDUCCION = new SelectList(db.TIPO_DE_DEDUCCION, "ID_TIPO_DEDUCCION", "NOMBRE_TIPO_DEDUCCION");
            return View();
        }

        // POST: REGISTRO_TRANSACCION_DEDUCCION/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
     //   [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID_TRANSACCION_DEDUCCION,ID_EMPLEADO,ID_TIPO_DEDUCCION,FECHA,MONTO")] REGISTRO_TRANSACCION_DEDUCCION rEGISTRO_TRANSACCION_DEDUCCION)
        {
            if (ModelState.IsValid)
            {
                db.REGISTRO_TRANSACCION_DEDUCCION.Add(rEGISTRO_TRANSACCION_DEDUCCION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "NOMBRE_EMPLEADO", rEGISTRO_TRANSACCION_DEDUCCION.ID_EMPLEADO);
            ViewBag.ID_TIPO_DEDUCCION = new SelectList(db.TIPO_DE_DEDUCCION, "ID_TIPO_DEDUCCION", "NOMBRE_TIPO_DEDUCCION", rEGISTRO_TRANSACCION_DEDUCCION.ID_TIPO_DEDUCCION);
            return View(rEGISTRO_TRANSACCION_DEDUCCION);
        }

        // GET: REGISTRO_TRANSACCION_DEDUCCION/Edit/5
     //   [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRO_TRANSACCION_DEDUCCION rEGISTRO_TRANSACCION_DEDUCCION = db.REGISTRO_TRANSACCION_DEDUCCION.Find(id);
            if (rEGISTRO_TRANSACCION_DEDUCCION == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "COD_EMPLEADO", rEGISTRO_TRANSACCION_DEDUCCION.ID_EMPLEADO);
            ViewBag.ID_TIPO_DEDUCCION = new SelectList(db.TIPO_DE_DEDUCCION, "ID_TIPO_DEDUCCION", "NOMBRE_TIPO_DEDUCCION", rEGISTRO_TRANSACCION_DEDUCCION.ID_TIPO_DEDUCCION);
            return View(rEGISTRO_TRANSACCION_DEDUCCION);
        }

        // POST: REGISTRO_TRANSACCION_DEDUCCION/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
     //   [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID_TRANSACCION_DEDUCCION,ID_EMPLEADO,ID_TIPO_DEDUCCION,FECHA,MONTO")] REGISTRO_TRANSACCION_DEDUCCION rEGISTRO_TRANSACCION_DEDUCCION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEGISTRO_TRANSACCION_DEDUCCION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "COD_EMPLEADO", rEGISTRO_TRANSACCION_DEDUCCION.ID_EMPLEADO);
            ViewBag.ID_TIPO_DEDUCCION = new SelectList(db.TIPO_DE_DEDUCCION, "ID_TIPO_DEDUCCION", "NOMBRE_TIPO_DEDUCCION", rEGISTRO_TRANSACCION_DEDUCCION.ID_TIPO_DEDUCCION);
            return View(rEGISTRO_TRANSACCION_DEDUCCION);
        }

        // GET: REGISTRO_TRANSACCION_DEDUCCION/Delete/5
     //   [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRO_TRANSACCION_DEDUCCION rEGISTRO_TRANSACCION_DEDUCCION = db.REGISTRO_TRANSACCION_DEDUCCION.Find(id);
            if (rEGISTRO_TRANSACCION_DEDUCCION == null)
            {
                return HttpNotFound();
            }
            return View(rEGISTRO_TRANSACCION_DEDUCCION);
        }

        // POST: REGISTRO_TRANSACCION_DEDUCCION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
     //   [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            REGISTRO_TRANSACCION_DEDUCCION rEGISTRO_TRANSACCION_DEDUCCION = db.REGISTRO_TRANSACCION_DEDUCCION.Find(id);
            db.REGISTRO_TRANSACCION_DEDUCCION.Remove(rEGISTRO_TRANSACCION_DEDUCCION);
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
