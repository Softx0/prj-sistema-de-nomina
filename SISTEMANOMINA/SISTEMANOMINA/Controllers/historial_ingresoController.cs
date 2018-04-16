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
    public class historial_ingresoController : Controller
    {
        private SISTEMA_DE_NOMINAEntities db = new SISTEMA_DE_NOMINAEntities();

        // GET: historial_ingreso
        public ActionResult Index()
        {
            return View(db.historial_ingreso.ToList());
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
