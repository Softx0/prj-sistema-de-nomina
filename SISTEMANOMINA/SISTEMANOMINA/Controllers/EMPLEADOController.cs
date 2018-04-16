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
    public class EMPLEADOController : Controller
    {
        private SISTEMA_DE_NOMINAEntities db = new SISTEMA_DE_NOMINAEntities();

        // GET: EMPLEADO
      //  [Authorize(Roles = "Admin,Admina")]
        public ActionResult Index(String Criterio = null)
        {
            return View(db.EMPLEADO.Where(
            p => Criterio == null ||
            p.CEDULA_EMPLEADO.StartsWith(Criterio) ||
            p.COD_EMPLEADO.StartsWith(Criterio) ||
            p.DEPARTAMENTO.NOMBRE_DEPARTAMENTO.StartsWith(Criterio) ||
            p.PUESTO.NOMBRE_PUESTO.StartsWith(Criterio) ||
            p.NOMBRE_EMPLEADO.StartsWith(Criterio) ||
            p.SALARIO_MENSUAL_EMPLEADO.ToString().StartsWith(Criterio)).ToList());
        }

        // GET: EMPLEADO/Details/5
      //  [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADO);
        }

        // GET: EMPLEADO/Create
    //    [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ID_DEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "ID_DEPARTAMENTO", "NOMBRE_DEPARTAMENTO");
            ViewBag.ID_PUESTO = new SelectList(db.PUESTO, "ID_PUESTO", "NOMBRE_PUESTO");
            return View();
        }

        // POST: EMPLEADO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      //  [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID_EMPLEADO,COD_EMPLEADO,CEDULA_EMPLEADO,NOMBRE_EMPLEADO,ID_DEPARTAMENTO,ID_PUESTO,SALARIO_MENSUAL_EMPLEADO,RESPONSABLE_AREA")] EMPLEADO eMPLEADO)
        {
            if (!validaCedula(eMPLEADO.CEDULA_EMPLEADO))
                ModelState.AddModelError("CEDULA_EMPLEADO", "Cedula Incorrecta");
            if (ModelState.IsValid)
            {
                db.EMPLEADO.Add(eMPLEADO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_DEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "ID_DEPARTAMENTO", "NOMBRE_DEPARTAMENTO", eMPLEADO.ID_DEPARTAMENTO);
            ViewBag.ID_PUESTO = new SelectList(db.PUESTO, "ID_PUESTO", "NOMBRE_PUESTO", eMPLEADO.ID_PUESTO);
            return View(eMPLEADO);
        }

        // GET: EMPLEADO/Edit/5
    //    [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_DEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "ID_DEPARTAMENTO", "NOMBRE_DEPARTAMENTO", eMPLEADO.ID_DEPARTAMENTO);
            ViewBag.ID_PUESTO = new SelectList(db.PUESTO, "ID_PUESTO", "NOMBRE_PUESTO", eMPLEADO.ID_PUESTO);
            return View(eMPLEADO);
        }

        // POST: EMPLEADO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID_EMPLEADO,COD_EMPLEADO,CEDULA_EMPLEADO,NOMBRE_EMPLEADO,ID_DEPARTAMENTO,ID_PUESTO,SALARIO_MENSUAL_EMPLEADO,RESPONSABLE_AREA")] EMPLEADO eMPLEADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPLEADO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_DEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "ID_DEPARTAMENTO", "NOMBRE_DEPARTAMENTO", eMPLEADO.ID_DEPARTAMENTO);
            ViewBag.ID_PUESTO = new SelectList(db.PUESTO, "ID_PUESTO", "NOMBRE_PUESTO", eMPLEADO.ID_PUESTO);
            return View(eMPLEADO);
        }

        // GET: EMPLEADO/Delete/5
      //  [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADO);
        }

        // POST: EMPLEADO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
       // [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            db.EMPLEADO.Remove(eMPLEADO);
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

        public static bool validaCedula(string pCedula)
        {
            int vnTotal = 0;
            string vcCedula = pCedula.Replace("-", "");
            int pLongCed = vcCedula.Trim().Length;
            int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            if ((vcCedula == "00000000000"))
                return false;
            if (pLongCed < 11 || pLongCed > 11)
                return false;

            for (int vDig = 1; vDig <= pLongCed; vDig++)
            {
                int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                if (vCalculo < 10)
                    vnTotal += vCalculo;
                else
                    vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
            }

            if (vnTotal % 10 == 0)
                return true;
            else
                return false;
        }
    }
}
