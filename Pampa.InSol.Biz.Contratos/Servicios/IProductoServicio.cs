using Pampa.InSol.Entities.Entities;
using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Biz.Contratos.Servicios
{
    public interface IProductoServicio : IServicio<Producto>
    {
        Producto GetProductoById(int id);

        Producto Save(ProductoModel entity);

        bool Delete(int id);

        void DeleteModuloRelacionado(int idProducto, string nombre);
    }
}
