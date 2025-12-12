using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pampa.InSol.Infrastructure.Seguridad
{
    public class JwtSecurityToken
    {
        private const String CodeAnalysisSupressionJustification = "Los miembros de esta clase respetan la nomenclatura especificada en la RFC 6749.";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = CodeAnalysisSupressionJustification)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "token", Justification = CodeAnalysisSupressionJustification)]
        public String Access_token { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = CodeAnalysisSupressionJustification)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "in", Justification = CodeAnalysisSupressionJustification)]
        public Int32 Expires_in { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = CodeAnalysisSupressionJustification)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "type", Justification = CodeAnalysisSupressionJustification)]
        public String Token_type { get; set; }
    }
}