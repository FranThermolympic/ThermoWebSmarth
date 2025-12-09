using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
 using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using Excel = Microsoft.Office.Interop.Excel;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Extensions;
using OfficeOpenXml;

namespace ThermoWeb
{
    public partial class FichasParametros : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("/PRODUCCION/FichasParametros.aspx");

        }

    }
    
}