using AutoMapper;
using Pampa.InSol.Entities;
using Pampa.InSol.Entities.Entities;
using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pampa.InSol.Infrastructure
{
    public static class MapperConfig
    {
        public static MapperConfiguration MapperConfiguration { get; set; }

        private static IMapper _mapper { get; set; }

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    _mapper = MapperConfiguration.CreateMapper();
                }

                return _mapper;
            }
        }

        public static void RegisterMappinngs()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioModel>().ForMember(x => x.Roles, opt => opt.Ignore());
                cfg.CreateMap<Rol, RolViewModel>().ForMember(x => x.ActivoView, opt => opt.Ignore());

                cfg.CreateMap<Ambiente, AmbienteModel>();
                cfg.CreateMap<BitacoraInterfaz, BitacoraInterfazModel>();
                cfg.CreateMap<CicloAplicativo, CicloAplicativoModel>();
                cfg.CreateMap<Obsolescencia, ObsolescenciaModel>();
                cfg.CreateMap<Sitio, SitioModel>();
                cfg.CreateMap<Servicio, ServicioModel>();
                cfg.CreateMap<TipoProducto, TipoProductoModel>();
                cfg.CreateMap<Frecuencia, FrecuenciaModel>();
                cfg.CreateMap<Interfaz, InterfazModel>();
                cfg.CreateMap<Modulo, ModuloModel>();
                cfg.CreateMap<Negocio, NegocioModel>();
                cfg.CreateMap<Producto, ProductoModel>()
                    .ForMember(x => x.Sox, o => o.MapFrom(p => p.Sox ?? false));
                cfg.CreateMap<Proceso, ProcesoModel>();
                cfg.CreateMap<ProductoAmbiente, ProductoAmbienteModel>();
                //cfg.CreateMap<ProductoNegocio, ProductoNegocioModel>();
                cfg.CreateMap<Tecnologia, TecnologiaModel>();
                cfg.CreateMap<TipoDato, TipoDatoModel>();
                cfg.CreateMap<TipoInterfaz, TipoInterfazModel>();
                cfg.CreateMap<Transporte, TransporteModel>();
                cfg.CreateMap<Ambiente, AmbienteModel>();
                cfg.CreateMap<RolAmbiente, RolAmbienteModel>();

                cfg.CreateMap<Negocio, GenericDropdownItem>();
                cfg.CreateMap<Proceso, GenericDropdownItem>();
                cfg.CreateMap<Ambiente, GenericDropdownItem>();
            });
        }
    }
}