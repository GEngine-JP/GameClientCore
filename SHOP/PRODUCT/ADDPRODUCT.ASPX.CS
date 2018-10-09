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

///引入新的名字空间
using ASPNETAJAXWeb.AjaxEBusiness;

public partial class Product_AddProduct : System.Web.UI.Page
{
	int categoryID = -1;
	int userID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {   ///判断用户是否登录，并获取用户信息
		if(Session["UserID"] == null)
		{
			Response.Redirect("~/Default.aspx");
		}
		userID = Int32.Parse(Session["UserID"].ToString());
    }
	protected void btnCommit_Click(object sender,EventArgs e)
	{
		if(AddProduct() > 0)
		{
			Response.Redirect("~/Product/Product.aspx");
		}
	}

	private int AddProduct()
	{
		Product product = new Product();
		int productID = product.AddProduct(
			tbName.Text,
			ucProductAttribute.CategoryID,
			userID,
			decimal.Parse(tbPrice.Text.Trim()),
			Int32.Parse(tbStock.Text.Trim()),
			tbRemark.Text);
		if(productID <= 0) return productID;
		///添加商品特性的值
		foreach(Control c in ucProductAttribute.AttributePanel.Controls)
		{
			if(c is TextBox)
			{   ///读取属性及其值
				TextBox tbox = (TextBox)c;
				string s = tbox.Text;
				product.AddAttributeValue(
					productID,
					Int32.Parse(tbox.Attributes["IDValue"]),
					tbox.Text);
			}
		}		
		return productID;
	}

}
