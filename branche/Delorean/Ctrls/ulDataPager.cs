using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ctrls
{
    public class ulDataPager : DataPager
    {
        protected override HtmlTextWriterTag TagKey
        {
            get {return HtmlTextWriterTag.Ul; }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (HasControls())
            {
                System.Web.UI.HtmlControls.HtmlGenericControl i;
                int c = 0;
                foreach (Control child in Controls)
                {
                    var item = child as DataPagerFieldItem;
                    if (item == null || !item.HasControls())
                    {
                        child.RenderControl(writer);
                        continue;
                    }

                    foreach (Control button in item.Controls)
                    {
                        var space = button as LiteralControl;
                        if (space != null && space.Text == "&nbsp;") continue;

                        if (c == 0)
                        {
                            i = new System.Web.UI.HtmlControls.HtmlGenericControl("i");
                            i.Attributes.Add("class", "icon-caret-left");
                            button.Controls.Add(i);
                        }
                        else if (c == 2)
                        {
                            i = new System.Web.UI.HtmlControls.HtmlGenericControl("i");
                            i.Attributes.Add("class", "icon-caret-right");
                            button.Controls.Add(i);
                        }

                        writer.RenderBeginTag(HtmlTextWriterTag.Li);
                        button.RenderControl(writer);
                        writer.RenderEndTag();
                    }

                    c++;
                }
            }
        }
    }
}
