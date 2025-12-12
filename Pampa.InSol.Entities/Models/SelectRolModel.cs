using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Pampa.InSol.Entities.Models
{
    public class SelectRolModel
    {
        public SelectRolModel()
        {
            RolesAsignados = new List<SelectListItem>();
            RolesDisponibles = new List<SelectListItem>();
        }

        public List<SelectListItem> RolesDisponibles { get; set; }
        public List<SelectListItem> RolesAsignados { get; set; }

    }
}
