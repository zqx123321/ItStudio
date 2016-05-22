<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Linq;
using Newtonsoft.Json;


public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string name = context.Request.Form["name"];
        string pwd = context.Request["password"];
        using (var db = new ITStudioEntities())
        {
            var a = new admins();
            a.name = name;
            a.password = pwd;
            db.admins.Add(a);
            db.SaveChanges();
        }
       context.Response.Write("添加成功");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}