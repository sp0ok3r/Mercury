using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercuryBOT.Helpers
{
    public static class Html
    {
        public class Table : HtmlBase, IDisposable
        {
            public Table(StringBuilder sb, string classAttributes = "", string id = "") : base(sb)
            {
                Append("<table border=1>");
            }

            public void StartHead(string classAttributes = "", string id = "")
            {
                Append("<thead>");
            }

            public void EndHead()
            {
                Append("</thead>");
            }

            public void StartFoot(string classAttributes = "", string id = "")
            {
                Append("<tfoot>");
            }

            public void EndFoot()
            {
                Append("</tfoot>");
            }

            public void StartBody(string classAttributes = "", string id = "")
            {
                Append("<tbody>");
            }

            public void EndBody()
            {
                Append("</tbody>");
            }

            public void Dispose()
            {
                Append("</table>");
            }

            public Row AddRow(string classAttributes = "", string id = "")
            {
                return new Row(GetBuilder(), classAttributes, id);
            }
        }

        public class Row : HtmlBase, IDisposable
        {
            public Row(StringBuilder sb, string classAttributes = "", string id = "") : base(sb)
            {
                Append("<tr>");
            }
            public void Dispose()
            {
                Append("</tr>");
            }
            public void AddCell(string innerText, string classAttributes = "", string id = "", string colSpan = "")
            {
                Append("<td>");
                Append(innerText);
                Append("</td>");
            }
        }

        public abstract class HtmlBase
        {
            private StringBuilder _sb;

            protected HtmlBase(StringBuilder sb)
            {
                _sb = sb;
            }

            public StringBuilder GetBuilder()
            {
                return _sb;
            }

            protected void Append(string toAppend)
            {
                _sb.Append(toAppend);
            }
        }
    }
}