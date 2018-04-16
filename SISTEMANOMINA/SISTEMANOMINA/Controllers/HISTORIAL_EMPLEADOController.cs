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
    public class HISTORIAL_EMPLEADOController : Controller
    {
        private SISTEMA_DE_NOMINAEntities db = new SISTEMA_DE_NOMINAEntities();

        // GET: HISTORIAL_EMPLEADO
        public ActionResult Index(String Criterio = null)
        {
            return View(db.HISTORIAL_EMPLEADO.Where(
                p => Criterio == null ||
                p.NOMBRE_EMPLEADO.StartsWith(Criterio) ||
                p.TIPO.StartsWith(Criterio) ||
                p.MONTO.ToString().StartsWith(Criterio)).ToList());
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
