using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pampa.InSol.Entities.Models
{
    public class RolViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public string ActivoView { get => Activo ? "SI" : "NO"; }
    }

    public class FuncionViewModel
    {
        public int Id { get; set; }
        public string Descipcion { get; set; }
        public int? IdPadre { get; set; }
        public bool Asignado { get; set; }
    }
    public class RolFilterViewModel
    {
        public RolFilterViewModel()
        {
            SoloActivos = true;
        }
        public string Descripcion { get; set; }
        public bool SoloActivos { get; set; }
    }
    public class RolFuncionViewModel
    {
        public long RolId { get; set; }
        public long FuncionId { get; set; }
    }
    public class ItemModel
    {
        public ItemModel()
        {
            Children = new List<ItemModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdPadre { get; set; }
        public List<ItemModel> Children { get; set; }
    }

    public class NodeModel
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public NodeStateModel state { get; set; }
    }
    public class NodeStateModel
    {
        public bool ischecked { get; set; }
    }

}