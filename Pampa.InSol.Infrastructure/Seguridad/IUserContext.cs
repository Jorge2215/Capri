using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pampa.InSol.Infrastructure.Seguridad
{
    public interface IUserContext
    {
        int AliasSede { get; }

        long CodigoAfipTerminal { get; }

        long IdSede { get; }

        long IdSociedad { get; }

        long IdTerminal { get; }

        long IdUsuario { get; }

        string NombreSede { get; }

        string NombreSociedad { get; }

        string NombreTerminal { get; }

        string NombreUsuario { get; }

        bool SedeEsAcopio { get; }

        bool TerminalSeleccionaCosechaPorDefecto { get; }

        bool TerminalUtilizaTarjeta { get; }

        long IdPuestoTrabajo { get; }
    }
}