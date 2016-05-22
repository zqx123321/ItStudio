using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class seeWork : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("~/login.aspx");

        if (!IsPostBack)
        {
            Session["pagenum"] = 1;
            int currentPage = 1;
            int pageSize = getPageSize();
            ArticlesBind(currentPage, pageSize);
        }
    }

    protected void RptArticles_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete") //删除文章
        {
            string IDStr = e.CommandArgument.ToString().Trim();
            int id = Convert.ToInt32(IDStr);
            using (var db = new ITStudioEntities())
            {
                works del = db.works.SingleOrDefault(a => a.id == id);
                db.works.Remove(del);
                db.SaveChanges();
            }
            ArticlesBind(getPageNum(), getPageSize());
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除成功');</script>");
        }
    }
    void ArticlesBind(int CurrentPage, int PageSize) //文章绑定
    {
        using (var db = new ITStudioEntities())
        {
            var dataSource = from items in db.works
                             orderby items.id descending
                             select new { items.id, items.title, items.time, items.link };
            int totalAmount = dataSource.Count();
            Session["pageCount"] = Math.Ceiling((double)totalAmount / (double)PageSize); //总页数，向上取整
            dataSource = dataSource.Skip(PageSize * (CurrentPage - 1)).Take(PageSize); //分页
            RptArticles.DataSource = dataSource.ToList();
            RptArticles.DataBind();

            LtlArticlesCount.Text = totalAmount.ToString();
        }
    }
     protected void DdlPageSize_SelectedIndexChanged(object sender, EventArgs e) // pageSize下拉列表改变
    {
        ArticlesBind(1, getPageSize()); //从第一页绑定，防止单页项目数量变多，导致页码超出范围。
        TxtPageNum.Text = "1";
        Session["pagenum"] = 1;
    }

    int getPageCount(int pageSize) //获得总页数
    {
        int pageCount = 1;
        if (Session["pageCount"] == null)
        {
            using (var db = new ITStudioEntities())
            {
                var dataSource = from items in db.works
                                 orderby items.id
                                 select new { items };
                int totalAmount = dataSource.Count();
                pageCount = (int)Math.Ceiling((double)totalAmount / (double)pageSize); //总页数，向上取整
            }
            Session["pageCount"] = pageCount;
        }
        else
        {
            pageCount = Convert.ToInt32(Session["pageCount"]);
        }
        return pageCount;
    }

    int getPageSize() //获得页面项目数目
    {
        int pageSize = 5;
        if (DdlPageSize.SelectedValue != null)
        {
            pageSize = Convert.ToInt32(DdlPageSize.SelectedValue);
        }
        return pageSize;
    }

    int getPageNum() //获得当前文本框中的合法数字页码
    {
        int pageNum = Convert.ToInt16(Session["pagenum"]);
        return pageNum;
    }

    protected void BtnPreviousPage_Click(object sender, EventArgs e)
    {
        int pageNum = Convert.ToInt16(Session["pagenum"]) - 1;
        int pageSize = getPageSize();
        if (pageNum < 1)
        {
            pageNum = 1;
            return;
        }
        Session["pagenum"] = pageNum;
        ArticlesBind(pageNum, pageSize);
        TxtPageNum.Text = pageNum.ToString();
    }

    protected void BtnNextPage_Click(object sender, EventArgs e)
    {
        int pageNum = Convert.ToInt16(Session["pagenum"]) + 1;
        int pageSize = getPageSize();
        if (pageNum >= getPageCount(pageSize))
        {
            pageNum = getPageCount(pageSize);
        }
        Session["pagenum"] = pageNum;
        ArticlesBind(pageNum, pageSize);
        TxtPageNum.Text = pageNum.ToString();
    }

    protected void BtnHomePage_Click(object sender, EventArgs e)
    {
        ArticlesBind(1, getPageSize());
        TxtPageNum.Text = "1";
        Session["pagenum"] = 1;
    }

    protected void BtnTrailerPage_Click(object sender, EventArgs e)
    {
        int pageSize = getPageSize();
        int pageNum = getPageCount(pageSize);
        if (pageNum <= 0) //没有内容的情况
        {
            pageNum = 1;
        }
        Session["pagenum"] = pageNum;
        ArticlesBind(pageNum, pageSize);
        TxtPageNum.Text = pageNum.ToString();
    }

    protected void BtnJumpPage_Click(object sender, EventArgs e)
    {
        int pageNum ;
        if (!Filter.IsNumeric(TxtPageNum.Text)||TxtPageNum.Text.Length>8)
            pageNum = 1;
        else pageNum = Convert.ToInt32(TxtPageNum.Text);
        int pageSize = getPageSize();
        int pageCount = getPageCount(pageSize);
        if (pageNum < 1)
        {
            pageNum = 1;
        }
        else if (pageNum > pageCount)
        {
            pageNum = getPageCount(pageSize);
        }
        Session["pagenum"] = pageNum;
        ArticlesBind(pageNum, pageSize);
        TxtPageNum.Text = pageNum.ToString();
    }
}