using System.Collections.Generic;

namespace Pampa.InSol.Biz.Contratos.Servicios.Entidad
{
    public interface IABMServicio<TEntity, TModelo, TFiltro>
    {
        List<TModelo> Buscar(TFiltro filtro);

        void Eliminar(int id);
    }
}
