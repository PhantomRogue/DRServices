using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.divOutput.InnerText = "What the Fook";
        }

        protected void btnLogWindow_Click(object sender, EventArgs e)
        {
            this.divOutput.InnerText = "Log Window";
        }

        protected void btnExpWindow_Click(object sender, EventArgs e)
        {
            this.divOutput.InnerText = "EXP Window";
        }

        protected void btnThoughts_Click(object sender, EventArgs e)
        {
            this.divOutput.InnerText = "Thoughts Window";
        }
    }
}