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
    public class Historial_DeduccionesController : Controller
    {
        private SISTEMA_DE_NOMINAEntities db = new SISTEMA_DE_NOMINAEntities();
        // GET: Historial_Deducciones
        public ActionResult Index(String Criterio = null)
        {
            return View(db.Historial_Deducciones.Where(
                p => Criterio == null ||
                p.NOMBRE_EMPLEADO.StartsWith(Criterio) ||
                p.NOMBRE_TIPO_DEDUCCION.StartsWith(Criterio) ||
                p.MONTO_DEDUCCION.ToString().StartsWith(Criterio)).ToList());
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
