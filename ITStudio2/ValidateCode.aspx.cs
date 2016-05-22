using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ValidateCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Captcha cap = new Captcha();
        string Code = cap.CreateVerifyCode(4,0);
        Session["Code"] = (Code.ToString()).ToLower();
        System.Drawing.Bitmap bitmap = cap.CreateImage(Code);
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        Response.Clear();
        Response.ContentType = "image/Png";
        Response.BinaryWrite(ms.GetBuffer());
        bitmap.Dispose();
        ms.Dispose();
        ms.Close();
        Response.End();
    }
}