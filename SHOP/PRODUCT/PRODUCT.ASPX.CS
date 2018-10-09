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

using ASPNETAJAXWeb.AjaxEBusiness;

public partial class Product_Product : System.Web.UI.Page
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
		///显示分类的层次结构
		category.InitFenleiList(ddlCategory);
		///设置第一项为选择项，如果存在
		if(ddlCategory.Items.Count > 0)
		{
			ddlCategory.SelectedIndex = 0;
			BindProductData(Int32.Parse(ddlCategory.SelectedValue));
		}
	}

	private void BindProductData(int categoryID)
	{   ///获取商品数据
		Product product = new Product();
        DataSet ds = product.GetProductByFenlei(categoryID);
		if(ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
		///显示商品信息
		gvProduct.DataSource = ds;
		gvProduct.DataBind();
	}

	protected void gvProduct_RowCommand(object sender,GridViewCommandEventArgs e)
	{
		if(e.CommandName.ToLower() == "update")
		{   ///重定向到修改商品页面
			Response.Redirect("~/Product/UpdateProduct.aspx?ProductID=" + e.CommandArgument.ToString());
			return;
		}
		if(e.CommandName.ToLower() == "picture")
		{   ///重定向到修改商品图片页面
			Response.Redirect("~/Product/ProductPicture.aspx?ProductID=" + e.CommandArgument.ToString());
			return;
		}
		if(e.CommandName.ToLower() == "comment")
		{   ///重定向到管理商品评论页面
			Response.Redirect("~/Product/ProductComment.aspx?ProductID=" + e.CommandArgument.ToString());
			return;
		}
		if(e.CommandName.ToLower() == "del")
		{   ///删除选择的商品
			Product product = new Product();
			if(product.DeleteProduct(Int32.Parse(e.CommandArgument.ToString())) > 0)
			{
				BindPageData();
			}
			return;
		}
	}
	protected void gvProduct_RowDataBound(object sender,GridViewRowEventArgs e)
	{   ///添加删除确认的对话框
		ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
		if(imgDelete != null)
		{
			imgDelete.Attributes.Add("onclick","return confirm(\"您确认要删除当前行的商品吗？\");");
		}
	}
	protected void btnAdd_Click(object sender,EventArgs e)
	{
		Response.Redirect("~/Product/AddProduct.aspx");
	}
	protected void ddlCategory_SelectedIndexChanged(object sender,EventArgs e)
	{
		BindProductData(Int32.Parse(ddlCategory.SelectedValue));
	}
}
