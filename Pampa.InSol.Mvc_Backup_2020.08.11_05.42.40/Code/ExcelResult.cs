using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Code
{
    /// <summary>
    /// Resultado del tipo Excel para que el usuario se descargue
    /// </summary>
    public class ExcelResult : ActionResult
    {
        public string FileName { get; set; }

        public byte[] Excel { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + this.FileName);
            context.HttpContext.Response.ContentType = "application/vnd.ms-excel";
            context.HttpContext.Response.BinaryWrite(this.Excel);
        }
    }
}