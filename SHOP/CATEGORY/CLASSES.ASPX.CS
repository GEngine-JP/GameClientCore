using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

///引入新的命名空间
using ASPNETAJAXWeb.AjaxEBusiness;

public partial class Category_Category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if(!Page.IsPostBack)
		{
			BindPageData();
		}
	}

	private void BindPageData()
	{   ///获取数据
		Category category = new Category();
		DataSet ds = category.GetFenleis();
		///显示数据
		gvCategory.DataSource = ds;
		gvCategory.DataBind();
	}
	protected void gvCategory_RowCommand(object sender,GridViewCommandEventArgs e)
	{
		if(e.CommandName.ToLower() == "update")
		{   ///重定向到修改分类页面
			Response.Redirect("~/Category/UpdateClasses.aspx?CategoryID=" + e.CommandArgument.ToString());
			return;
		}
		Category category = new Category();
		if(e.CommandName.ToLower() == "up" || e.CommandName.ToLower() == "down")
		{
            category.UpdateFenleiOrder(Int32.Parse(e.CommandArgument.ToString()), e.CommandName);
			BindPageData();
			return;
		}

		if(e.CommandName.ToLower() == "del")
		{   ///删除选择的商品分类
            if (category.DeleteFenlei(Int32.Parse(e.CommandArgument.ToString())) > 0)
			{
				BindPageData();
			}
			return;
		}
	}
	protected void gvCategory_RowDataBound(object sender,GridViewRowEventArgs e)
	{   ///添加删除确认的对话框
		ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
		if(imgDelete != null)
		{
			imgDelete.Attributes.Add("onclick","return confirm(\"您确认要删除当前行的商品分类吗？\");");
		}
	}

	protected void btnAdd_Click(object sender,EventArgs e)
	{
		Response.Redirect("~/Category/AddClasses.aspx");
	}
}
