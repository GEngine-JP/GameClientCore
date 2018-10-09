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

public partial class Product_ProductComment : System.Web.UI.Page
{
    private int productID = -1;
	protected void Page_Load(object sender,EventArgs e)
	{   ///获取商品的ID
		if(Request.Params["ProductID"] != null)
		{
			productID = Int32.Parse(Request.Params["ProductID"].ToString());
		}
		if(productID > 0)
		{
			BindPageData(productID);
		}
	}

	private void BindPageData(int productID)
	{   ///显示商品的评论
		Product product = new Product();
		gvComment.DataSource = product.GetCommentByProdcut(productID);
		gvComment.DataBind();
	}

	protected void gvComment_RowCommand(object sender,GridViewCommandEventArgs e)
	{
		Product product = new Product();
		if(e.CommandName.ToLower() == "del")
		{   ///删除选择的商品评论
			if(product.DeleteProductComment(Int32.Parse(e.CommandArgument.ToString())) > 0)
			{
				BindPageData(productID);
			}
			return;
		}
	}
	protected void gvComment_RowDataBound(object sender,GridViewRowEventArgs e)
	{   ///添加删除确认的对话框
		ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
		if(imgDelete != null)
		{
			imgDelete.Attributes.Add("onclick","return confirm(\"您确认要删除当前行的商品评论吗？\");");
		}
	}
}
