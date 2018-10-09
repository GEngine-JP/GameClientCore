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
using System.Data.SqlClient;

public partial class Product_UpdateProduct : System.Web.UI.Page
{
	private int productID = -1;

    protected void Page_Load(object sender, EventArgs e)
    {   ///获取商品的ID值
		if(Request.Params["ProductID"] != null)
		{
			productID = Int32.Parse(Request.Params["ProductID"].ToString());
		}
		if(!Page.IsPostBack && productID > 0)
		{
			BindPageData(productID);
		}
    }

	private void BindPageData(int productID)
	{   ///获取商品信息
		Product product = new Product();
		SqlDataReader dr = product.GetSingleProduct(productID);
		if(dr == null) return;
		///读取商品信息
		if(dr.Read())
		{
			tbName.Text = dr["Name"].ToString();
			tbRemark.Text = dr["Remark"].ToString();
		}
		dr.Close();
	}

	private int UpdateProduct()
	{   ///修改商品的属性值
		Product product = new Product();
		int success = product.UpdateProduct(productID,tbName.Text,tbRemark.Text);
		if(success <= 0) return success - 1;
		///修改商品特性的值
		foreach(Control c in ucProductAttribute.AttributePanel.Controls)
		{
			if(c is TextBox)
			{
				TextBox tbox = (TextBox)c;
				product.AddAttributeValue(productID,
					Int32.Parse(tbox.Attributes["IDValue"]),
					tbox.Text);
			}
		}
		return success;
	}

	protected void btnCommit_Click(object sender,EventArgs e)
	{
		if(UpdateProduct() > 0)
		{
			Response.Redirect("~/Product/Product.aspx");
		}
	}
}
