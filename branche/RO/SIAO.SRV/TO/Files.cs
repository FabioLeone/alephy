using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace SIAO.SRV.TO
{
    public class FilesTO
    {
        public int id { get; set; }
        public DateTime data { get; set; }
        public int UserId { get; set; }
        public String cnpj { get; set; }
        public String tipo { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }
        public String NomeFantasia { get; set; }
    }

    public class iTPageEventHandler : iTextSharp.text.pdf.PdfPageEventHelper
    {
        public Image ImageHeader { get; set; }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            ImageHeader.SetAbsolutePosition(30, 755);
            ImageHeader.ScalePercent(12);

            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            PdfPCell cell;
            Font f = new Font(Font.HELVETICA, 9F);
            tabFot.TotalWidth = 416F;
            Phrase p = new Phrase(new Chunk(ImageHeader, 0, -4));
            p.Add(new Chunk("UVF / CasBrasil: (19) 3368-4446 |  Antonio Silva: (19) 99222-0436 treinamento@casbrasil.com.br", f));
            cell = new PdfPCell(p);
            cell.Border = 0;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 100, document.Bottom, writer.DirectContent);

        }
    }
}
