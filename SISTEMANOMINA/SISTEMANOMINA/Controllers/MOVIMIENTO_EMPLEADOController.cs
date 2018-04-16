using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SISTEMANOMINA;

namespace SISTEMANOMINA.Controllers
{
    public class MOVIMIENTO_EMPLEADOController : Controller
    {
        private SISTEMA_DE_NOMINAEntities db = new SISTEMA_DE_NOMINAEntities();

        // GET: MOVIMIENTO_EMPLEADO
        public ActionResult Index(String Criterio = null)
        {
            var mOVIMIENTO_EMPLEADO = db.MOVIMIENTO_EMPLEADO.Include(m => m.EMPLEADO);
              
            return View(mOVIMIENTO_EMPLEADO.Where(
                p => Criterio == null ||
                p.EMPLEADO.NOMBRE_EMPLEADO.StartsWith(Criterio) ||
                p.MONTO_PAGAR.ToString().StartsWith(Criterio)).ToList());
        }

        // GET: MOVIMIENTO_EMPLEADO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVIMIENTO_EMPLEADO mOVIMIENTO_EMPLEADO = db.MOVIMIENTO_EMPLEADO.Find(id);
            if (mOVIMIENTO_EMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(mOVIMIENTO_EMPLEADO);
        }

        // GET: MOVIMIENTO_EMPLEADO/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "COD_EMPLEADO");
            return View();
        }

        // POST: MOVIMIENTO_EMPLEADO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_MOVIMIENTO,ID_EMPLEADO,MONTO_PAGAR")] MOVIMIENTO_EMPLEADO mOVIMIENTO_EMPLEADO)
        {
            if (ModelState.IsValid)
            {
                db.MOVIMIENTO_EMPLEADO.Add(mOVIMIENTO_EMPLEADO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "COD_EMPLEADO", mOVIMIENTO_EMPLEADO.ID_EMPLEADO);
            return View(mOVIMIENTO_EMPLEADO);
        }

        // GET: MOVIMIENTO_EMPLEADO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVIMIENTO_EMPLEADO mOVIMIENTO_EMPLEADO = db.MOVIMIENTO_EMPLEADO.Find(id);
            if (mOVIMIENTO_EMPLEADO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "COD_EMPLEADO", mOVIMIENTO_EMPLEADO.ID_EMPLEADO);
            return View(mOVIMIENTO_EMPLEADO);
        }

        // POST: MOVIMIENTO_EMPLEADO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_MOVIMIENTO,ID_EMPLEADO,MONTO_PAGAR")] MOVIMIENTO_EMPLEADO mOVIMIENTO_EMPLEADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mOVIMIENTO_EMPLEADO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "COD_EMPLEADO", mOVIMIENTO_EMPLEADO.ID_EMPLEADO);
            return View(mOVIMIENTO_EMPLEADO);
        }

        // GET: MOVIMIENTO_EMPLEADO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVIMIENTO_EMPLEADO mOVIMIENTO_EMPLEADO = db.MOVIMIENTO_EMPLEADO.Find(id);
            if (mOVIMIENTO_EMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(mOVIMIENTO_EMPLEADO);
        }

        // POST: MOVIMIENTO_EMPLEADO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MOVIMIENTO_EMPLEADO mOVIMIENTO_EMPLEADO = db.MOVIMIENTO_EMPLEADO.Find(id);
            db.MOVIMIENTO_EMPLEADO.Remove(mOVIMIENTO_EMPLEADO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult exportaExcel()
        {
            string filename = "ReporteNomina.xlsx";
            string filepath = @"c:\tmp\" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("Empleado ,    Monto a Pagar"); //Encabezado 
            foreach (var i in db.MOVIMIENTO_EMPLEADO.ToList())
            {
                sw.WriteLine("" + i.EMPLEADO.NOMBRE_EMPLEADO + "    " + i.MONTO_PAGAR + "" );
            }
            sw.Close();

            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = false,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
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
