using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;

namespace ThermoWeb
{
    public partial class FichasParametros_nuevo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("/PRODUCCION/FichasParametros_nuevo.aspx");
        }
        
    }

}