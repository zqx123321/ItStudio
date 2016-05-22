<%@ WebHandler Language="C#" Class="APIforyuner" %>

using System;
using System.Web;
using System.Linq;
using Newtonsoft.Json;

public class APIforyuner : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "text/plain";
        int Year ;
        Year =Convert.ToInt32( context.Request.Form["year"]);
        using (var db = new ITStudioEntities())
        {
            var dataSource = from items in db.members
                             orderby items.id descending
                             where items.grade == Year
                             select new { items.name, items.job, items.photo};
            string final = JsonConvert.SerializeObject(dataSource);
            context.Response.Write(final);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}