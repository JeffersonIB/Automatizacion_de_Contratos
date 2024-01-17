using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Contratos.Pages.Test
{
    public partial class Genera_Contrato3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string Representante = DDLRepresentante.SelectedValue;
            string selectedEmployeeId = CheckBoxListEmpleados.SelectedValue;
            string fecharelacion = frelacion.Value;
            string fechacontrato = fcontrato.Value;
            //string fileName = NombreDocumento.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = mainPart.Document.AppendChild(new Body());

                    // Add paragraphs and text using Open XML SDK
                    Paragraph titleParagraph = new Paragraph(new Run(new Text("CONTRATO INDIVIDUAL DE TRABAJO SUSCRITO ENTRE")));
                    Paragraph subTitleParagraph = new Paragraph(new Run(new Text("EXPORTADORA ENLASA, SOCIEDAD ANONIMA\nY EL TRABAJADOR " + "NOMBRE EMPLEADO")));
                    body.Append(titleParagraph, subTitleParagraph);
                    // Add more paragraphs and text as needed
                    mainPart.Document.Save();
                }
                string tempFilePath = Path.Combine(Path.GetTempPath(), "Archivo" + ".docx");
                File.WriteAllBytes(tempFilePath, memoryStream.ToArray());

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "Archivo" + ".docx");
                Response.WriteFile(tempFilePath);
                Response.Flush();
                Response.End();
            }
        }
        private string GetMonthName(int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }
    }
}