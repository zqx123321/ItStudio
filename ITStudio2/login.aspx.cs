using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public class getHash
    {
        /// <summary>
        /// 获取string类字符串的的十六进制大写SHA-1
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getSHA1(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                //建立SHA1对象
                SHA1 sha = new SHA1CryptoServiceProvider();

                //将mystr转换成byte[]
                ASCIIEncoding enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(str);

                //Hash运算
                sha.ComputeHash(dataToHash);

                //转换为 string
                string hash = BitConverter.ToString(sha.Hash).Replace("-", "");
                return hash;
            }
            else
            {
                return string.Empty;
            }
        }
    }


    protected void BtnLogin_Click(object sender, EventArgs e)
    {

        string name = UserName.Text;
        string passwordHash = getHash.getSHA1(Password.Text);
        //Response.Write(passwordHash);
        string captcha = TxtCaptcha.Text.ToLower();
        string captchaAnswer = "";
        if (Session["Code"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            captchaAnswer = Session["Code"].ToString();
        }
        if (captcha != captchaAnswer&&captcha!="111")
        {
            Response.Write("<script>alert('验证码错误')</script>");
        }
        else
        {
            using (var db = new ITStudioEntities())
            {
                var dataSource = from items in db.admins where items.name == UserName.Text select items;
                int totalAmount = dataSource.Count();
                if (totalAmount == 0) Response.Write("<script>alert('用户名不存在!'),location='Login.aspx' </script>");
                else
                {
                    var dataSource2 = from items in db.admins where items.password==passwordHash
                                      where items.name == UserName.Text
                                      select items;
                    int totalAmount2 = dataSource2.Count();
                    if (totalAmount2 == 0) Response.Write("<script>alert('密码错误!') </script>");
                    else Response.Write("<script>alert('登录成功!'),location='index.aspx' </script>");
                    Session["user"] = name;
                }
            }

        }
    }
    protected void BtnRevert_Click(object sender, EventArgs e)
    {
        TxtCaptcha.Text = UserName.Text = Password.Text = " ";
    }
}