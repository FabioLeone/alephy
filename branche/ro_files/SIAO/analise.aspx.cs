﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.BLL;

namespace SIAO
{
    public partial class analise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FilesBLL.GetFiles("Analise", 7,true);
        }
    }
}