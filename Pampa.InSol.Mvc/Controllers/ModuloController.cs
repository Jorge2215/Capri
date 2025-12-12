using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Entities.Entities;
using Pampa.InSol.Entities.Models;

namespace Pampa.InSol.Mvc.Controllers
{
    public class ModuloController : BaseController
    {
        private readonly IModuloServicio moduloServicio;

        public ModuloController(IModuloServicio moduloServicio)
        {
            this.moduloServicio = moduloServicio;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}